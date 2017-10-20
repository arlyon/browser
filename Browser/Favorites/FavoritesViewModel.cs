namespace Browser.History
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Browser.Annotations;
    using Browser.Favorites;
    using Browser.Requests;

    /// <inheritdoc />
    /// <summary>
    /// The history view model.
    /// </summary>
    public class FavoritesViewModel : INotifyPropertyChanged
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
        /// Prevents a default instance of the <see cref="FavoritesViewModel"/> class from being created.
        /// </summary>
        private FavoritesViewModel()
        {
        }

        /// <summary>
        /// The from history location.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <returns>
        /// The <see cref="HistoryViewModel"/>.
        /// </returns>
        public static FavoritesViewModel FromFavoritesLocation(FavoritesLocation location)
        {
            FavoritesViewModel hvm = new FavoritesViewModel()
            {
                Name = location.Name,
                Url = location.Url.ToString(),
                _favoritesLocation = location
            };

            location.PropertyChanged += hvm.UpdateViewModel;

            return hvm;
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
