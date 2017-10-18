﻿namespace Browser.Views
{
    using System;
    using System.Collections.Generic;
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
        }

        /// <summary>
        ///     The browser closed.
        /// </summary>
        public event EventHandler BrowserClosed;

        /// <summary>
        ///     The close tab event.
        /// </summary>
        public event EventHandler CloseTab;

        /// <summary>
        ///     The close window event.
        /// </summary>
        public event EventHandler CloseWindow;

        /// <summary>
        ///     The favorites double click.
        /// </summary>
        public event EventHandler FavoritesDoubleClick;

        /// <summary>
        ///     The go home.
        /// </summary>
        public event EventHandler GoHome;

        /// <summary>
        ///     The history double click.
        /// </summary>
        public event HistoryClickEventHandler HistoryDoubleClick;

        /// <summary>
        ///     The home changed.
        /// </summary>
        public event EventHandler HomeChanged;

        /// <summary>
        ///     The middle mouse click.
        /// </summary>
        public event MouseEventHandler MiddleMouseClick;

        /// <summary>
        ///     The new incognito tab.
        /// </summary>
        public event EventHandler NewIncognitoTab;

        /// <summary>
        ///     The new tab.
        /// </summary>
        public event EventHandler NewTab;

        /// <summary>
        ///     The new window.
        /// </summary>
        public event EventHandler NewWindow;

        /// <summary>
        ///     The next tab.
        /// </summary>
        public event EventHandler NextTab;

        /// <summary>
        ///     The prev tab.
        /// </summary>
        public event EventHandler PrevTab;

        /// <summary>
        ///     The reload tab.
        /// </summary>
        public event EventHandler ReloadTab;

        /// <summary>
        ///     The tab changed.
        /// </summary>
        public event EventHandler TabChanged;

        /// <summary>
        ///     Inserts a tab at the specified index, or if out of range and the start or end.
        /// </summary>
        /// <param name="index">The index to insert it at.</param>
        /// <param name="tab">The TabPage to insert.</param>
        public void InsertTab(int index, TabPage tab)
        {
            index = Math.Min(index, this.Tabs.TabPages.Count); // allow insertion at the end of the list
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
        ///     Sets the image list of the browser to the specified list.
        /// </summary>
        /// <param name="list">The imagelist you want to set.</param>
        public void SetImageList(ImageList list)
        {
            this.Favicons = list;
            this.Tabs.ImageList = this.Favicons;
            this.History.SmallImageList = this.Favicons;
        }

        /// <summary>
        /// The write favorite.
        /// </summary>
        /// <param name="favorite">
        /// The favorite.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void WriteFavorite(FavoritesLocation favorite)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The write favorite.
        /// </summary>
        /// <param name="favorites">
        /// The favorites.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void WriteFavorite(List<FavoritesLocation> favorites)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The write history.
        /// </summary>
        /// <param name="newLocation">
        /// The new location.
        /// </param>
        public void WriteHistory(HistoryLocation newLocation)
        {
            var listViewItem = new ListViewItem(newLocation.Url.Host);
            listViewItem.SubItems.Add(newLocation.Date.ToString());
            this.History.Items.Insert(0, listViewItem);
        }

        /// <summary>
        /// Asynchronously writes the history to the tab.
        /// </summary>
        /// <param name="list">
        /// The args location.
        /// </param>
        public void WriteHistory(LinkedList<HistoryLocation> list)
        {
            this.History.BeginUpdate();
            var history = new ListView.ListViewItemCollection(this.History);

            foreach (var l in list.OrderBy(location => location.Date))
            {
                // TODO location favicon
                var listViewItem = new ListViewItem(l.Url.Host);
                listViewItem.SubItems.Add(l.Date.ToString());
                history.Insert(0, listViewItem);
            }

            this.History.EndUpdate();
        }

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
        /// The on favorites clicked.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnFavoritesListClicked(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var index = this.Favorites.IndexFromPoint(e.Location);

            if (index != ListBox.NoMatches)
            {
                // ie !-1
            }
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
            this.FavoritesDoubleClick?.Invoke(sender, e);
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
        /// The on history list double clicked.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnHistoryListDoubleClicked(object sender, MouseEventArgs e)
        {
            // TODO this
            // TODO this
            /**
            if (this.History.IndexFromPoint(e.Location) == -1) return;
            var l = (Location)this.History.Items[this.History.IndexFromPoint(e.Location)];
            this.HistoryDoubleClick?.Invoke(sender, new HistoryClickEventArgs(l)); **/
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
            this.TabChanged?.Invoke(sender, e);
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
            this.MiddleMouseClick?.Invoke(sender, e);
        }

        /// <summary>
        /// The browser_ form closed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnWindowClosed(object sender, FormClosedEventArgs e)
        {
            this.BrowserClosed?.Invoke(sender, e);
        }
    }
}