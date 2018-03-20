namespace Browser.Favorites
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using Browser.Config;
    using Browser.Favicon;
    using Browser.History;
    using Browser.Requests;

    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    /// <summary>
    /// The favorites sqlite database implementation.
    /// </summary>
    public class SqliteFavorites : IFavorites
    {
        /// <summary>
        /// The favicon cache.
        /// </summary>
        private readonly IFavicon _faviconCache;

        /// <summary>
        /// The favorites dictionary.
        /// </summary>
        private Dictionary<int, FavoritesLocation> _favorites;

        /// <summary>
        /// The favorites view models.
        /// </summary>
        private BindingList<FavoritesViewModel> _favoritesViewModels;

        /// <summary>
        /// The configuration file.
        /// </summary>
        private IConfig _config;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteFavorites"/> class.
        /// </summary>
        /// <param name="config">
        /// The config file.
        /// </param>
        /// <param name="cache">
        /// The cache.
        /// </param>
        public SqliteFavorites(IConfig config, IFavicon cache)
        {
            this._config = config;
            this._faviconCache = cache;
            this.Load();
            this.OnFavoritesAddOrUpdate += this.UpdateFavorites;
        }

        /// <summary>
        /// Updates the item in memory with the same id with new data.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void UpdateFavorites(object sender, FavoritesUpdateEventArgs args)
        {
            // update favorites
            this._favorites[args.Location.Id] = args.Location;

            // update viewmodel
            var toUpdate = this._favoritesViewModels.SingleOrDefault(model => model.Url == args.Location.Url.ToString());

            if (toUpdate == null)
            {
                this._favoritesViewModels.Add(FavoritesViewModel.FromFavoritesLocation(args.Location, this._faviconCache));
            }
            else
            {
                toUpdate.Name = args.Location.Name;
                toUpdate.Url = args.Location.Url.ToString();
            }
        }

        /// <summary>
        /// Loads all the data from the database and into memory.
        /// </summary>
        private void Load()
        {
            using (var db = new DataContext())
            {
                var query = db.Favorites.Include(favorite => favorite.Url);
                this._favorites = query.ToDictionary(entry => entry.Id, entry => entry);
                this._favoritesViewModels = new BindingList<FavoritesViewModel>(
                    query.AsEnumerable().Select(fav => FavoritesViewModel.FromFavoritesLocation(fav, this._faviconCache)).ToList());
            }
        }

        /// <summary>
        /// Adds a url to the favorites list and into the database.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="FavoritesLocation"/>.
        /// </returns>
        private FavoritesLocation Add(Url url)
        {
            var loc = new FavoritesLocation() { Name = url.Host, Url = url };

            using (var db = new DataContext())
            {
                // if url exists in database
                var inDbUrl = url.Id > 0 ? db.Urls.Single(u => u.Id == url.Id) : db.Urls.SingleOrDefault(u => u.HashCode == url.HashCode);
                loc.Url = inDbUrl ?? loc.Url;

                db.Favorites.Add(loc);
                db.SaveChanges();
            }
            
            // push change to lists
            this.OnFavoritesAddOrUpdate?.Invoke(this, new FavoritesUpdateEventArgs(loc));
            return loc;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the favorites view model.
        /// </summary>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        public BindingList<FavoritesViewModel> GetViewModel()
        {
            return this._favoritesViewModels;
        }

        /// <summary>
        /// Deletes a favorites entry by its ID.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public async void DeleteById(int id)
        {
            using (var db = new DataContext())
            {
                var toRemove = await db.Favorites.Include(fav => fav.Url).SingleAsync(fav => fav.Id == id);

                this._favorites.Remove(id);
                foreach (var elem in this._favoritesViewModels)
                {
                    if (elem.Url == toRemove.Url.ToString())
                    {
                        this._favoritesViewModels.Remove(elem);
                        break;
                    }
                }
                
                db.Favorites.Remove(toRemove);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Changes the entry in the database and calls the event to update in memory.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="update">
        /// The get update.
        /// </param>
        public async void UpdateById(int id, FavoritesLocation update)
        {
            using (var db = new DataContext())
            {
                var updated = await db.Favorites.SingleAsync(fav => fav.Id == id);
                updated.Url = update.Url;
                updated.Name = update.Name;
                await db.SaveChangesAsync();

                this.OnFavoritesAddOrUpdate?.Invoke(this, new FavoritesUpdateEventArgs(updated));
            }

        }

        /// <summary>
        /// The favorites updated event.
        /// </summary>
        public event FavoritesUpdateEventHandler OnFavoritesAddOrUpdate;

        /// <inheritdoc />
        /// <summary>
        /// Gets or creates a favorites location.
        /// </summary>
        /// <param name="locUrl">
        /// The loc url.
        /// </param>
        /// <returns>
        /// The <see cref="T:Browser.Favorites.FavoritesLocation" />.
        /// </returns>
        public FavoritesLocation GetOrCreate(Url locUrl)
        {
            var inmem = this._favorites.SingleOrDefault(entry => entry.Value.Url == locUrl).Value;
            return inmem ?? this.Add(locUrl);
        }
    }
}