namespace Browser.History
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Browser.Annotations;
    using Browser.Config;
    using Browser.Requests;

    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    /// <summary>
    /// The history class.
    /// </summary>
    public class History : IHistory
    {
        /// <summary>
        /// The History.
        /// </summary>
        private LinkedList<HistoryLocation> _history;

        /// <summary>
        /// The History view model.
        /// </summary>
        private BindingList<HistoryViewModel> _historyViewModel;

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
            this.HistoryUpdated += this.AddToHistory;
        }

        /// <summary>
        /// The add to history.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void AddToHistory(object sender, HistoryPushEventArgs args)
        {
            this._history.AddLast(args.Change);
            this._historyViewModel.Insert(0, HistoryViewModel.FromHistoryLocation(args.Change));
            args.Change.PropertyChanged += this.SaveChanges;
        }

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SaveChanges(object sender, PropertyChangedEventArgs e)
        {
            HistoryLocation updated = (HistoryLocation)sender;

            using (var db = new DataContext())
            {
                try
                {

                    var entity = db.History.Single(loc => loc.Date == updated.Date);
                    entity.Title = updated.Title;
                    db.SaveChanges();
                }
                catch
                {
                    //FUCK
                }
            }
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
        public void Push(HistoryLocation historyLocation)
        {
            using (var db = new DataContext())
            {
                try
                {
                    // try to find the matching url by id or by hash code
                    var foundurl = historyLocation.Url.Id != 0 ? 
                        db.Urls.Single(url => url.Id == historyLocation.Url.Id) : 
                        db.Urls.FirstOrDefault(url => url.HashCode == historyLocation.Url.HashCode);

                    historyLocation.Url = foundurl ?? historyLocation.Url;
                }
                catch
                {
                    // wait
                }
               

                db.History.Add(historyLocation);
                db.SaveChanges();
            }

            // update memory history and report update in view model
            this.HistoryUpdated?.Invoke(this, new HistoryPushEventArgs(historyLocation));
        }



        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="LinkedList"/>.
        /// </returns>
        public BindingList<HistoryViewModel> GetViewModel()
        {
            return this._historyViewModel;
        }

        /// <summary>
        /// The load function.
        /// </summary>
        private void Load()
        {
            using (var db = new DataContext())
            {
                // do not track the query, and load either all or all in x hours based on config
                IQueryable<HistoryLocation> query = db.History.AsNoTracking()
                    .Where(history => !this._config.LoadAllHistory || DateTime.Now < history.Date.Add(this._config.HistoryTimeSpan))
                    .Include(history => history.Url);

                // create history list
                this._history = new LinkedList<HistoryLocation>(query);

                // create view model
                this._historyViewModel = new BindingList<HistoryViewModel>(query
                    .AsEnumerable()
                    .Select(HistoryViewModel.FromHistoryLocation)
                    .Reverse()
                    .ToList());
            }

        }
    }
}