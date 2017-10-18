namespace Browser.Views
{
    using System.Windows.Forms;

    using global::Browser.Requests;

    /// <summary>
    /// The render document.
    /// </summary>
    public partial class RenderDocument : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderDocument"/> class.
        /// </summary>
        /// <param name="document">
        /// The document.
        /// </param>
        public RenderDocument(HttpResponse document)
        {
            this.InitializeComponent();
            this.Display(document);
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="document">
        /// The document.
        /// </param>
        private async void Display(HttpResponse document)
        {
            this.Text = await document.GetTitle();
            this.Browser.DocumentText = document.Content;
        }
    }
}