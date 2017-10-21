namespace Browser.History
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Browser.Annotations;
    using Browser.Cache;
    using Browser.Requests;

    /// <summary>
    /// The history view model.
    /// </summary>
    public class HistoryViewModel : INotifyPropertyChanged, IHasFavicon
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the date.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// The _history location.
        /// </summary>
        private HistoryLocation _historyLocation;

        /// <summary>
        /// Prevents a default instance of the <see cref="HistoryViewModel"/> class from being created.
        /// </summary>
        private HistoryViewModel()
        {
        }

        /// <summary>
        /// The from history location.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <param name="faviconLookup">
        /// The favicon Lookup.
        /// </param>
        /// <returns>
        /// The <see cref="HistoryViewModel"/>.
        /// </returns>
        public static HistoryViewModel FromHistoryLocation(HistoryLocation location, IFaviconCache faviconLookup)
        {
            HistoryViewModel hvm = new HistoryViewModel()
                {
                    Name = string.IsNullOrEmpty(location.Title)
                               ? location.Url.Host
                               : location.Title,
                    Date = location.Date,
                    _historyLocation = location,
                    _faviconLookup = faviconLookup
                };

            location.PropertyChanged += hvm.UpdateViewModel;

            return hvm;
        }

        /// <summary>
        /// Gets or sets the _favicon lookup.
        /// </summary>
        private IFaviconCache _faviconLookup;

        /// <summary>
        /// The get favicon id.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public async Task<int> GetFaviconId()
        {
            return await this._faviconLookup.Request(this.GetUrl());
        }

        /// <summary>
        /// The get url.
        /// </summary>
        /// <returns>
        /// The <see cref="Url"/>.
        /// </returns>
        public Url GetUrl()
        {
            return this._historyLocation.Url;
        }

        /// <summary>
        /// The update view model.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void UpdateViewModel(object sender, PropertyChangedEventArgs e)
        {
            this.Name = string.IsNullOrEmpty(this._historyLocation.Title)
                       ? this._historyLocation.Url.Host
                       : this._historyLocation.Title;
            this.Date = this._historyLocation.Date;
            this.OnPropertyChanged();
        }

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
