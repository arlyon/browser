namespace Browser.Favorites
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;

    using Browser.History;
    using Browser.Presenters;
    using Browser.Requests;

    /// <summary>
    /// The history push event handler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public delegate void FavoritesUpdateEventHandler(object sender, FavoritesUpdateEventArgs args);

    public class FavoritesUpdateEventArgs
    {
        public FavoritesUpdateEventArgs(FavoritesLocation l)
        {
            this.location = l;
        }
        public FavoritesLocation location { get; set; }
    }

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
        /// <returns>
        /// The <see cref="FavoritesLocation"/>.
        /// </returns>
        FavoritesLocation GetOrCreate(Url url);

        /// <summary>
        /// GetViewModel the list of favorites.
        /// </summary>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        BindingList<FavoritesViewModel> GetViewModel();

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        void DeleteById(int id);

        /// <summary>
        /// The update by id.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="getUpdate">
        ///     The get update.
        /// </param>
        void UpdateById(int id, FavoritesLocation getUpdate);

        /// <summary>
        /// The favorites updated.
        /// </summary>
        event FavoritesUpdateEventHandler OnFavoritesUpdated;
    }
}