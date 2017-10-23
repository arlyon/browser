namespace Browser.Tests.Favicon
{
    using System.Threading.Tasks;

    using Browser.Favicon;
    using Browser.Requests;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The favicon cache.
    /// </summary>
    [TestClass]
    public class FaviconCacheTests : FaviconCacheBaseTests
    {
        /// <summary>
        /// The create cache.
        /// </summary>
        /// <returns>
        /// The <see cref="IFaviconCache"/>.
        /// </returns>
        protected override IFavicon CreateCache()
        {
            return new FaviconCache();
        }
    }

    /// <summary>
    /// The mock favicon cache test.
    /// </summary>
    [TestClass]
    public class MockFaviconCacheTest : FaviconCacheBaseTests
    {
        /// <summary>
        /// The create cache.
        /// </summary>
        /// <returns>
        /// The <see cref="IFaviconCache"/>.
        /// </returns>
        protected override IFavicon CreateCache()
        {
            return new MockFaviconCache();
        }
    }

    /// <summary>
    /// The mock favicon cache test.
    /// </summary>
    [TestClass]
    public abstract class FaviconCacheBaseTests
    {
        /// <summary>
        /// The cache.
        /// </summary>
        private IFavicon _cache;

        /// <summary>
        /// Run before all tests to instantiate the cache.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this._cache = this.CreateCache();
        }

        /// <summary>
        /// The create favorites.
        /// </summary>
        /// <returns>
        /// The <see cref="IFaviconCache"/>.
        /// </returns>
        protected abstract IFavicon CreateCache();

        /// <summary>
        /// The request test.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [TestMethod]
        public async Task RequestTest()
        {
            var expected = this._cache.Favicons.Images.Count;
            var id = await this._cache.Request(Url.FromString("www.reddit.com"));
            Assert.AreEqual(expected, id);
        }
    }
}