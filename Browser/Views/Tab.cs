namespace Browser.Views
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using global::Browser.Properties;
    using global::Browser.Requests;

    /// <inheritdoc cref="TabPage" />
    /// <summary>
    ///     The base class for the browser tabs.
    /// </summary>
    public partial class Tab : TabPage, ITab
    {
        /// <summary>
        /// The synchronization context.
        /// </summary>
        private static readonly SynchronizationContext SynchronizationContext = SynchronizationContext.Current;

        /// <summary>
        ///     Held onto so that the spinner can reference it when done spinning.
        /// </summary>
        private int _faviconIndex;

        private bool _loading;

        public Tab()
        {
            this.InitializeComponent();
            this.Text = Resources.NewTab;
            this.Loading = false;
            this._faviconIndex = 0;
            this.UrlBar.Focus();

            // cant manipulate the browser history is there is none
            this.CanReload(false);
            this.CanGoForward(false);
            this.CanGoBack(false);
        }

        public event EventHandler AddFavorite;

        public event EventHandler Back;

        public event EventHandler Forward;

        public event EventHandler GoHome;

        public event EventHandler Reload;

        public event EventHandler RenderPage;

        public event UrlUpdateEventHandler Submit;

        public int FaviconIndex
        {
            set
            {
                this._faviconIndex = value;
                this.ImageIndex = this._faviconIndex;
            }
        }

        /// <summary>
        ///     Sets the loading value and starts the spinner if not already running.
        /// </summary>
        public bool Loading
        {
            get => this._loading;
            set
            {
                if (value && !this._loading)
                {
                    this._loading = true;
                    this.StartSpinner();
                }
                else
                {
                    this._loading = value;
                }
            }
        }

        public void CanGoBack(bool value)
        {
            this.GoBackButton.Enabled = value;
            this.GoBackButton.Visible = value;
        }

        public void CanGoForward(bool value)
        {
            this.GoForwardButton.Enabled = value;
            this.GoForwardButton.Visible = value;
        }

        public void CanReload(bool value)
        {
            this.ReloadButton.Enabled = value;
            this.ReloadButton.Visible = value;
        }

        public bool Contains(Point eLocation)
        {
            return this.ClientRectangle.Contains(eLocation);
        }

        public void Display(HttpResponse response, string title)
        {
            this.Text = title;
            this.UrlBar.Text = response.Url.ToString();
            this.ResponseMessageBox.Text = response.Header;
            this.StatusCode.Text = (int)response.Status + " - " + response.Status;

            var statusClass = (int)response.Status / 100;

            // color the status code
            if (statusClass == 2) this.StatusCode.ForeColor = Color.Green;
            else if (statusClass <= 4) this.StatusCode.ForeColor = Color.Red;

            // tidy the html document and display it
            using (var doc = Document.FromString(response.Content))
            {
                doc.ShowWarnings = false;
                doc.AddTidyMetaElement = false;
                doc.Quiet = true;
                doc.IndentBlockElements = AutoBool.Auto;
                doc.CleanAndRepair();
                this.ResponseBodyBox.Text = doc.Save();
            }
        }

        public int GetIndex()
        {
            return this.TabIndex;
        }

        private void BackButtonPressed(object sender, EventArgs e)
        {
            this.Back?.Invoke(this, e);
        }

        private void FavoritesButtonPressed(object sender, EventArgs e)
        {
            this.AddFavorite?.Invoke(this, e);
        }

        private void ForwardButtonPressed(object sender, EventArgs e)
        {
            this.Forward?.Invoke(this, e);
        }

        private void HomeButtonPressed(object sender, EventArgs e)
        {
            this.GoHome?.Invoke(this, e);
        }

        private void OnUrlKeyDown(object sender, KeyEventArgs e)
        {
            // on enter key press
            if (e.KeyCode != Keys.Enter) return;
            this.Submit?.Invoke(this, UrlUpdateEventArgs.FromKeyEventArgs(e, new Url(this.UrlBar.Text)));
        }

        private void ReloadButtonPressed(object sender, EventArgs e)
        {
            this.Reload?.Invoke(this, e);
        }

        private void Render_Click(object sender, EventArgs e)
        {
            this.RenderPage?.Invoke(sender, e);
        }

        /// <summary>
        /// The start spinner.
        /// </summary>
        private async void StartSpinner()
        {
#if DEBUG
            await Task.Run(
                () =>
                    {
                        var i = 0;

                        while (this.Loading)
                        {
                            // the spinner has 16 frames from id 1-16;
                            i = (i % 16) + 1;

                            // post a new message to the main thread updating the index
                            SynchronizationContext.Post(index => { this.ImageIndex = (int)index; }, i);

                            // 8 frames per second
                            Thread.Sleep(125);
                        }
                    });

            // set the image to the correct index for the favicon
            this.ImageIndex = this._faviconIndex;
#endif
        }
    }
}