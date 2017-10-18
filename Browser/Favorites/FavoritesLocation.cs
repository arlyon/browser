namespace Browser.Favorites
{
    using System;

    using Browser.History;
    using Browser.Requests;

    /// <summary>
    /// The favorites entry.
    /// </summary>
    public class FavoritesLocation
    {
        /// <inheritdoc />
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
            this.Title = url.Host;
            this.ID = Guid.NewGuid();
        }

        /// <inheritdoc />
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
            this.Title = l.Title;
            this.ID = Guid.NewGuid();
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Browser.Favorites.FavoritesLocation" /> class.
        /// </summary>
        public FavoritesLocation()
        {
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public Url Url { get; set; }
    }
}