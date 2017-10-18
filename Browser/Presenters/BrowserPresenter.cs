namespace Browser.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Browser.Cache;
    using Browser.Config;
    using Browser.Favorites;
    using Browser.History;
    using Browser.Requests;
    using Browser.Views;

    /// <summary>
    ///     The primary class for the browser. Handles the tabs, history and favorites.
    /// </summary>
    internal class BrowserPresenter
    {
        private readonly IBrowser _browser; // view

        private readonly IConfig _config; // model

        private readonly IFaviconCache _favicons; // model

        private readonly IFavorites _favorites; // model

        private readonly IHistory _history; // model

        private readonly List<TabPresenter> _tabs;

        private int _currentTabIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserPresenter"/> class.
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
        public BrowserPresenter(
            IBrowser browser,
            IHistory history,
            IFavorites favorites,
            IConfig config,
            IFaviconCache favicons)
        {
            this._browser = browser;
            this._history = history;
            this._favorites = favorites;
            this._favicons = favicons;
            this._config = config;

            this._tabs = new List<TabPresenter>();

            // set the favicon favicons up
            this._browser.SetImageList(this._favicons.Favicons);

            // add event handlers
            this._browser.CloseTab += this.CloseTab;
            this._browser.CloseWindow += this.CloseWindow;
            this._browser.MiddleMouseClick += this.MiddleMouseClick;
            this._browser.NewTab += this.NewTab;
            this._browser.NewWindow += this.NewWindow;
            this._browser.NextTab += this.NextTab;
            this._browser.PrevTab += this.PrevTab;
            this._browser.TabChanged += this.ChangeTab;
            this._browser.NewIncognitoTab += this.NewIncognitoTab;
            this._browser.HomeChanged += this.SetNewHome;
            this._browser.HistoryDoubleClick += this.PushFromHistory;
            this._browser.FavoritesDoubleClick += null;
            this._browser.BrowserClosed += this.SaveChanges;
            this._browser.ReloadTab += this.ReloadTab;

            // set history data source
            this._history.HistoryUpdated += this.UpdateHistory;

            // show window
            this._browser.Show();

            // create a new tab
            this.NewTab(this._config.Home);
        }

        /// <summary>
        /// The get form.
        /// </summary>
        /// <returns>
        /// The <see cref="Form"/>.
        /// </returns>
        public Form GetForm()
        {
            return (Form)this._browser;
        }

        /// <summary>
        ///     Changes the current tab index to match the index selected on the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeTab(object sender, EventArgs e)
        {
            this.SwitchToTab(((TabControl)sender).SelectedIndex);
        }

        /// <summary>
        ///     Accepts the close tab event and closes the current tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseTab(object sender, EventArgs e)
        {
            this.CloseTab(this._tabs[this._currentTabIndex].Name);
        }

        /// <summary>
        ///     Closes the tab matching the specified guid.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        private void CloseTab(string guid)
        {
            var index = this._tabs.FindIndex(p => p.Name == guid);
            this._browser.RemoveTab(this._tabs[index].Name);
            this._tabs.RemoveAt(index);
            if (this._tabs.Count == 0) this._browser.Close();
        }

        /// <summary>
        ///     Handles the close window event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void CloseWindow(object sender, EventArgs e)
        {
            this._browser.Close();
        }

        /// <summary>
        ///     Finds the tab under the middle mouse click and closes it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiddleMouseClick(object sender, MouseEventArgs e)
        {
            foreach (var tab in this._tabs)
            {
                if (!tab.Contains(e.Location)) continue;
                this.CloseTab(tab.Name);
                return;
            }
        }

        /// <summary>
        ///     Calls the new incognito tab function when the event is fired.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewIncognitoTab(object sender, EventArgs e)
        {
            this.NewIncognitoTab();
        }

        /// <summary>
        ///     Creates a new incognito tab.
        /// </summary>
        private TabPresenter NewIncognitoTab()
        {
            var tab = new Tab();
            var presenter = new IncognitoTabPresenter(tab, this._favorites, this._config, this._favicons);
            this._tabs.Add(presenter);
            this._browser.InsertTab(this._tabs.Count - 1, tab); // insert tab
            this.SwitchToTab(this._tabs.Count - 1); // switch to new tab
            return presenter;
        }

        /// <summary>
        ///     Calls the new tab function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewTab(object sender, EventArgs e)
        {
            this.NewTab();
        }

        /// <summary>
        /// Creates a new tab.
        /// </summary>
        /// <returns>
        /// The <see cref="TabPresenter"/>.
        /// </returns>
        private TabPresenter NewTab()
        {
            var tab = new Tab();
            var presenter = new TabPresenter(tab, this._favorites, this._config, this._favicons, this._history);
            this._tabs.Add(presenter);
            this._browser.InsertTab(this._tabs.Count - 1, tab); // insert tab
            this.SwitchToTab(this._tabs.Count - 1); // switch to new tab
            return presenter;
        }

        /// <summary>
        /// Creates a new tab.
        /// </summary>
        /// <param name="url">
        ///     The url.
        /// </param>
        private void NewTab(Url url)
        {
            var tab = this.NewTab();
            tab.Push(url);
            return;
        }

        /// <summary>
        ///     Creates a new browser presenter, with the same history and favorites bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewWindow(object sender, EventArgs e)
        {
            new BrowserPresenter(new Browser(), this._history, this._favorites, this._config, this._favicons);
        }

        /// <summary>
        ///     Increment the tab index.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextTab(object sender, EventArgs e)
        {
            this.SwitchToTab(++this._currentTabIndex);
        }

        /// <summary>
        ///     Decrement the tab index.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrevTab(object sender, EventArgs e)
        {
            this.SwitchToTab(--this._currentTabIndex);
        }

        private void PushFromHistory(object sender, HistoryClickEventArgs e)
        {
            this._tabs[this._currentTabIndex].Push(e.Location.Url);
        }

        private void ReloadTab(object sender, EventArgs e)
        {
            this._tabs[this._currentTabIndex].Reload();
        }

        /// <summary>
        ///     Saves everything and closes the window.
        /// </summary>
        private void SaveChanges(object sender, EventArgs e)
        {
            this._history.Save();
            this._favorites.Save();
            this._config.Save();
        }

        private void SetNewHome(object sender, EventArgs e)
        {
            this._config.Home = this._tabs[this._currentTabIndex].Url();
        }

        /// <summary>
        ///     Switches to the specified tab.
        /// </summary>
        /// <param name="index"></param>
        private void SwitchToTab(int index)
        {
            try
            {
                index = index % this._tabs.Count;
            }
            catch
            {
                // 0 tabs open, window will close
            }

            this.SwitchToTab(this._tabs[index].Name);
        }

        /// <summary>
        ///     Switches to the tab identified by the guid.
        /// </summary>
        /// <param name="guid"></param>
        private void SwitchToTab(string guid)
        {
            var index = this._tabs.FindIndex(p => p.Name == guid); // index in list
            if (index == -1) return; // if the guid doesnt exist, then we cant switch to it

            this._currentTabIndex = index; // update the tabindex
            this._browser.SelectTab(guid); // update the tab index on the ui
        }

        /// <summary>
        /// Updates the history list box.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void UpdateHistory(object sender, HistoryPushEventArgs args)
        {
            this._browser.WriteHistory(args.Change);
        }
    }
}