namespace Browser.History
{
    using System.Collections.Generic;

    /// <summary>
    /// The history.
    /// </summary>
    public class History : IHistory
    {
        /// <summary>
        /// The _history.
        /// </summary>
        private LinkedList<HistoryLocation> _history;

        /// <summary>
        /// Initializes a new instance of the <see cref="History"/> class. 
        /// </summary>
        public History() => this.Load();

        /// <inheritdoc />
        /// <summary>
        /// The history updated.
        /// </summary>
        public event HistoryPushEventHandler HistoryUpdated;

        /// <inheritdoc />
        /// <summary>
        /// The push.
        /// </summary>
        /// <param name="historyLocation">
        /// The historyLocation.
        /// </param>
        public void Push(HistoryLocation historyLocation)
        {
                // TODO push to database
                this._history.AddLast(historyLocation);
                this.HistoryUpdated?.Invoke(this, new HistoryPushEventArgs(this._history));
        }

        /// <summary>
        /// The load.
        /// </summary>
        private void Load()
        {
            // TODO load from database
        }
    }
}