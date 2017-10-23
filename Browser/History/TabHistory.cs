namespace Browser.History
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Browser.Requests;

    /// <summary>
    /// The history for a single tab.
    /// </summary>
    public class TabHistory : ITabHistory
    {
        /// <summary>
        /// The history chain.
        /// </summary>
        private readonly LinkedList<HistoryLocation> _historyChain;

        /// <summary>
        /// The current page.
        /// </summary>
        private LinkedListNode<HistoryLocation> _currentPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabHistory"/> class.
        /// </summary>
        /// <param name="history">
        /// The history.
        /// </param>
        public TabHistory(IHistory history)
        {
            this._historyChain = new LinkedList<HistoryLocation>();
            this.GlobalHistory = history;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TabHistory"/> class.
        /// </summary>
        /// <param name="historyChain">
        /// Create a tab history with an already existing chain.
        /// </param>
        /// <param name="history">
        /// The history.
        /// </param>
        public TabHistory(LinkedList<HistoryLocation> historyChain, IHistory history)
        {
            this._historyChain = historyChain;
            this.GlobalHistory = history;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Called when the current historyLocation in history changes.
        /// </summary>
        public event CurrentLocationChangeHandler OnCurrentLocationChange;

        /// <summary>
        /// Gets or sets the Global History
        /// </summary>
        protected IHistory GlobalHistory { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Goes backwards a step in the history chain.
        /// </summary>
        /// <returns></returns>
        public HistoryLocation Back()
        {
            Contract.Requires(this.CanGoBackward());
            if (!this.CanGoBackward()) return null;

            this._currentPage = this._currentPage.Previous;

            this.OnCurrentLocationChange?.Invoke(
                this,
                new CurrentLocationChangeArgs(ChangeType.Backward, this._currentPage?.Value));

            return this._currentPage?.Value;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks whether the page can be added to favorites (ie there is a loaded historyLocation).
        /// </summary>
        /// <returns></returns>
        public bool CanAddToFavorites()
        {
            return this._currentPage != null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks if it is possible to go back in history.
        /// </summary>
        /// <returns></returns>
        public bool CanGoBackward()
        {
            return this._currentPage?.Previous != null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks if it is possible to go forwards in history.
        /// </summary>
        /// <returns></returns>
        public bool CanGoForward()
        {
            return this._currentPage?.Next != null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks if it is possible to reload the page.
        /// </summary>
        /// <returns></returns>
        public bool CanReload()
        {
            return this._currentPage != null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Returns the current historyLocation in history.
        /// </summary>
        /// <returns>The current historyLocation is history.</returns>
        public HistoryLocation Current()
        {
            Contract.Requires(this._currentPage?.Value != null);
            return this._currentPage?.Value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Goes forward a step in the history chain.
        /// </summary>
        /// <returns></returns>
        public HistoryLocation Forward()
        {
            Contract.Requires(this.CanGoForward());
            if (!this.CanGoForward()) return null;

            this._currentPage = this._currentPage.Next;

            this.OnCurrentLocationChange?.Invoke(
                this,
                new CurrentLocationChangeArgs(ChangeType.Forward, this._currentPage?.Value));

            return this._currentPage?.Value;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Severs any future elements from the chain
        ///     and pushes a new element to the tab history
        ///     as well as the global <see cref="SqliteHistory" />.
        /// </summary>
        /// <param name="url">The <see cref="Url" /> to navigate to.</param>
        /// <returns>The new historyLocation.</returns>
        public virtual HistoryLocation Push(Url url)
        {
            while (this._historyChain.Last != this._currentPage) this._historyChain.RemoveLast();

            this._historyChain.AddLast(new HistoryLocation(url));
            this._currentPage = this._historyChain.Last;
            this.GlobalHistory?.Push(this._currentPage.Value);

            this.OnCurrentLocationChange?.Invoke(
                this,
                new CurrentLocationChangeArgs(ChangeType.Push, this._currentPage?.Value));

            return this._currentPage.Value;
        }

        /// <summary>
        /// The update title.
        /// </summary>
        /// <param name="pageTitle">
        /// The page title.
        /// </param>
        public void UpdateTitle(string pageTitle)
        {
            this._currentPage.Value.Title = pageTitle;
        }
    }
}