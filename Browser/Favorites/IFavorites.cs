namespace Browser.Favorites
{
    using System.ComponentModel;

    using Browser.History;
    using Browser.Requests;

    /// <summary>
    /// The Favorites interface.
    /// </summary>
    public interface IFavorites
    {
        /// <summary>
        /// Gets or creates a new favorites location.
        /// </summary>
        /// <param name="url">
        /// The url to get or create.
        /// </param>
        /// <returns>
        /// The <see cref="FavoritesLocation"/>.
        /// </returns>
        FavoritesLocation GetOrCreate(Url url);

        /// <summary>
        /// Gets a <see cref="BindingList{T}"/> with the viewmodels for the favorites.
        /// </summary>
        /// <returns>
        /// The <see cref="BindingList{T}"/>.
        /// </returns>
        BindingList<FavoritesViewModel> GetViewModel();

        /// <summary>
        /// The delete by id method.
        /// </summary>
        /// <param name="id">
        /// The id to delete.
        /// </param>
        void DeleteById(int id);

        /// <summary>
        /// The update by id method.
        /// </summary>
        /// <param name="id">
        /// The id to update.
        /// </param>
        /// <param name="update">
        /// The new location date to update.
        /// </param>
        void UpdateById(int id, FavoritesLocation update);

        /// <summary>
        /// The favorites updated event.
        /// </summary>
        /// <remarks>
        /// Called when a new <see cref="FavoritesLocation"/> is
        /// added to the favorites or when a favorites location is
        /// updated with new data.
        /// </remarks>
        event FavoritesUpdateEventHandler OnFavoritesAddOrUpdate;
    }

    /// <summary>
    /// The favorites update event handler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public delegate void FavoritesUpdateEventHandler(object sender, FavoritesUpdateEventArgs args);

    /// <summary>
    /// The favorites update event args.
    /// </summary>
    public class FavoritesUpdateEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritesUpdateEventArgs"/> class.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        public FavoritesUpdateEventArgs(FavoritesLocation location)
        {
            this.Location = location;
        }

        /// <summary>
        /// Gets the Location.
        /// </summary>
        public FavoritesLocation Location { get; }
    }
}