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
            using (var conn = new SQLiteConnection("data.db"))
            {
                string statement =
                    $"INSERT INTO `History` (Id, Date, Url, Title) values ({historyLocation.ID}, {historyLocation.Date}, {historyLocation.Url}, {historyLocation.Title}";
                SQLiteCommand insert = new SQLiteCommand(statement, conn);
                insert.ExecuteNonQuery();

                this._history.AddLast(historyLocation);
                this.HistoryUpdated?.Invoke(this, new HistoryPushEventArgs(this._history));
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// The save.
        /// </summary>
        public async void Save()
        {
            using (var conn = new SQLiteConnection("data.db"))
            {
                await conn.SaveChangesAsync();
            }
        }

        /// <summary>
        /// The load.
        /// </summary>
        private void Load()
        {
            using (var conn = new SQLiteConnection("data.db"))
            {
                var test = new LinkedList<HistoryLocation>(conn.History);
                this._history = new LinkedList<HistoryLocation>(conn.History.OrderBy(location => location.Date));
            }
        }
    }
}