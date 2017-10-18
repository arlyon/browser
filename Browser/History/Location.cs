namespace Browser.History
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Browser.Requests;

    /// <inheritdoc />
    /// <summary>
    /// The location.
    /// </summary>
    public class Location : INotifyPropertyChanged
    {
        private int _faviconId;

        /// <summary>
        /// The title.
        /// </summary>
        private string _title;

        /// <summary>
        /// The url.
        /// </summary>
        private Url _url;

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        public Location(string title, Url url)
        {
            this._title = title;
            this._url = url;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        public Location(Url url)
        {
            this._url = url;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        public Location()
        {
        }

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The display text.
        /// </summary>
        public string DisplayText => string.IsNullOrEmpty(this.Title) ? this.Url.Host : this.Title;

        /// <summary>
        /// Gets or sets the favicon id.
        /// </summary>
        public int FaviconId
        {
            get => this._faviconId;
            set
            {
                this._faviconId = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get => this._title;
            set
            {
                this._title = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the url.
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

        /// <summary>
        /// Called when any properties change to notify listening event handlers.
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