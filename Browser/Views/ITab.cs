namespace Browser.Views
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using global::Browser.Requests;

    /// <summary>
    ///     The interface for interacting with a tab in the browser.
    /// </summary>
    public interface ITab
    {
        event EventHandler AddFavorite;

        event EventHandler Back;

        event EventHandler Forward;

        event EventHandler GoHome;

        event EventHandler Reload;

        event EventHandler RenderPage;

        event UrlUpdateEventHandler Submit;

        int FaviconIndex { set; }

        bool Loading { get; set; }

        string Name { get; set; }

        void CanGoBack(bool value);

        void CanGoForward(bool value);

        void CanReload(bool canReload);

        bool Contains(Point eLocation);

        void Display(HttpResponse response, string title);

        int GetIndex();
    }

    public delegate void UrlUpdateEventHandler(object sender, UrlUpdateEventArgs e);

    public class UrlUpdateEventArgs : KeyEventArgs
    {
        public Url Url;

        public UrlUpdateEventArgs(Keys keyData, Url url)
            : base(keyData)
        {
            this.Url = url;
        }

        public static UrlUpdateEventArgs FromKeyEventArgs(KeyEventArgs e, Url url)
        {
            return new UrlUpdateEventArgs(e.KeyData, url);
        }
    }
}