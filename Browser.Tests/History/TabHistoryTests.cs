namespace Browser.Tests.History
{
    using Browser.Favorites;
    using Browser.History;
    using Browser.Requests;
    using Browser.Tests.Config;
    using Browser.Tests.Favorites;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The history tests.
    /// </summary>
    [TestClass]
    public class TabHistoryTests : TabHistoryBaseTests
    {
        /// <summary>
        /// The create history.
        /// </summary>
        /// <returns>
        /// The <see cref="ITabHistory"/>.
        /// </returns>
        protected override ITabHistory CreateHistory()
        {
            return new TabHistory(new MockHistory());
        }
    }

    /// <summary>
    /// The history tests.
    /// </summary>
    [TestClass]
    public class MockTabHistoryTests : TabHistoryBaseTests
    {
        /// <summary>
        /// The create history.
        /// </summary>
        /// <returns>
        /// The <see cref="ITabHistory"/>.
        /// </returns>
        protected override ITabHistory CreateHistory()
        {
            return new MockTabHistory();
        }
    }

    /// <summary>
    /// The tab history tests.
    /// </summary>
    [TestClass]
    public abstract class TabHistoryBaseTests
    {
        /// <summary>
        /// The cache.
        /// </summary>
        private ITabHistory _tabHistory;

        /// <summary>
        /// Run before all tests to instantiate the cache.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this._tabHistory = this.CreateHistory();
        }

        /// <summary>
        /// The create favorites.
        /// </summary>
        /// <returns>
        /// The <see cref="ITabHistory"/>.
        /// </returns>
        protected abstract ITabHistory CreateHistory();

        /// <summary>
        /// The back test.
        /// </summary>
        [TestMethod]
        public void BackTest()
        {
            // add and push two elements to tab history
            var first = Url.FromString("firsturl.com");
            var second = Url.FromString("secondurl.com");
            this._tabHistory.Push(first);
            this._tabHistory.Push(second);

            // go back
            this._tabHistory.Back();

            // assert equality
            Assert.AreEqual(first, this._tabHistory.Current().Url);

            // trying to back at the start or end of the
            // chain should return null without changing
            Assert.IsNull(this._tabHistory.Back());
            Assert.AreEqual(first, this._tabHistory.Current().Url);

        }

        /// <summary>
        /// The can add to favorites test.
        /// </summary>
        [TestMethod]
        public void CanAddToFavoritesTest()
        {
            // no url in history, so we can't add it
            Assert.IsFalse(this._tabHistory.CanAddToFavorites());

            // we can only add to favorites when the current Location is not null
            Assert.AreEqual(this._tabHistory.Current() != null, this._tabHistory.CanAddToFavorites());

            this._tabHistory.Push(Url.FromString("testurl.com"));

            // url in history, we can add it
            Assert.IsTrue(this._tabHistory.CanAddToFavorites());
        }

        /// <summary>
        /// The can go backward test.
        /// </summary>
        [TestMethod]
        public void CanGoBackwardTest()
        {
            // add and push two elements to tab history
            var first = Url.FromString("firsturl.com");
            var second = Url.FromString("secondurl.com");

            // nothing in history cant go back
            Assert.IsFalse(this._tabHistory.CanGoBackward());

            // on "oldest" thing in history, cant go back
            this._tabHistory.Push(first);
            Assert.IsFalse(this._tabHistory.CanGoBackward());

            // there are "older" things in history, can go back.
            this._tabHistory.Push(second);
            Assert.IsTrue(this._tabHistory.CanGoBackward());
        }

        /// <summary>
        /// The can go forward test.
        /// </summary>
        [TestMethod]
        public void CanGoForwardTest()
        {
            // add and push two elements to tab history
            var first = Url.FromString("firsturl.com");
            var second = Url.FromString("secondurl.com");

            // nothing in history cant go forward
            Assert.IsFalse(this._tabHistory.CanGoForward());

            // on "newest" thing in history, cant go forward
            this._tabHistory.Push(first);
            Assert.IsFalse(this._tabHistory.CanGoForward());

            // there are "newer" things in history, can go forward.
            this._tabHistory.Push(second);
            this._tabHistory.Back();
            Assert.IsTrue(this._tabHistory.CanGoForward());
        }

        /// <summary>
        /// The can reload test.
        /// </summary>
        [TestMethod]
        public void CanReloadTest()
        {
            // no url in history, so we can't reload it
            Assert.IsFalse(this._tabHistory.CanReload());

            // we can only reload when the current Location is not null
            Assert.AreEqual(this._tabHistory.Current() != null, this._tabHistory.CanReload());

            this._tabHistory.Push(Url.FromString("testurl.com"));

            // url in history, we can reload
            Assert.IsTrue(this._tabHistory.CanReload());
        }

        /// <summary>
        /// The current test.
        /// </summary>
        [TestMethod]
        public void CurrentTest()
        {
            var url = Url.FromString("www.test.com");

            // push a new Location and assert the created Location
            // and the current Location are equal
            var createdLoc = this._tabHistory.Push(url);
            var currentLoc = this._tabHistory.Current();
            Assert.AreEqual(createdLoc, currentLoc);

            // add one more and assert again
            var nextLoc = this._tabHistory.Push(url);
            currentLoc = this._tabHistory.Current();
            Assert.AreEqual(nextLoc, currentLoc);

            // go back and assert again
            var prevLoc = this._tabHistory.Back();
            currentLoc = this._tabHistory.Current();
            Assert.AreEqual(currentLoc, prevLoc);
        }

        /// <summary>
        /// The forward test.
        /// </summary>
        [TestMethod]
        public void ForwardTest()
        {
            // add and push two elements to tab history
            var first = Url.FromString("firsturl.com");
            var second = Url.FromString("secondurl.com");
            this._tabHistory.Push(first);
            this._tabHistory.Push(second);

            // go back, then forward
            this._tabHistory.Back();
            this._tabHistory.Forward();

            // assert equality
            Assert.AreEqual(second, this._tabHistory.Current().Url);

            // trying to forward at the end of the
            // chain should return null without changing
            Assert.IsNull(this._tabHistory.Forward());
            Assert.AreEqual(second, this._tabHistory.Current().Url);
        }

        /// <summary>
        /// The push test.
        /// </summary>
        [TestMethod]
        public void PushTest()
        {
            var url = Url.FromString("www.test.com");

            // push url and assert it was added to chain
            // and moved to
            var firstLoc = this._tabHistory.Push(url);
            var currentLoc = this._tabHistory.Current();
            Assert.AreEqual(firstLoc, currentLoc);

            // push url and assert it was added to chain
            // and moved to
            var secondLoc = this._tabHistory.Push(url);
            currentLoc = this._tabHistory.Current();
            Assert.AreEqual(secondLoc, currentLoc);

            this._tabHistory.Back();
            this._tabHistory.Push(url);

            // pushing a new url overwrites, make sure that
            // the secondLoc has been severed from the chain,
            // and our newly pushed element is at the end
            Assert.IsNull(this._tabHistory.Forward());
        }

        /// <summary>
        /// The update title test.
        /// </summary>
        [TestMethod]
        public void UpdateTitleTest()
        {
            // add and push two elements to tab history
            var first = Url.FromString("firsturl.com");
            var second = Url.FromString("secondurl.com");
            this._tabHistory.Push(first);
            this._tabHistory.Push(second);

            const string NewTitle = "NewTitle";
            this._tabHistory.UpdateTitle(NewTitle);

            Assert.AreEqual(this._tabHistory.Current().Title, NewTitle);
        }
    }
}