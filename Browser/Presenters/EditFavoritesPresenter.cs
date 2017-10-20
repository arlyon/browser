namespace Browser.Presenters
{
    using System;
    using System.Threading.Tasks;

    using Browser.Favorites;
    using Browser.History;
    using Browser.Requests;
    using Browser.Views;

    /// <summary>
    /// The favorites presenter.
    /// </summary>
    internal class EditFavoritesPresenter
    {
        /// <summary>
        /// The favorites window.
        /// </summary>
        private IEditFavorites _favoritesWindow;

        /// <summary>
        /// The location being edited.
        /// </summary>
        private FavoritesLocation _location;

        /// <summary>
        /// The _favorites.
        /// </summary>
        private IFavorites _favorites;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditFavoritesPresenter"/> class.
        /// If the location exists in the favorites menu, it lets you edit the already
        /// existing one otherise it will create a new entry.
        /// </summary>
        /// <param name="window">
        /// The window.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="favorites">
        /// The favorites.
        /// </param>
        public EditFavoritesPresenter(IEditFavorites window, Url url, IFavorites favorites)
        {
            this.SetUp(window, url, favorites);
        }

        /// <summary>
        /// The set up.
        /// </summary>
        /// <param name="window">
        /// The window.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="favorites">
        /// The favorites.
        /// </param>
        private void SetUp(IEditFavorites window, Url url, IFavorites favorites)
        {
            this._favoritesWindow = window;
            this._favorites = favorites;
            this._location = this._favorites.GetOrCreate(url);

            this._favoritesWindow.SaveButtonClicked += this.Save;
            this._favoritesWindow.DeleteButtonClicked += this.Delete;
            this._favoritesWindow.CancelButtonClicked += this.Cancel;

            this._favoritesWindow.DisplayFavoritesLocation(this._location.Name, this._location.Url.ToString());
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Delete(object sender, EventArgs e)
        {
            this._favorites.DeleteById(this._location.Id);
            this._favoritesWindow.Close();
        }

        /// <summary>
        /// The cancel.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Cancel(object sender, EventArgs e)
        {
            this._favoritesWindow.Close();
        }

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Save(object sender, EventArgs e)
        {;
            this._favorites.UpdateById(this._location.Id, this._favoritesWindow.GetUpdate());
            this._favoritesWindow.Close();
        }
    }
}
