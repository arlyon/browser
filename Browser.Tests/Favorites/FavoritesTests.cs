namespace Browser.Tests.Favorites
{
    using System.IO;
    using System.Linq;

    using Browser.Favorites;
    using Browser.Requests;
    using Browser.Tests.Config;
    using Browser.Tests.Favicon;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The favorites tests.
    /// </summary>
    [TestClass]
    public class FavoritesTests : FavoritesBaseTests
    {
        /// <inheritdoc />
        /// <summary>
        /// The create favorites.
        /// </summary>
        /// <returns>
        /// The <see cref="T:Browser.Favorites.IFavorites" />.
        /// </returns>
        protected override IFavorites CreateFavorites()
        {
            if (File.Exists("sqlite.db"))
            {
                File.Delete("sqlite.db");
            }

            return new SqliteFavorites(new MockConfig(), new MockFaviconCache());
        }
    }

    /// <summary>
    /// The favorites tests.
    /// </summary>
    [TestClass]
    public class MockFavoritesTests : FavoritesBaseTests
    {
        /// <inheritdoc />
        /// <summary>
        /// The create favorites.
        /// </summary>
        /// <returns>
        /// The <see cref="T:Browser.Favorites.IFavorites" />.
        /// </returns>
        protected override IFavorites CreateFavorites()
        {
            return new MockFavorites();
        }
    }

    /// <summary>
    /// The favorites tests.
    /// </summary>
    [TestClass]
    public abstract class FavoritesBaseTests
    {
        /// <summary>
        /// The _favorites.
        /// </summary>
        private IFavorites _favorites;

        /// <summary>
        /// Run before all tests to instantiate the favorites.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this._favorites = this.CreateFavorites();
        }

        /// <summary>
        /// The create favorites.
        /// </summary>
        /// <returns>
        /// The <see cref="IFavorites"/>.
        /// </returns>
        protected abstract IFavorites CreateFavorites();
        
        /// <summary>
        /// The delete by id test.
        /// </summary>
        [TestMethod]
        public void DeleteByIdTest()
        {
            // create Location and count how many
            var loc = this._favorites.GetOrCreate(Url.FromString("www.test.com"));
            var count = this._favorites.GetViewModel().Count;

            // delete Location and make sure the count is correct
            this._favorites.DeleteById(loc.Id);
            Assert.AreEqual(count - 1, this._favorites.GetViewModel().Count);
        }

        /// <summary>
        /// The get or create test.
        /// </summary>
        [TestMethod]
        public void GetOrCreateTest()
        {
            // create Location
            var url = Url.FromString("www.test.com");
            var loc = this._favorites.GetOrCreate(url);

            // make sure the favorites Location is correct
            Assert.AreEqual(url, loc.Url);
            Assert.AreEqual(url.Host, loc.Name);

            // add an already existing item
            var loc2 = this._favorites.GetOrCreate(url);

            // make sure that it returned the existing one
            Assert.AreEqual(loc, loc2);
            this._favorites.DeleteById(loc.Id);
        }

        /// <summary>
        /// The get view model test.
        /// </summary>
        [TestMethod]
        public void GetViewModelTest()
        {
            // create new item
            var favorites = new SqliteFavorites(new MockConfig(), new MockFaviconCache());
            var loc = favorites.GetOrCreate(Url.FromString("www.test.com"));

            // get view model and assert the item exists
            var model = favorites.GetViewModel();
            var count = model.Count;
            Assert.IsNotNull(model.Single(modellocation => modellocation.Url == loc.Url.ToString()));
            Assert.AreEqual(count, model.Count);

            // add new item to favorites and assert viewmodel is updated
            var loc2 = favorites.GetOrCreate(Url.FromString("www.test2.com"));
            Assert.AreEqual(++count, model.Count);
            favorites.DeleteById(loc.Id);
        }

        /// <summary>
        /// The update by id test.
        /// </summary>
        [TestMethod]
        public void UpdateByIdTest()
        {
            // create new item
            var favorites = new SqliteFavorites(new MockConfig(), new MockFaviconCache());
            var loc = favorites.GetOrCreate(Url.FromString("www.test.com"));

            var newurl = Url.FromString("www.updatedtest.com");
            favorites.UpdateById(loc.Id, new FavoritesLocation(newurl));
            var loc2 = favorites.GetOrCreate(newurl);
            Assert.AreEqual(loc.Id, loc2.Id);
            Assert.AreNotEqual(loc.Name, loc2.Name);
            Assert.AreNotEqual(loc.Url, loc2.Url);
        }
    }
}