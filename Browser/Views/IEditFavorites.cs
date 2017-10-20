namespace Browser.Views
{
    using System;

    using global::Browser.Favorites;

    /// <summary>
    /// The EditFavorites interface.
    /// </summary>
    internal interface IEditFavorites
    {
        /// <summary>
        /// The save button clicked.
        /// </summary>
        event EventHandler SaveButtonClicked;

        /// <summary>
        /// The cancel button clicked.
        /// </summary>
        event EventHandler CancelButtonClicked;

        /// <summary>
        /// The delete button clicked.
        /// </summary>
        event EventHandler DeleteButtonClicked;

        /// <summary>
        /// The display favorites location.
        /// </summary>
        /// <param name="name">The name of the favorite.</param>
        /// <param name="url">The Url of the favorite.</param>
        void DisplayFavoritesLocation(string name, string url);

        /// <summary>
        /// The close.
        /// </summary>
        void Close();

        /// <summary>
        /// The get update.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        FavoritesLocation GetUpdate();
    }
}
