namespace Browser.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using global::Browser.Favorites;
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
        event EventHandler FavoritesDoubleClick;

        /// <summary>
        /// The go home.
        /// </summary>
        event EventHandler GoHome;

        /// <summary>
        /// The history double click.
        /// </summary>
        event HistoryClickEventHandler HistoryDoubleClick;

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
        /// Sets the image list.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        void SetImageList(ImageList list);

        /// <summary>
        /// Tells the browser to show the window.
        /// </summary>
        void Show();

        /// <summary>
        /// The write favorite.
        /// </summary>
        /// <param name="favorite">
        /// The favorite.
        /// </param>
        void WriteFavorite(FavoritesLocation favorite);

        /// <summary>
        /// The write favorite.
        /// </summary>
        /// <param name="favorites">
        /// The favorites.
        /// </param>
        void WriteFavorite(List<FavoritesLocation> favorites);

        /// <summary>
        /// Writes a list of locations to the history.
        /// </summary>
        /// <param name="list">
        /// The list of locations to write.
        /// </param>
        void WriteHistory(LinkedList<HistoryLocation> list);

        /// <summary>
        /// Writes a single location to the history.
        /// </summary>
        /// <param name="newLocation">
        /// The new location.
        /// </param>
        void WriteHistory(HistoryLocation newLocation);
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
    public delegate void HistoryClickEventHandler(object sender, HistoryClickEventArgs args);

    /// <summary>
    /// The history click event args.
    /// </summary>
    public class HistoryClickEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryClickEventArgs"/> class.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        public HistoryClickEventArgs(Location location)
        {
            this.Location = location;
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public Location Location { get; set; }
    }
}