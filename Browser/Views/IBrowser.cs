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
        /// Opens the <see cref="IBrowser"/>.
        /// </summary>
        void Show();

        /// <summary>
        /// Closes the <see cref="IBrowser"/>.
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
        /// Binds the specified image list to the UI.
        /// </summary>
        /// <param name="list">
        /// The images to bind.
        /// </param>
        /// <remarks>
        /// Used for binding the favicons to the UI.
        /// </remarks>
        void BindImageList(ImageList list);

        /// <summary>
        /// Binds a list of <see cref="FavoritesViewModel"/> to the UI.
        /// </summary>
        /// <param name="favorites">
        /// The <see cref="BindingList{T}"/> to bind.
        /// </param>
        void BindFavorites(BindingList<FavoritesViewModel> favorites);

        /// <summary>
        /// Binds a list of <see cref="HistoryViewModel"/> to the UI.
        /// </summary>
        /// <param name="history">
        /// The <see cref="BindingList{T}"/> to bind.
        /// </param>
        void BindHistory(BindingList<HistoryViewModel> history);

        #region Events

        /// <summary>
        /// The close tab event.
        /// </summary>
        event EventHandler CloseTab;

        /// <summary>
        /// The close window event.
        /// </summary>
        event EventHandler CloseWindow;

        /// <summary>
        /// The favorites list open event.
        /// </summary>
        event FavoritesListEventHandler FavoritesListOpen;

        /// <summary>
        /// The favorites list edit event.
        /// </summary>
        event FavoritesListEventHandler FavoritesListEdit;

        /// <summary>
        /// The history list open event.
        /// </summary>
        event HistoryListEventHandler HistoryListOpen;

        /// <summary>
        /// The history list save to favorites event.
        /// </summary>
        event HistoryListEventHandler HistoryListSave;

        /// <summary>
        /// The home changed event.
        /// </summary>
        event EventHandler HomeChanged;

        /// <summary>
        /// The tab list middle mouse click event.
        /// </summary>
        event MouseEventHandler TabListMiddleMouseClick;

        /// <summary>
        /// The new incognito tab event.
        /// </summary>
        event EventHandler NewIncognitoTab;

        /// <summary>
        /// The new tab event.
        /// </summary>
        event EventHandler NewTab;

        /// <summary>
        /// The new window event.
        /// </summary>
        event EventHandler NewWindow;

        /// <summary>
        /// The next tab event.
        /// </summary>
        event EventHandler NextTab;

        /// <summary>
        /// The previous tab event.
        /// </summary>
        event EventHandler PrevTab;

        /// <summary>
        /// The reload tab event.
        /// </summary>
        event EventHandler ReloadTab;

        /// <summary>
        /// The tab changed event.
        /// </summary>
        event TabChangedEventHandler TabChanged;

        /// <summary>
        /// The go home event.
        /// </summary>
        event EventHandler GoHome;

        #endregion

    }

    /// <summary>
    /// The tab changed event handler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public delegate void TabChangedEventHandler(object sender, TabChangedEventArgs args);

    /// <summary>
    /// The tab changed event args.
    /// </summary>
    public class TabChangedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabChangedEventArgs"/> class.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        public TabChangedEventArgs(int index)
        {
            this.SelectedIndex = index;
        }

        /// <summary>
        /// Gets the selected index.
        /// </summary>
        public int SelectedIndex { get; }
    }

    /// <summary>
    /// The history double click event handler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="historyClickEventArgs">
    /// The history click event args.
    /// </param>
    public delegate void HistoryListEventHandler(object sender, HistoryClickEventArgs historyClickEventArgs);

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
        /// Gets the index.
        /// </summary>
        public int ClickedIndex { get; }
    }

    /// <summary>
    /// The favorites double click event handler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public delegate void FavoritesListEventHandler(object sender, FavoritesClickEventArgs args);

    /// <inheritdoc />
    /// <summary>
    /// The favorites click event args.
    /// </summary>
    public class FavoritesClickEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritesClickEventArgs" /> class.
        /// </summary>
        /// <param name="favoritesSelectedIndex">
        /// The favorites selected index.
        /// </param>
        public FavoritesClickEventArgs(int favoritesSelectedIndex)
        {
            this.ClickedIndex = favoritesSelectedIndex;
        }

        /// <summary>
        /// Gets the selected index.
        /// </summary>
        public int ClickedIndex { get; }
    }
}