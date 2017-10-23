namespace Browser.Presenters
{
    using Browser.Favorites;
    using Browser.Requests;
    using Browser.Views;

    /// <summary>
    /// The favorites presenter.
    /// </summary>
    public class EditFavoritesPresenter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditFavoritesPresenter"/> class.
        /// </summary>
        /// <param name="window">
        /// The <see cref="IEditFavorites"/> window.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="favorites">
        /// The favorites.
        /// </param>
        /// <remarks>
        /// If the Location exists in the favorites menu, it lets you edit the already
        /// existing one otherise it will create a new entry.
        /// </remarks>
        public EditFavoritesPresenter(IEditFavorites window, Url url, IFavorites favorites)
        {
            var location = favorites.GetOrCreate(url);

            // When the save button is clicked, update the entry in favorites and close the window.
            window.SaveButtonClicked += (s, e) => { favorites.UpdateById(location.Id, window.GetUpdate()); window.Close(); };

            // When the delete button is clicked, delete the entry in favorites and close the window.
            window.DeleteButtonClicked += (s, e) => { favorites.DeleteById(location.Id); window.Close(); };

            // When the cancel button is clicked, just close the window.
            window.CancelButtonClicked += (s, e) => window.Close();

            window.DisplayFavoritesLocation(location.Name, location.Url.ToString());

            window.Show();
        }
    }
}
