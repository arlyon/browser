namespace Browser.Favorites
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    using Browser.Annotations;
    using Browser.History;
    using Browser.Requests;

    /// <inheritdoc />
    /// <summary>
    /// The favorites entry.
    /// </summary>
    public class FavoritesLocation : INotifyPropertyChanged
    {
        /// <summary>
        /// The name.
        /// </summary>
        private string _name;

        /// <summary>
        /// The url.
        /// </summary>
        private Url _url;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Browser.Favorites.FavoritesLocation" /> class.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        public FavoritesLocation(Url url)
        {
            this.Name = url.Host;
            this.Url = url;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Browser.Favorites.FavoritesLocation" /> class. 
        /// </summary>
        /// <param name="l">
        /// The l.
        /// </param>
        public FavoritesLocation(Location l)
        {
            this.Name = l.Url.Host;
            this.Url = l.Url;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Browser.Favorites.FavoritesLocation" /> class.
        /// </summary>
        public FavoritesLocation()
        {
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public Url Url
        {
            get => this._url;
            set
            {
                this._url = value;
                this.OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The on property changed function which is called in the properties
        /// of the class to notify changes to listeners.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}