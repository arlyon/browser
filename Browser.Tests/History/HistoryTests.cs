namespace Browser.Tests.History
{
    using System.IO;
    using System.Linq;
    
    using Browser.History;
    using Browser.Requests;
    using Browser.Tests.Config;
    using Browser.Tests.Favicon;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The history tests.
    /// </summary>
    [TestClass]
    public class HistoryTests : HistoryBaseTests
    {
        /// <summary>
        /// The create history.
        /// </summary>
        /// <returns>
        /// The <see cref="IHistory"/>.
        /// </returns>
        protected override IHistory CreateHistory()
        {
            if (File.Exists("sqlite.db"))
            {
                File.Delete("sqlite.db");
            }

            return new SqliteHistory(new MockConfig(), new MockFaviconCache());
        }
    }

    /// <summary>
    /// The history tests.
    /// </summary>
    [TestClass]
    public class MockHistoryTests : HistoryBaseTests
    {
        /// <summary>
        /// The create history.
        /// </summary>
        /// <returns>
        /// The <see cref="IHistory"/>.
        /// </returns>
        protected override IHistory CreateHistory()
        {
            return new MockHistory();
        }
    }

    /// <summary>
    /// The base history tests.
    /// </summary>
    [TestClass]
    public abstract class HistoryBaseTests
    {
        /// <summary>
        /// The cache.
        /// </summary>
        private IHistory _history;

        /// <summary>
        /// Run before all tests to instantiate the cache.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this._history = this.CreateHistory();
        }

        /// <summary>
        /// The create favorites.
        /// </summary>
        /// <returns>
        /// The <see cref="IHistory"/>.
        /// </returns>
        protected abstract IHistory CreateHistory();

        /// <summary>
        /// The get view model test.
        /// </summary>
        [TestMethod]
        public void GetViewModelTest()
        {
            // create new item
            this._history.Push(new HistoryLocation(Url.FromString("www.test.com")));

            // get view model and assert the item exists
            var viewModels = this._history.GetViewModel();
            var count = viewModels.Count;
            Assert.IsNotNull(viewModels.Single(viewModel => viewModel.GetUrl() == Url.FromString("www.test.com")));
            Assert.AreEqual(count, viewModels.Count);

            // add new item to favorites and assert viewmodel is updated
            this._history.Push(new HistoryLocation(Url.FromString("www.test2.com")));
            Assert.AreEqual(++count, viewModels.Count);
        }

        /// <summary>
        /// The push test.
        /// </summary>
        [TestMethod]
        public void PushTest()
        {
            // create new item
            Assert.AreEqual(0, this._history.GetViewModel().Count);
            this._history.Push(new HistoryLocation(Url.FromString("www.test.com")));
            Assert.AreEqual(1, this._history.GetViewModel().Count);
        }
    }
}