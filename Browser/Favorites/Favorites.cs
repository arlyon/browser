namespace Browser.Favorites
{
    using System.Collections.Generic;

    using Browser.Requests;

    /// <inheritdoc />
    /// <summary>
    /// The favorites.
    /// </summary>
    public class Favorites : IFavorites
    {
        /// <summary>
        /// The _favorites.
        /// </summary>
        private List<FavoritesLocation> _favorites;

        /// <summary>
        /// Initializes a new instance of the <see cref="Favorites"/> class.
        /// </summary>
        public Favorites()
        {
            this._favorites = new List<FavoritesLocation>();
        }

        /// <inheritdoc />
        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        public void Add(Url url)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// The save.
        /// </summary>
        public void Save()
        {
        }
    }
}