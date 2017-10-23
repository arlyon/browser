namespace Browser.Views
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using global::Browser.Requests;

    /// <summary>
    /// The interface for interacting with a tab in the browser.
    /// </summary>
    public interface ITab
    {
        int FaviconIndex { set; }

        bool Loading { set; }

        string Name { get; set; }

        void CanGoBack(bool value);

        void CanGoForward(bool value);

        void CanReload(bool canReload);

        void Display(HttpResponse response, string title);

        void Display(string title);

        void Display(HttpResponse httpResponse);

        /// <summary>
        /// The show.
        /// </summary>
        void Show();
        
        event EventHandler AddFavorite;

        event EventHandler Back;

        event EventHandler Forward;

        event EventHandler GoHome;

        event EventHandler Reload;

        event EventHandler RenderPage;

        event UrlUpdateEventHandler Submit;
    }

    /// <summary>
    /// The url update event handler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    public delegate void UrlUpdateEventHandler(object sender, UrlUpdateEventArgs e);

    /// <summary>
    /// The url update event args.
    /// </summary>
    public class UrlUpdateEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlUpdateEventArgs"/> class.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        public UrlUpdateEventArgs(Url url)
        {
            this.Url = url;
        }

        /// <summary>
        /// Gets the url.
        /// </summary>
        public Url Url { get; }
    }
}