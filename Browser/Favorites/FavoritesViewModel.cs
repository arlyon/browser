namespace Browser.History
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Browser.Annotations;
    using Browser.Cache;
    using Browser.Favorites;
    using Browser.Requests;

    /// <inheritdoc />
    /// <summary>
    /// The history view model.
    /// </summary>
    public class FavoritesViewModel : INotifyPropertyChanged, IHasFavicon
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => this.name;
            set
            {
                this.name = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url
        {
            get => this.url;
            set
            {
                this.url = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// The _history location.
        /// </summary>
        private FavoritesLocation _favoritesLocation;

        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The url.
        /// </summary>
        private string url;

        /// <summary>
        /// The _favicon lookup.
        /// </summary>
        private IFaviconCache _faviconLookup;

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
        public static FavoritesViewModel FromFavoritesLocation(FavoritesLocation location, IFaviconCache faviconLookup)
        {
            FavoritesViewModel hvm = new FavoritesViewModel()
            {
                Name = location.Name,
                Url = location.Url.ToString(),
                _favoritesLocation = location,
                _faviconLookup = faviconLookup
            };

            location.PropertyChanged += hvm.UpdateViewModel;

            return hvm;
        }

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
            return this._favoritesLocation.Url;
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
            this.Name = string.IsNullOrEmpty(this._favoritesLocation.Name)
                       ? this._favoritesLocation.Url.Host
                       : this._favoritesLocation.Name;
            this.Url = this._favoritesLocation.Url.ToString();
            this.OnPropertyChanged();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="FavoritesViewModel"/> class from being created.
        /// </summary>
        private FavoritesViewModel()
        {
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
