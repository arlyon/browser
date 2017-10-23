using System;
using System.Collections.Generic;
using System.Linq;

namespace Browser.Tests.Views
{
    using System.ComponentModel;
    using System.Windows.Forms;

    using Browser.History;
    using Browser.Views;

    /// <summary>
    /// The mock browser view.
    /// </summary>
    public class MockBrowserView : IBrowser
    {
        /// <summary>
        /// The tabs.
        /// </summary>
        private List<string> _tabs;

        /// <summary>
        /// Gets or sets the _selected tab.
        /// </summary>
        public string SelectedTab { get; set; }

        /// <summary>
        /// Gets a value indicating whether is closed.
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockBrowserView"/> class.
        /// </summary>
        public MockBrowserView()
        {
            this._tabs = new List<string>();
        }

        /// <summary>
        /// The show.
        /// </summary>
        public void Show()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// The close.
        /// </summary>
        public void Close()
        {
            this.IsClosed = true;
        }

        /// <summary>
        /// The insert tab.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="tab">
        /// The tab.
        /// </param>
        public void InsertTab(int index, TabPage tab)
        {
            index = Math.Min(index, this._tabs.Count); // allow insertion at the end of the history
            index = Math.Max(index, 0);

            this._tabs.Insert(index, tab.Name);
            this.SelectedTab = tab.Name;
        }

        /// <summary>
        /// The remove tab.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        public void RemoveTab(string guid)
        {
            var index = this._tabs.IndexOf(guid);
            this._tabs.RemoveAt(index);
            this.SelectedTab = this._tabs[Math.Min(index, this._tabs.Count - 1)];
        }

        /// <summary>
        /// The select tab.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        public void SelectTab(string guid)
        {
            this.SelectedTab = guid;
        }

        /// <summary>
        /// The bind image list.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        public void BindImageList(ImageList list)
        {
        }

        /// <summary>
        /// The bind favorites.
        /// </summary>
        /// <param name="favorites">
        /// The favorites.
        /// </param>
        public void BindFavorites(BindingList<FavoritesViewModel> favorites)
        {
        }

        /// <summary>
        /// The bind history.
        /// </summary>
        /// <param name="history">
        /// The history.
        /// </param>
        public void BindHistory(BindingList<HistoryViewModel> history)
        {
        }

        public void InvokeCloseTab()
        {
            this.CloseTab?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CloseTab;

        public void InvokeCloseWindow()
        {
            this.CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CloseWindow;

        public void InvokeFavoritesListOpen(int index)
        {
            this.FavoritesListOpen?.Invoke(this, new FavoritesClickEventArgs(index));
        }

        public event FavoritesListEventHandler FavoritesListOpen;

        public void InvokeFavoritesListEdit(int index)
        {
            this.FavoritesListEdit?.Invoke(this, new FavoritesClickEventArgs(index));
        }

        public event FavoritesListEventHandler FavoritesListEdit;

        public void InvokeHistoryListOpen(int index)
        {
            this.HistoryListOpen?.Invoke(this, new HistoryClickEventArgs(index));
        }

        public event HistoryListEventHandler HistoryListOpen;

        public void InvokeHistoryListSave(int index)
        {
            this.HistoryListSave?.Invoke(this, new HistoryClickEventArgs(index));
        }

        public event HistoryListEventHandler HistoryListSave;

        public void InvokeHomeChanged()
        {
            this.HomeChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler HomeChanged;

        public void InvokeTabListMiddleMouseClick(MouseButtons button)
        {
            this.TabListMiddleMouseClick?.Invoke(this, new MouseEventArgs(button, 1, 0, 0, 0));
        }

        public event MouseEventHandler TabListMiddleMouseClick;

        public void InvokeNewIncognitoTab()
        {
            this.NewIncognitoTab?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler NewIncognitoTab;

        public void InvokeNewTab()
        {
            this.NewTab?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler NewTab;

        public void InvokeNewWindow()
        {
            this.NewWindow?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler NewWindow;

        public void InvokeNextTab()
        {
            this.NextTab?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler NextTab;

        public void InvokePrevTab()
        {
            this.PrevTab?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler PrevTab;

        public void InvokeReloadTab()
        {
            this.ReloadTab?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ReloadTab;

        public void InvokeTabChanged(int index)
        {
            this.TabChanged?.Invoke(this, new TabChangedEventArgs(index));
        }

        public event TabChangedEventHandler TabChanged;

        public void InvokeGoHome()
        {
            this.GoHome?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler GoHome;
    }
}
