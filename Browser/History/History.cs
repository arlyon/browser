namespace Browser.History
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Browser.Config;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The history class.
    /// </summary>
    public class History : IHistory
    {
        /// <summary>
        /// The history linked list (since we only really need access to the last element).
        /// </summary>
        private LinkedList<HistoryLocation> _history;

        /// <summary>
        /// The configuration file.
        /// </summary>
        private IConfig _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="History"/> class. 
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public History(IConfig config)
        {
            this._config = config;
            this.Load();
        }

        /// <inheritdoc />
        /// <summary>
        /// The history updated event.
        /// </summary>
        public event HistoryPushEventHandler HistoryUpdated;

        /// <inheritdoc />
        /// <summary>
        /// Pushes a new item to history and to the database.
        /// </summary>
        /// <param name="historyLocation">
        /// The historyLocation.
        /// </param>
        public async void Push(HistoryLocation historyLocation)
        {
            using (var db = new DataContext())
            {
                db.History.Add(historyLocation);
                await db.SaveChangesAsync();
            }
            this._history.AddLast(historyLocation);
            this.HistoryUpdated?.Invoke(this, new HistoryPushEventArgs(this._history));
        }

        /// <summary>
        /// The load function.
        /// </summary>
        private void Load()
        {
            using (var db = new DataContext())
            {
                // do not track the query
                this._history = new LinkedList<HistoryLocation>(
                    db.History.AsNoTracking()
                        .Where(history => this._config.LoadAllHistory ? DateTime.Now < history.Date.Add(this._config.HistoryTimeSpan) : true)
                        .Include(history => history.Url));
            }
        }
    }
}