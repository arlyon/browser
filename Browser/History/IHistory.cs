namespace Browser.History
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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

        /// <summary>
        /// Gets the history list.
        /// </summary>
        /// <returns>
        /// The <see cref="LinkedList"/>.
        /// </returns>
        BindingList<HistoryViewModel> GetViewModel();
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
        public HistoryPushEventArgs(HistoryLocation history)
        {
            this.Change = history;
        }

        /// <summary>
        /// Gets the new historyLocation.
        /// </summary>
        public HistoryLocation Change { get; }
    }
}