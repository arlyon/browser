namespace Browser.Favorites
{
    using Browser.Presenters;
    using Browser.Requests;

    /// <inheritdoc />
    /// <summary>
    /// The SQLiteFavorites interface.
    /// </summary>
    public interface IFavorites
    {
        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        void Add(Url url);
    }
}