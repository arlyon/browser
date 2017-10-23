namespace Browser.Presenters
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    using Browser.Config;
    using Browser.Favicon;
    using Browser.Favorites;
    using Browser.History;
    using Browser.Requests;
    using Browser.Views;

    /// <summary>
    /// The tab presenter.
    /// </summary>
    public class TabPresenter : ITabPresenter
    {
        /// <summary>
        /// The config.
        /// </summary>
        private readonly IConfig _config;

        /// <summary>
        /// The _favicons.
        /// </summary>
        private readonly IFavicon _favicons;

        /// <summary>
        /// The favorites.
        /// </summary>
        private readonly IFavorites _favorites;

        /// <summary>
        /// The _tab.
        /// </summary>
        private readonly ITab _tab;

        /// <summary>
        /// Gets the tab history.
        /// </summary>
        private readonly ITabHistory _tabHistory;

        /// <summary>
        /// The _current response.
        /// </summary>
        private HttpResponse _currentResponse;

        /// <summary>
        /// The _name.
        /// </summary>
        private string _name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => this._name;
            set
            {
                this._tab.Name = value;
                this._name = value;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TabPresenter"/> class.
        /// </summary>
        /// <param name="tab">
        /// The tab.
        /// </param>
        /// <param name="favorites">
        /// The favorites.
        /// </param>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="favicons">
        /// The favicons.
        /// </param>
        /// <param name="history">
        /// The history.
        /// </param>
        /// <param name="tabHistory">
        /// The tab History.
        /// </param>
        public TabPresenter(ITab tab, IFavorites favorites, IConfig config, IFavicon favicons, IHistory history, ITabHistory tabHistory)
        {
            // initialize variables
            this._tabHistory = tabHistory;
            this._tab = tab;
            this._favorites = favorites;
            this._config = config;
            this._favicons = favicons;

            // give guid for easy identification
            this.Name = Guid.NewGuid().ToString();

            // register the event listeners
            this.RegisterEventListeners();

            tab.Show();
        }

        /// <summary>
        /// Sets up all the event listeners.
        /// </summary>
        private void RegisterEventListeners()
        {
            this._tabHistory.OnCurrentLocationChange += (s, e) => this.LoadCurrentPage();

            this._tab.Reload += (s, e) => this.Reload();
            this._tab.Back += (s, e) => { if (this._tabHistory.CanGoBackward()) this._tabHistory.Back(); };
            this._tab.Forward += (s, e) => { if (this._tabHistory.CanGoForward()) this._tabHistory.Forward(); };
            this._tab.GoHome += (s, e) => this._tabHistory.Push(this._config.Home);
            this._tab.Submit += (s, e) => this.Push(e.Url);
            this._tab.RenderPage += (s, e) => { if (this._currentResponse != null) new RenderDocument(this._currentResponse).Show(); };
            this._tab.AddFavorite += (s, e) =>
                {
                    var ef = new EditFavoritesWindow();
                    var pres = new EditFavoritesPresenter(
                        ef,
                        this._tabHistory.Current().Url,
                        this._favorites);
                };
        }
        
        /// <summary>
        /// Pushes a url, or alternatively a google search to the tab history which then triggers a page load.
        /// </summary>
        /// <param name="url">The url to push.</param>
        public void Push(Url url)
        {
            if (url == null || url.ToString().Equals(string.Empty)) return;
            if (!url.Host.Contains("."))
                url = new Url(
                    "https://",
                    "www.google.com",
                    "/search?q=" + (url.Host + url.Path).Replace(' ', '+'));
            this._tabHistory.Push(url);
        }

        /// <summary>
        /// The reload.
        /// </summary>
        public void Reload()
        {
            this.LoadCurrentPage();
        }

        /// <summary>
        /// Returns the url that the page is currently visiting.
        /// </summary>
        /// <returns>
        /// The <see cref="Url"/>.
        /// </returns>
        public Url Url()
        {
            return this._tabHistory.Current()?.Url;
        }

        /// <summary>
        /// Loads the current historyLocation in the tab's history.
        /// </summary>
        private async void LoadCurrentPage()
        {
            this._tab.Loading = true;
            this._tab.Display(this._tabHistory.Current().Url.Host);

            try
            {
                await this.MakeAndDisplayRequest();
            }
            catch (Exception e)
            {
                this.HandleAndDisplayError(e);
            }

            this._tab.Loading = false;
        }

        /// <summary>
        /// The make and display request.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task MakeAndDisplayRequest()
        {
            var locationAtRequestStart = this._tabHistory.Current();
            var responseTask = HttpRequest.GetAsync(this._tabHistory.Current().Url);
            var faviconIndexTask = this._favicons.Request(this._tabHistory.Current().Url);
            var page = await responseTask;
            var titletask = page.GetTitle();
            var favicon = await faviconIndexTask;

            // make sure the user hasnt navigated away and update page
            if (!this._tabHistory.Current().Equals(locationAtRequestStart)) return;
            this._tab.FaviconIndex = favicon;
            this._currentResponse = page;
            this._tab.Display(page, page.Url.Host);
            this._tab.CanGoForward(this._tabHistory.CanGoForward());
            this._tab.CanGoBack(this._tabHistory.CanGoBackward());
            this._tab.CanReload(this._tabHistory.CanReload());

            // make sure the user hasnt navigated away and update title
            if (!this._tabHistory.Current().Equals(locationAtRequestStart) ||
                string.IsNullOrEmpty(await titletask)) return;

            this._tab.Display(await titletask); // push the title to the tab title
            this._tabHistory.UpdateTitle(await titletask); // and to histoy
        }

        /// <summary>
        /// The handle and display error.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        private void HandleAndDisplayError(Exception e)
        {
            // if there is an exception, display it on the ui
            if (e is WebException we && we.Response is HttpWebResponse response)
            {
                this._tab.Display(
                    new HttpResponse()
                        {
                            Content = e.Message,
                            Url = this._tabHistory.Current().Url,
                            Status = response.StatusCode
                        });
            }
            else this._tab.Display(new HttpResponse() { Content = e.Message, Url = this._tabHistory.Current().Url });
        }
    }
}