namespace Browser.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;

    using global::Browser.Favorites;
    using global::Browser.History;

    /// <inheritdoc cref="Form" />
    /// <summary>
    ///     The main control for the application.
    /// </summary>
    public partial class Browser : Form, IBrowser
    {
        /// <inheritdoc />
        /// <summary>
        ///     Constructor.
        /// </summary>
        public Browser()
        {
            this.InitializeComponent();
            this._recentlySelectedFavorite = -1;
            this._recentlySelectedHistory = -1;
        }

        /// <summary>
        /// The _recently selected.
        /// </summary>
        private int _recentlySelectedFavorite;

        /// <summary>
        /// The _recently selected history.
        /// </summary>
        private int _recentlySelectedHistory;

        /// <summary>
        ///     Inserts a tab at the specified index, or if out of range and the start or end.
        /// </summary>
        /// <param name="index">The index to insert it at.</param>
        /// <param name="tab">The TabPage to insert.</param>
        public void InsertTab(int index, TabPage tab)
        {
            index = Math.Min(index, this.Tabs.TabPages.Count); // allow insertion at the end of the history
            index = Math.Max(index, 0);

            this.Tabs.TabPages.Insert(index, tab);
            tab.TabIndex = index;
        }

        /// <summary>
        ///     Removes tab by the guid.
        /// </summary>
        /// <param name="guid">The GUID of the tab you want to remove.</param>
        public void RemoveTab(string guid)
        {
            try
            {
                this.Tabs.TabPages.RemoveByKey(guid);
            }
            catch
            {
                // tab doesnt exist
            }
        }

        /// <summary>
        ///     Selects the tab by the guid.
        /// </summary>
        /// <param name="guid">The GUID of the tab you want to select.</param>
        public void SelectTab(string guid)
        {
            this.Tabs.SelectedIndex = this.Tabs.TabPages.Cast<TabPage>().First(p => p.Name == guid).TabIndex;
        }

        /// <summary>
        ///     Sets the image history of the browser to the specified history.
        /// </summary>
        /// <param name="list">The imagelist you want to set.</param>
        public void BindImageList(ImageList list)
        {
            this.Favicons = list;
            this.Tabs.ImageList = this.Favicons;
            this.History.SmallImageList = this.Favicons;
            this.Favorites.SmallImageList = this.Favicons;
        }

        /// <inheritdoc />
        /// <summary>
        /// The bind favorites.
        /// </summary>
        /// <param name="favorites">
        ///     The favorites.
        /// </param>
        public void BindFavorites(BindingList<FavoritesViewModel> favorites)
        {
            this.Favorites.DataSource = favorites;
        }

        /// <inheritdoc />
        /// <summary>
        /// The bind history.
        /// </summary>
        /// <param name="history">
        ///     The history.
        /// </param>
        public void BindHistory(BindingList<HistoryViewModel> history)
        {
            this.History.DataSource = history;
        }

        #region Events
        
        /// <summary>
        /// The close tab event.
        /// </summary>
        public event EventHandler CloseTab;

        /// <summary>
        /// The close window event.
        /// </summary>
        public event EventHandler CloseWindow;

        /// <summary>
        /// The favorites list open event.
        /// </summary>
        public event FavoritesListEventHandler FavoritesListOpen;

        /// <summary>
        /// The favorites list edit event.
        /// </summary>
        public event FavoritesListEventHandler FavoritesListEdit;

        /// <summary>
        /// The history list open event.
        /// </summary>
        public event HistoryListEventHandler HistoryListOpen;

        /// <summary>
        /// The history list save event.
        /// </summary>
        public event HistoryListEventHandler HistoryListSave;

        /// <summary>
        /// The home changed event.
        /// </summary>
        public event EventHandler HomeChanged;

        /// <summary>
        /// The middle mouse click event.
        /// </summary>
        public event MouseEventHandler TabListMiddleMouseClick;

        /// <summary>
        /// The new incognito tab event.
        /// </summary>
        public event EventHandler NewIncognitoTab;

        /// <summary>
        /// The new tab event.
        /// </summary>
        public event EventHandler NewTab;

        /// <summary>
        /// The new window event.
        /// </summary>
        public event EventHandler NewWindow;

        /// <summary>
        /// The next tab event.
        /// </summary>
        public event EventHandler NextTab;

        /// <summary>
        /// The prev tab event.
        /// </summary>
        public event EventHandler PrevTab;

        /// <summary>
        /// The reload tab event.
        /// </summary>
        public event EventHandler ReloadTab;

        /// <summary>
        /// The tab changed event.
        /// </summary>
        public event TabChangedEventHandler TabChanged;

        /// <summary>
        /// The go home event.
        /// </summary>
        public event EventHandler GoHome;

        /// <summary>
        /// The close tab button pressed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnCloseTabButtonPressed(object sender, EventArgs e)
        {
            if (sender is TabControl) return;
            this.CloseTab?.Invoke(sender, e);
        }

        /// <summary>
        /// The close window button pressed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnCloseWindowButtonPressed(object sender, EventArgs e)
        {
            this.CloseWindow?.Invoke(sender, e);
        }

        /// <summary>
        /// The go home button pressed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnGoHomeButtonPressed(object sender, EventArgs e)
        {
            this.GoHome?.Invoke(sender, e);
        }

        /// <summary>
        /// The new incognito tab button pressed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnNewIncognitoTabButtonPressed(object sender, EventArgs e)
        {
            this.NewIncognitoTab?.Invoke(sender, e);
        }

        /// <summary>
        /// The new tab button pressed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnNewTabButtonPressed(object sender, EventArgs e)
        {
            this.NewTab?.Invoke(sender, e);
        }

        /// <summary>
        /// The new window button pressed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnNewWindowButtonPressed(object sender, EventArgs e)
        {
            this.NewWindow?.Invoke(sender, e);
        }

        /// <summary>
        /// The next tab button pressed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnNextTabButtonPressed(object sender, EventArgs e)
        {
            this.NextTab?.Invoke(sender, e);
        }

        /// <summary>
        /// The on prev tab button pressed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnPrevTabButtonPressed(object sender, EventArgs e)
        {
            this.PrevTab?.Invoke(sender, e);
        }

        /// <summary>
        /// The on reload tab button pressed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnReloadTabButtonPressed(object sender, EventArgs e)
        {
            this.ReloadTab?.Invoke(sender, e);
        }

        /// <summary>
        /// The on selected tab index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnSelectedTabIndexChanged(object sender, EventArgs e)
        {
            this.TabChanged?.Invoke(sender, new TabChangedEventArgs(((TabControl)sender).SelectedIndex));
        }

        /// <summary>
        /// The on set as home button pressed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnSetAsHomeButtonPressed(object sender, EventArgs e)
        {
            this.HomeChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// The on tabs mouse click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnTabsMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Middle) return;
            this.TabListMiddleMouseClick?.Invoke(sender, e);
        }

        /// <summary>
        /// The on favorites list click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnFavoritesListClick(object sender, MouseEventArgs e)
        {
            this._recentlySelectedFavorite = ((BindableListView)sender).SelectedIndices[0];
            if (e.Button != MouseButtons.Right) return;
            this.FavoritesRightClickMenu.Show(this, e.Location, ToolStripDropDownDirection.Right);
            
        }

        /// <summary>
        /// The edit favorites item.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void EditFavoritesItem(object sender, EventArgs e)
        {
            if (this._recentlySelectedFavorite == -1) return;
            this.FavoritesListEdit?.Invoke(sender, new FavoritesClickEventArgs(this._recentlySelectedFavorite));
        }

        /// <summary>
        /// The open favorites item.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OpenFavoritesItem(object sender, EventArgs e)
        {
            if (this._recentlySelectedFavorite == -1) return;
            this.FavoritesListOpen?.Invoke(sender, new FavoritesClickEventArgs(this._recentlySelectedFavorite));
        }

        /// <summary>
        /// The on history history double clicked.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnHistoryListDoubleClicked(object sender, MouseEventArgs e)
        {
            this.HistoryListOpen?.Invoke(sender, new HistoryClickEventArgs(this._recentlySelectedHistory));
        }

        /// <summary>
        /// The favorites double clicked.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnFavoritesListDoubleClicked(object sender, MouseEventArgs e)
        {
            if (this._recentlySelectedFavorite == -1) return;
            this.FavoritesListOpen?.Invoke(sender, new FavoritesClickEventArgs(this._recentlySelectedFavorite));
        }

        /// <summary>
        /// The open history item.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OpenHistoryItem(object sender, EventArgs e)
        {
            if (this._recentlySelectedHistory == -1) return;
            this.HistoryListOpen?.Invoke(sender, new HistoryClickEventArgs(this._recentlySelectedHistory));
        }

        /// <summary>
        /// The save history item.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SaveHistoryItem(object sender, EventArgs e)
        {
            if (this._recentlySelectedHistory == -1) return;
            this.HistoryListSave?.Invoke(
                sender,
                new HistoryClickEventArgs(this._recentlySelectedHistory));

        }

        /// <summary>
        /// The on history list click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnHistoryListClick(object sender, MouseEventArgs e)
        {
            this._recentlySelectedHistory = ((BindableListView)sender).SelectedIndices[0];
            if (e.Button != MouseButtons.Right) return;
            this.HistoryRightClickMenu.Show(this, e.Location, ToolStripDropDownDirection.Right);
        }
        
        #endregion
    }
}