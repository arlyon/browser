namespace Browser.Presenters
{
    using System;
    using System.Drawing;

    using Browser.Cache;
    using Browser.Config;
    using Browser.Favorites;
    using Browser.History;
    using Browser.Requests;
    using Browser.Views;

    /// <summary>
    /// The tab presenter.
    /// </summary>
    public class TabPresenter
    {
        /// <summary>
        /// The config.
        /// </summary>
        private readonly IConfig _config;

        /// <summary>
        /// The _favicons.
        /// </summary>
        private readonly IFaviconCache _favicons;

        /// <summary>
        /// The favorites.
        /// </summary>
        private readonly IFavorites _favorites;

        /// <summary>
        /// The _tab.
        /// </summary>
        private readonly ITab _tab; // view

        /// <summary>
        /// The _current response.
        /// </summary>
        private HttpResponse _currentResponse;

        /// <summary>
        /// The _name.
        /// </summary>
        private string _name;

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
        public TabPresenter(ITab tab, IFavorites favorites, IConfig config, IFaviconCache favicons, IHistory history)
        {
            this.TabHistory = new TabHistory(history);
            this._tab = tab;
            this._favorites = favorites;
            this._config = config;
            this._favicons = favicons;

            // so that we can identify it later
            this.Name = Guid.NewGuid().ToString();

            this._tab.AddFavorite += this.AddFavorite;
            this._tab.Back += this.Back;
            this._tab.Forward += this.Forward;
            this._tab.GoHome += this.GoHomeEvent;
            this._tab.Reload += this.Reload;
            this._tab.Submit += this.Submit;
            this._tab.RenderPage += this.RenderPage;

            this._favicons = favicons;

            this.TabHistory.OnCurrentLocationChange += this.LoadPage;
        }

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
        /// Gets the tab history.
        /// </summary>
        protected ITabHistory TabHistory { get; }

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="screenLocation">
        /// The e location.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Contains(Point screenLocation)
        {
            return this._tab.Contains(screenLocation);
        }

        /// <summary>
        /// The get index.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetIndex()
        {
            return this._tab.GetIndex();
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
                    "/search?q=" + (url.Host + url.Addon).Replace(' ', '+'),
                    string.Empty);
            this.TabHistory.Push(url);
        }

        /// <summary>
        /// The reload.
        /// </summary>
        public void Reload()
        {
            this.LoadCurrentPage();
        }

        /// <summary>
        /// The url.
        /// </summary>
        /// <returns>
        /// The <see cref="Url"/>.
        /// </returns>
        public Url Url()
        {
            return this.TabHistory.Current()?.Url;
        }

        /// <summary>
        /// The add favorite.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void AddFavorite(object sender, EventArgs e)
        {
            EditFavoritesWindow ef = new EditFavoritesWindow();
            EditFavoritesPresenter pres = new EditFavoritesPresenter(ef, this.TabHistory.Current().Url, this._favorites);
            ef.Show();
        }

        /// <summary>
        /// The back.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Back(object sender, EventArgs e)
        {
            if (this.TabHistory.CanGoBackward()) this.TabHistory.Back();
        }

        /// <summary>
        /// The forward.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Forward(object sender, EventArgs e)
        {
            if (this.TabHistory.CanGoForward()) this.TabHistory.Forward();
        }

        /// <summary>
        /// The go home event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void GoHomeEvent(object sender, EventArgs e)
        {
            this.TabHistory.Push(this._config.Home);
        }

        /// <summary>
        ///     Loads the current historyLocation in the tab's history.
        /// </summary>
        private async void LoadCurrentPage()
        {
            this._tab.Loading = true;
            var l = this.TabHistory.Current();
            try
            {
                var response = HttpRequest.GetAsync(this.TabHistory.Current().Url);
                var favicontask = this._favicons.Request(this.TabHistory.Current().Url);
                var page = await response;
                var titletask = page.GetTitle();
                var favicon = await favicontask;

                if (!this.TabHistory.Current().Equals(l)) return; // make sure the user hasnt navigated away

                this._tab.FaviconIndex = favicon;
                this._currentResponse = page;
                this._tab.Display(page, page.Url.Host);
                if (string.IsNullOrEmpty(await titletask)) return;
                if (!this.TabHistory.Current().Equals(l)) return; // make sure the user hasnt navigated away
                this._tab.Display(page, await titletask);
                this.TabHistory.UpdateTitle(await titletask);
            }
            catch
            {
                // TODO handle when pages arent properly loaded
                // page could not be loaded
            }

            this._tab.Loading = false;
            this._tab.CanGoForward(this.TabHistory.CanGoForward());
            this._tab.CanGoBack(this.TabHistory.CanGoBackward());
            this._tab.CanReload(this.TabHistory.CanReload());
        }

        /// <summary>
        /// The load page.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void LoadPage(object sender, EventArgs e)
        {
            this.LoadCurrentPage();
        }

        /// <summary>
        /// The reload.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Reload(object sender, EventArgs e)
        {
            this.LoadCurrentPage();
        }

        /// <summary>
        /// The render page.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void RenderPage(object sender, EventArgs e)
        {
            if (this._currentResponse != null) new RenderDocument(this._currentResponse).Show();
        }

        /// <summary>
        /// The submit.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Submit(object sender, UrlUpdateEventArgs e)
        {
            this.Push(e.Url);
        }
    }

    /// <summary>
    /// The incognito tab presenter.
    /// </summary>
    internal class IncognitoTabPresenter : TabPresenter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IncognitoTabPresenter"/> class.
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
        public IncognitoTabPresenter(ITab tab, IFavorites favorites, IConfig config, IFaviconCache favicons)
            : base(tab, favorites, config, favicons, null)
        {
        }
    }
}