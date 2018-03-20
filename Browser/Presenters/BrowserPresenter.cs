namespace Browser.Presenters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Browser.Config;
    using Browser.Favicon;
    using Browser.Favorites;
    using Browser.History;
    using Browser.Requests;
    using Browser.Views;

    /// <summary>
    /// The primary class for the browser. Handles the tabs, history and favorites.
    /// </summary>
    /// <typeparam name="T">
    /// The type of tab.
    /// </typeparam>
    public class BrowserPresenter<T> : IEnumerable<T> where T : ITabPresenter
    {
        /// <summary>
        /// The browser view.
        /// </summary>
        private readonly IBrowser _browser; // view

        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfig _config; // model

        /// <summary>
        /// The favicon cache.
        /// </summary>
        private readonly IFavicon _favicons; // model

        /// <summary>
        /// The favorites manager.
        /// </summary>
        private readonly IFavorites _favorites; // model

        /// <summary>
        /// The history manager.
        /// </summary>
        private readonly IHistory _history; // model

        /// <summary>
        /// Uses a factory method to create a tab of a specific concrete implementation.
        /// </summary>
        /// <returns>
        /// The <see cref="ITabPresenter"/>.
        /// </returns>
        private readonly Func<ITab, IFavorites, IConfig, IFavicon, IHistory, ITabHistory, T> _newTabFactory; // factory

        /// <summary>
        /// The tabs.
        /// </summary>
        private readonly List<T> _tabs;
        
        /// <summary>
        /// The current tab index.
        /// </summary>
        private int currentTabIndex;

        /// <summary>
        /// Gets or sets the current tab index.
        /// </summary>
        private int CurrentTabIndex
        {
            get => this.currentTabIndex;
            set => this.currentTabIndex = value % this._tabs.Count;
        }

        /// <summary>
        /// Returns the <see cref="T"/> at the specified index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        private T this[int index] => this._tabs[index];

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserPresenter{T}"/> class. 
        /// </summary>
        /// <param name="browser">
        /// The browser form.
        /// </param>
        /// <param name="history">
        /// The history model.
        /// </param>
        /// <param name="favorites">
        /// The favorites model.
        /// </param>
        /// <param name="config">
        /// The config model.
        /// </param>
        /// <param name="favicons">
        /// The favicon caching engine.
        /// </param>
        /// <param name="newTabFactory">
        /// The factory method for the tabs.
        /// </param>
        public BrowserPresenter(
            IBrowser browser,
            IHistory history,
            IFavorites favorites,
            IConfig config,
            IFavicon favicons,
            Func<ITab, IFavorites, IConfig, IFavicon, IHistory, ITabHistory, T> newTabFactory)
        {
            // instantiate the variables
            this._browser = browser;
            this._history = history;
            this._favorites = favorites;
            this._favicons = favicons;
            this._config = config;
            this._newTabFactory = newTabFactory;

            this._tabs = new List<T>();

            // register the event handlers
            this.RegisterEventHandlers();

            // bind the data to the page
            this.BindData();

            // show window
            this._browser.Show();
            
            // create a new tab
            this.NewTab(this._config.Home);
        }

        /// <summary>
        /// Registers all the event handlers.
        /// </summary>
        private void RegisterEventHandlers()
        {
            // switch to the selected index in the ui when the tab changes
            this._browser.TabChanged += (sender, e) => this.SwitchToTab(e.SelectedIndex);

            // reload the currently open tab
            this._browser.ReloadTab += (s, e) => this._tabs[this.CurrentTabIndex].Reload();

            // close the currently open tab
            this._browser.CloseTab += (s, e) => this.CloseTab(this._tabs[this.CurrentTabIndex].Name);

            // close the window
            this._browser.CloseWindow += (s, e) => this._browser.Close();

            this._browser.GoHome += (s, e) => this._tabs[this.CurrentTabIndex].Push(this._config.Home);

            // close the tab that matches the location
            this._browser.TabListMiddleMouseClick += (s, e) =>
                {
                    var tabControl = s as TabControl;
                    var tabs = tabControl.TabPages;
                    
                    for (var i = 0; i < tabs.Count; i++)
                    {
                        if (!tabControl.GetTabRect(i).Contains(e.Location)) continue;
                        this.CloseTab(this._tabs[i].Name);
                        return;
                    }
                };

            // create a new incognito tab
            this._browser.NewIncognitoTab += (s, e) => this.NewIncognitoTab();

            // create a new tab
            this._browser.NewTab += (s, e) => this.NewTab();

            // change the home field in the config
            this._browser.HomeChanged += (s, e) => this._config.Home = this._tabs[this.CurrentTabIndex].Url();

            // create a new browserpresenter
            this._browser.NewWindow += (s, e) => new BrowserPresenter<T>(
                new Browser(),
                this._history,
                this._favorites,
                this._config,
                this._favicons,
                this._newTabFactory);
            
            // switch to the next tab
            this._browser.NextTab += (s, e) => { this.CurrentTabIndex += 1; this.SwitchToTab(this.CurrentTabIndex); };

            // switch to the previous tab
            this._browser.PrevTab += (s, e) => { this.CurrentTabIndex -= 1; this.SwitchToTab(this.CurrentTabIndex); };

            // push the url from the clicked viewmodel onto the currenly opened tab
            this._browser.HistoryListOpen += (s, e) =>
                this._tabs[this.CurrentTabIndex].Push(this._history.GetViewModel()[e.ClickedIndex].GetUrl());

            // push the url from the clicked viewmodel onto the currenly opened tab
            this._browser.FavoritesListOpen += (s, e) =>
                this._tabs[this.CurrentTabIndex].Push(this._favorites.GetViewModel()[e.ClickedIndex].GetUrl());
            
            this._browser.FavoritesListEdit += this.EditFavoritesEntry;
            this._browser.HistoryListSave += this.SaveFavoriteFromHistory;
        }

        /// <summary>
        /// The save favorite from history.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void SaveFavoriteFromHistory(object sender, HistoryClickEventArgs args)
        {
            var window = new EditFavoritesWindow();
            new EditFavoritesPresenter(window, this._history.GetViewModel()[args.ClickedIndex].GetUrl(), this._favorites);
        }

        /// <summary>
        /// The edit favorites entry.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void EditFavoritesEntry(object sender, FavoritesClickEventArgs args)
        {
            var window = new EditFavoritesWindow();
            new EditFavoritesPresenter(window, this._favorites.GetViewModel()[args.ClickedIndex].GetUrl(), this._favorites);
        }

        /// <summary>
        /// The bind data function.
        /// </summary>
        private void BindData()
        {
            // set the favicon favicons up
            this._browser.BindImageList(this._favicons.Favicons);

            // Bind the history and favorites
            this._browser.BindHistory(this._history.GetViewModel());
            this._browser.BindFavorites(this._favorites.GetViewModel());
        }

        /// <summary>
        /// Returns the form associated with the presenter,
        /// or throws an error if there isn't one.
        /// </summary>
        /// <remarks>
        /// Only used in the Main function to instantiate
        /// the form that is associated with the presenter.
        /// </remarks>
        /// <exception cref="InvalidViewException">
        /// The Presenter's view is not a WinForms form.
        /// </exception>
        /// <returns>
        /// The <see cref="Form"/>.
        /// </returns>
        public Form GetForm()
        {
            if (this._browser is Form form) return form;
            else throw new InvalidViewException("The view is not a WinForms Form.");
        }

        /// <summary>
        /// Closes the tab matching the specified guid.
        /// </summary>
        /// <param name="guid">The GUID of the tab.</param>
        private void CloseTab(string guid)
        {
            // get the inted of the specified guid
            var index = this._tabs.FindIndex(p => p.Name == guid);
            
            // remove the tab at the index
            this._browser.RemoveTab(this[index].Name);

            // remove the tab in the tab list
            this._tabs.RemoveAt(index);

            // close the browser if we have no tabs left
            if (this._tabs.Count == 0)
            {
                this._browser.Close();
                return;
            }

            // set the current tab index to be at most the last tab
            this.CurrentTabIndex = Math.Min(this.CurrentTabIndex, this._tabs.Count - 1);
        }

        /// <summary>
        /// Creates a new incognito tab.
        /// </summary>
        /// <returns>
        /// The <see cref="ITabPresenter"/>.
        /// </returns>
        private ITabPresenter NewIncognitoTab()
        {
            return this.NewTab((IHistory)null);
        }

        /// <summary>
        /// Creates a new tab, and immediately navigates to
        /// the specified <see cref="Url"/>.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="ITabPresenter"/>.
        /// </returns>
        private ITabPresenter NewTab(Url url)
        {
            var tab = this.NewTab();
            tab.Push(url);
            return tab;
        }

        /// <summary>
        /// Creates a new tab.
        /// </summary>
        /// <returns>
        /// The <see cref="ITabPresenter"/>.
        /// </returns>
        private ITabPresenter NewTab()
        {
            return this.NewTab(this._history);
        }

        /// <summary>
        /// Creates a new tab, pushing history to the given IHistory object.
        /// </summary>
        /// <param name="history">
        /// The history.
        /// </param>
        /// <returns>
        /// The <see cref="ITabPresenter"/>.
        /// </returns>
        private ITabPresenter NewTab(IHistory history)
        {
            var tab = new Tab();
            var presenter = this._newTabFactory(tab, this._favorites, this._config, this._favicons, history, new TabHistory(history));
            this._tabs.Add(presenter);
            this._browser.InsertTab(this._tabs.Count - 1, tab); // insert tab
            this.SwitchToTab(this._tabs.Count - 1); // switch to new tab
            return presenter;
        }

        /// <summary>
        /// Switches to the specified tab.
        /// </summary>
        /// <param name="index">The index of the tab.</param>
        private void SwitchToTab(int index)
        {
            this.SwitchToTab(this[index].Name);
        }

        /// <summary>
        /// Switches to the tab identified by the guid.
        /// </summary>
        /// <param name="guid">The guid of the specified tab.</param>
        private void SwitchToTab(string guid)
        {
            var index = this._tabs.FindIndex(p => p.Name == guid); // index in list
            if (index == -1) return; // if the guid doesnt exist, then we cant switch to it

            this.CurrentTabIndex = index; // update the tabindex
            this._browser.SelectTab(guid); // update the tab index on the ui
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this._tabs.GetEnumerator();
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// The invalid view exception.
    /// </summary>
    public class InvalidViewException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Browser.Presenters.InvalidViewException" /> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public InvalidViewException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Browser.Presenters.InvalidViewException" /> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public InvalidViewException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}