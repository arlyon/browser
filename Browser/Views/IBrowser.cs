namespace Browser.Views
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    
    using global::Browser.History;

    /// <summary>
    /// The Browser interface.
    /// </summary>
    public interface IBrowser
    {
        /// <summary>
        /// The browser closed.
        /// </summary>
        event EventHandler BrowserClosed;

        /// <summary>
        /// The close tab.
        /// </summary>
        event EventHandler CloseTab;

        /// <summary>
        /// The close window.
        /// </summary>
        event EventHandler CloseWindow;

        /// <summary>
        /// The favorites double click.
        /// </summary>
        event FavoritesDoubleClickEventHandler FavoritesDoubleClick;

        /// <summary>
        /// The favorites menu edit click.
        /// </summary>
        event FavoritesDoubleClickEventHandler FavoritesMenuEditClick;

        /// <summary>
        /// The favorites menu open click.
        /// </summary>
        event FavoritesDoubleClickEventHandler FavoritesMenuOpenClick;

        /// <summary>
        /// The go home.
        /// </summary>
        event EventHandler GoHome;

        /// <summary>
        /// The history double click.
        /// </summary>
        event HistoryDoubleClickEventHandler HistoryDoubleClick;

        /// <summary>
        /// The history menu save to favorites click.
        /// </summary>
        event HistoryDoubleClickEventHandler HistoryMenuSaveToFavoritesClick;

        /// <summary>
        /// The home changed.
        /// </summary>
        event EventHandler HomeChanged;

        /// <summary>
        /// The middle mouse click.
        /// </summary>
        event MouseEventHandler MiddleMouseClick;

        /// <summary>
        /// The new incognito tab.
        /// </summary>
        event EventHandler NewIncognitoTab;

        /// <summary>
        /// The new tab.
        /// </summary>
        event EventHandler NewTab;

        /// <summary>
        /// The new window.
        /// </summary>
        event EventHandler NewWindow;

        /// <summary>
        /// The next tab.
        /// </summary>
        event EventHandler NextTab;

        /// <summary>
        /// The prev tab.
        /// </summary>
        event EventHandler PrevTab;

        /// <summary>
        /// The reload tab.
        /// </summary>
        event EventHandler ReloadTab;

        /// <summary>
        /// The tab changed.
        /// </summary>
        event EventHandler TabChanged;

        /// <summary>
        /// Tells the browser to close the window.
        /// </summary>
        void Close();

        /// <summary>
        /// Inserts a tab at the specified index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="tab">
        /// The tab.
        /// </param>
        void InsertTab(int index, TabPage tab);

        /// <summary>
        /// Removes a tab by guid.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        void RemoveTab(string guid);

        /// <summary>
        /// Selects a tab by guid.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        void SelectTab(string guid);

        /// <summary>
        /// Sets the image history.
        /// </summary>
        /// <param name="list">
        /// The history.
        /// </param>
        void SetImageList(ImageList list);

        /// <summary>
        /// Tells the browser to show the window.
        /// </summary>
        void Show();

        /// <summary>
        /// The write favorite.
        /// </summary>
        /// <param name="favorites">
        ///     The favorites.
        /// </param>
        void BindFavorites(BindingList<FavoritesViewModel> favorites);

        /// <summary>
        /// Writes a history of locations to the history.
        /// </summary>
        /// <param name="history">
        ///     The history of locations to write.
        /// </param>
        void BindHistory(BindingList<HistoryViewModel> history);
    }

    /// <summary>
    /// The history click event handler. Called when a history element is clicked on.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public delegate void HistoryDoubleClickEventHandler(object sender, HistoryClickEventArgs historyClickEventArgs);

    /// <summary>
    /// The history click event args.
    /// </summary>
    public class HistoryClickEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryClickEventArgs"/> class.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        public HistoryClickEventArgs(int index)
        {
            this.ClickedIndex = index;
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public int ClickedIndex { get; set; }
    }

    /// <summary>
    /// The favorites click event h andler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public delegate void FavoritesDoubleClickEventHandler(object sender, FavoritesClickEventArgs args);

    /// <summary>
    /// The favorites click event args.
    /// </summary>
    public class FavoritesClickEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritesClickEventArgs"/> class.
        /// </summary>
        /// <param name="favoritesSelectedIndex">
        /// The favorites selected index.
        /// </param>
        public FavoritesClickEventArgs(int favoritesSelectedIndex)
        {
            this.ClickedIndex = favoritesSelectedIndex;
        }

        /// <summary>
        /// Gets or sets the selected index.
        /// </summary>
        public int ClickedIndex { get; set; }
    }
}