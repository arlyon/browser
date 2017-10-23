namespace Browser.Tests.Favorites
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using Browser.Favorites;
    using Browser.History;
    using Browser.Requests;
    using Browser.Tests.Config;
    using Browser.Tests.Favicon;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <inheritdoc />
    /// <summary>
    /// The mock favorites.
    /// </summary>
    internal class MockFavorites : IFavorites
    {
        /// <summary>
        /// The _viewModel.
        /// </summary>
        private BindingList<FavoritesViewModel> _viewModel;

        /// <summary>
        /// The _id lookup.
        /// </summary>
        private Dictionary<int, FavoritesLocation> _idLookup;

        /// <summary>
        /// The id.
        /// </summary>
        private int id = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockFavorites"/> class.
        /// </summary>
        public MockFavorites()
        {
            this._viewModel = new BindingList<FavoritesViewModel>();
            this._idLookup = new Dictionary<int, FavoritesLocation>();
        }

        /// <summary>
        /// The mock get or create.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="FavoritesLocation"/>.
        /// </returns>
        public FavoritesLocation GetOrCreate(Url url)
        {
            var inDb = this._idLookup.SingleOrDefault(pair => pair.Value.Url == url);
            if (inDb.Value != null) return inDb.Value;

            var loc = new FavoritesLocation(url);
            this._viewModel.Add(FavoritesViewModel.FromFavoritesLocation(loc, new MockFaviconCache()));
            this._idLookup.Add(this.id, loc);
            this.OnFavoritesAddOrUpdate?.Invoke(this, new FavoritesUpdateEventArgs(loc));
            loc.Id = this.id++;
            return loc;
        }

        /// <summary>
        /// The get view model.
        /// </summary>
        /// <returns>
        /// The <see cref="BindingList{T}"/>.
        /// </returns>
        public BindingList<FavoritesViewModel> GetViewModel()
        {
            return this._viewModel;
        }

        /// <summary>
        /// The delete by id.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        public void DeleteById(int index)
        {
            var url = this._idLookup[index]?.Url;
            if (url == null) return;

            this._viewModel = new BindingList<FavoritesViewModel>(
                this._viewModel.ToList().Where(model => model.Url != url.ToString()).ToList());

            this._idLookup.Remove(index);
        }

        /// <summary>
        /// The update by id.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="update">
        /// The get update.
        /// </param>
        public void UpdateById(int index, FavoritesLocation update)
        {
            var url = this._idLookup[index]?.Url;
            if (url == null) return;

            var viewmodel = this._viewModel.ToList().Single(view => view.Url == url.ToString());
            viewmodel.Name = update.Name;
            viewmodel.Url = update.Url.ToString();
            this.OnFavoritesAddOrUpdate?.Invoke(this, new FavoritesUpdateEventArgs(update));
        }

        /// <summary>
        /// The on favorites updated.
        /// </summary>
        public event FavoritesUpdateEventHandler OnFavoritesAddOrUpdate;
    }
}