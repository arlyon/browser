namespace Browser.Tests.Presenters
{
    using System;
    using System.Drawing;
    using System.Linq;

    using Browser.History;
    using Browser.Presenters;
    using Browser.Requests;
    using Browser.Tests.Config;
    using Browser.Tests.Favicon;
    using Browser.Tests.Favorites;
    using Browser.Tests.History;
    using Browser.Tests.Views;
    using Browser.Views;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The tab presenter tests.
    /// </summary>
    [TestClass]
    public class TabPresenterTests
    {
        /// <summary>
        /// The push test.
        /// </summary>
        [TestMethod]
        public void PushTest()
        {
            var tabHistory = new MockTabHistory();
            
            var presenter = new TabPresenter(
                new MockTabView(), 
                new MockFavorites(),
                new MockConfig(),
                new MockFaviconCache(),
                new MockHistory(),
                tabHistory);

            var url = Url.FromString("www.test.com");

            presenter.Push(url);
            Assert.AreEqual(url, tabHistory.Current().Url);
        }

        /// <summary>
        /// The invoke add favorite event test.
        /// </summary>
        [TestMethod]
        public void InvokeAddFavoriteTest()
        {
            var favorites = new MockFavorites();

            var view = new MockTabView();
            var presenter = new TabPresenter(
                view,
                favorites,
                new MockConfig(),
                new MockFaviconCache(),
                new MockHistory(),
                new MockTabHistory());

            var url = Url.FromString("www.test.com");

            presenter.Push(url);
            view.InvokeAddFavorite();
            Assert.IsNotNull(favorites.GetViewModel().Single(model => model.Url == url.ToString()));
        }

        /// <summary>
        /// The invoke back event test.
        /// </summary>
        [TestMethod]
        public void InvokeBackTest()
        {
            var tabHistory = new MockTabHistory();

            var view = new MockTabView();
            var presenter = new TabPresenter(
                view,
                new MockFavorites(), 
                new MockConfig(),
                new MockFaviconCache(),
                new MockHistory(),
                tabHistory);

            var url1 = Url.FromString("www.first.com");
            var url2 = Url.FromString("www.second.com");

            presenter.Push(url1);
            presenter.Push(url2);

            Assert.AreEqual(url2, tabHistory.Current().Url);

            view.InvokeBack();

            Assert.AreEqual(url1, tabHistory.Current().Url);
        }

        /// <summary>
        /// The invoke forward test.
        /// </summary>
        [TestMethod]
        public void InvokeForwardTest()
        {
            var tabHistory = new MockTabHistory();

            var view = new MockTabView();
            var presenter = new TabPresenter(
                view,
                new MockFavorites(),
                new MockConfig(),
                new MockFaviconCache(),
                new MockHistory(),
                tabHistory);

            var url1 = Url.FromString("www.first.com");
            var url2 = Url.FromString("www.second.com");

            presenter.Push(url1);
            presenter.Push(url2);
            tabHistory.Back();

            Assert.AreEqual(url1, tabHistory.Current().Url);

            view.InvokeForward();

            Assert.AreEqual(url2, tabHistory.Current().Url);
        }

        /// <summary>
        /// The invoke go home test.
        /// </summary>
        [TestMethod]
        public void InvokeGoHomeTest()
        {
            var config = new MockConfig();
            var tabHistory = new MockTabHistory();

            var view = new MockTabView();
            var presenter = new TabPresenter(
                view,
                new MockFavorites(),
                config,
                new MockFaviconCache(),
                new MockHistory(),
                tabHistory);

            view.InvokeGoHome();
            Assert.AreEqual(config.Home, tabHistory.Current().Url);
        }

        [TestMethod]
        public void InvokeReloadTest()
        {
            var view = new MockTabView();
            var presenter = new TabPresenter(
                view,
                new MockFavorites(),
                new MockConfig(), 
                new MockFaviconCache(),
                new MockHistory(),
                new MockTabHistory());

            presenter.Push(Url.FromString("www.test.com"));

            int pageloads = view.PageLoads;
            view.InvokeReload();
            Assert.AreEqual(++pageloads, view.PageLoads);
        }

        [TestMethod]
        public void InvokeSubmitTest()
        {
            var tabHistory = new MockTabHistory();

            var view = new MockTabView();
            var presenter = new TabPresenter(
                view,
                new MockFavorites(),
                new MockConfig(),
                new MockFaviconCache(),
                new MockHistory(),
                tabHistory);

            var url = Url.FromString("www.test.com");
            view.InvokeSubmit(url);
            Assert.AreEqual(url, tabHistory.Current().Url);
        }

    }
}