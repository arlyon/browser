namespace Browser.History
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Browser.Presenters;

    /// <summary>
    /// The history push event handler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public delegate void HistoryPushEventHandler(object sender, HistoryPushEventArgs args);

    /// <inheritdoc />
    /// <summary>
    /// The history interface.
    /// </summary>
    public interface IHistory
    {
        /// <summary>
        /// The history updated.
        /// </summary>
        event HistoryPushEventHandler HistoryUpdated;

        /// <summary>
        /// The push.
        /// </summary>
        /// <param name="historyLocation">
        /// The historyLocation.
        /// </param>
        void Push(HistoryLocation historyLocation);
    }

    /// <inheritdoc />
    /// <summary>
    /// The history push event args.
    /// </summary>
    public class HistoryPushEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Browser.History.HistoryPushEventArgs" /> class.
        /// </summary>
        /// <param name="history">
        ///     The entire history.
        /// </param>
        public HistoryPushEventArgs(LinkedList<HistoryLocation> history)
        {
            this.History = history;
        }

        /// <summary>
        /// Gets the new historyLocation.
        /// </summary>
        public HistoryLocation Change => this.History.Last();

        /// <summary>
        /// Gets the historyLocation.
        /// </summary>
        public LinkedList<HistoryLocation> History { get; }
    }
}