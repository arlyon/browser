namespace Browser.Tests.History
{
    using System.Collections.Generic;

    using Browser.History;
    using Browser.Requests;

    /// <summary>
    /// The mock tab history.
    /// </summary>
    public class MockTabHistory : ITabHistory
    {
        /// <summary>
        /// The _history.
        /// </summary>
        private readonly LinkedList<HistoryLocation> _history;

        /// <summary>
        /// The _current location.
        /// </summary>
        private LinkedListNode<HistoryLocation> _currentLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockTabHistory"/> class.
        /// </summary>
        public MockTabHistory()
        {
            this._history = new LinkedList<HistoryLocation>();
        }

        /// <summary>
        /// The back.
        /// </summary>
        /// <returns>
        /// The <see cref="HistoryLocation"/>.
        /// </returns>
        public HistoryLocation Back()
        {
            if (!this.CanGoBackward()) return null;
            else this._currentLocation = this._currentLocation.Previous;
            this.OnCurrentLocationChange?.Invoke(this, new CurrentLocationChangeArgs(ChangeType.Backward, this._currentLocation?.Value));
            return this._currentLocation?.Value;
        }

        /// <summary>
        /// The can add to favorites.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CanAddToFavorites()
        {
            return this._currentLocation != null;
        }

        /// <summary>
        /// The can go backward.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CanGoBackward()
        {
            return this._currentLocation?.Previous != null;
        }

        /// <summary>
        /// The can go forward.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CanGoForward()
        {
            return this._currentLocation?.Next != null;
        }

        /// <summary>
        /// The can reload.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CanReload()
        {
            return this._currentLocation != null;
        }

        /// <summary>
        /// The current.
        /// </summary>
        /// <returns>
        /// The <see cref="HistoryLocation"/>.
        /// </returns>
        public HistoryLocation Current()
        {
            return this._currentLocation?.Value;
        }

        /// <summary>
        /// The forward.
        /// </summary>
        /// <returns>
        /// The <see cref="HistoryLocation"/>.
        /// </returns>
        public HistoryLocation Forward()
        {
            if (!this.CanGoForward()) return null;
            else this._currentLocation = this._currentLocation.Next;
            this.OnCurrentLocationChange?.Invoke(this, new CurrentLocationChangeArgs(ChangeType.Forward, this._currentLocation?.Value));
            return this._currentLocation?.Value;
        }

        /// <summary>
        /// The push.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="HistoryLocation"/>.
        /// </returns>
        public HistoryLocation Push(Url url)
        {
            while (this._history.Last != this._currentLocation) this._history.RemoveLast();
            this._history.AddLast(new HistoryLocation(url));
            this._currentLocation = this._history.Last;
            this.OnCurrentLocationChange?.Invoke(this, new CurrentLocationChangeArgs(ChangeType.Push, this._currentLocation?.Value));
            return this._currentLocation?.Value;
        }

        /// <summary>
        /// The update title.
        /// </summary>
        /// <param name="pageTitle">
        /// The page title.
        /// </param>
        public void UpdateTitle(string pageTitle)
        {
            this._currentLocation.Value.Title = pageTitle;
        }
        
        /// <summary>
        /// The on current location change.
        /// </summary>
        public event CurrentLocationChangeHandler OnCurrentLocationChange;
    }
}
