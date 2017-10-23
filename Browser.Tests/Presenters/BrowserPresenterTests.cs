namespace Browser.Tests.Presenters
{
    using Browser.Favorites;
    using Browser.Presenters;
    using Browser.Requests;
    using Browser.Tests.Config;
    using Browser.Tests.Favicon;
    using Browser.Tests.Favorites;
    using Browser.Tests.History;
    using Browser.Tests.Views;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The browser presenter tests.
    /// </summary>
    [TestClass]
    public class BrowserPresenterTests
    {
        /// <summary>
        /// The invoke close tab event.
        /// </summary>
        [TestMethod]
        public void InvokeCloseTab()
        {
            var view = new MockBrowserView();
            var presenter = new BrowserPresenter<MockTabPresenter>(
                view,
                new MockHistory(),
                new MockFavorites(),
                new MockConfig(),
                new MockFaviconCache(),
                (tab, favorites, config, favicons, history, tabHistory) => new MockTabPresenter(tab, favorites, config, favicons, history, tabHistory));

            view.InvokeNewTab();
            var first = view.SelectedTab;
            view.InvokeNewTab();
            var second = view.SelectedTab;
            view.InvokeCloseTab();
            Assert.AreEqual(view.SelectedTab, first);
        }

        /// <summary>
        /// The invoke close window event.
        /// </summary>
        [TestMethod]
        public void InvokeCloseWindow()
        {
            var view = new MockBrowserView();
            var presenter = new BrowserPresenter<MockTabPresenter>(
                view,
                new MockHistory(),
                new MockFavorites(),
                new MockConfig(),
                new MockFaviconCache(),
                (tab, favorites, config, favicons, history, tabHistory) => new MockTabPresenter(tab, favorites, config, favicons, history, tabHistory));

            view.InvokeCloseWindow();
            Assert.IsTrue(view.IsClosed);
        }

        /// <summary>
        /// The invoke favorites list open event.
        /// </summary>
        [TestMethod]
        public void InvokeFavoritesListOpen()
        {
            var config = new MockConfig();
            var favorites = new MockFavorites();
            var view = new MockBrowserView();
            var presenter = new BrowserPresenter<MockTabPresenter>(
                view,
                new MockHistory(),
                favorites,
                config,
                new MockFaviconCache(),
                (tab, f, c, favicons, history, tabHistory) => new MockTabPresenter(tab, f, c, favicons, history, tabHistory));

            favorites.GetOrCreate(Url.FromString("www.myfavorite.com"));
            view.InvokeFavoritesListOpen(0);

            // easiest way to check if the url has changed
            var oldHome = config.Home;
            view.InvokeHomeChanged();
            Assert.AreNotEqual(oldHome, config.Home);
        }

        /// <summary>
        /// The invoke favorites list edit event.
        /// </summary>
        [TestMethod]
        public void InvokeFavoritesListEdit()
        {
            var view = new MockBrowserView();
            var presenter = new BrowserPresenter<MockTabPresenter>(
                view,
                new MockHistory(),
                new MockFavorites(),
                new MockConfig(),
                new MockFaviconCache(),
                (tab, favorites, config, favicons, history, tabHistory) => new MockTabPresenter(tab, favorites, config, favicons, history, tabHistory));

            // TODO finish
        }

        /// <summary>
        /// The invoke history list open event.
        /// </summary>
        [TestMethod]
        public void InvokeHistoryListOpen()
        {
            var config = new MockConfig();
            var favorites = new MockFavorites();
            var view = new MockBrowserView();
            var presenter = new BrowserPresenter<MockTabPresenter>(
                view,
                new MockHistory(),
                favorites,
                config,
                new MockFaviconCache(),
                (tab, f, c, favicons, history, tabHistory) => new MockTabPresenter(tab, f, c, favicons, history, tabHistory));

            var url = Url.FromString("www.myfavorite.com");
            var url2 = Url.FromString("www.myfavorite2.com");

            favorites.GetOrCreate(url);
            favorites.GetOrCreate(url2);
            view.InvokeFavoritesListOpen(0);
            view.InvokeFavoritesListOpen(1);
            
            // 0 - mock.com
            // 1 - url
            // 2 - url2

            view.InvokeHistoryListOpen(1);

            // easiest way to check if the url has changed
            view.InvokeHomeChanged();
            Assert.AreEqual(url, config.Home);
            
            view.InvokeHistoryListOpen(2);

            // easiest way to check if the url has changed
            view.InvokeHomeChanged();
            Assert.AreEqual(url2, config.Home);
        }

        /// <summary>
        /// The invoke history list save event.
        /// </summary>
        [TestMethod]
        public void InvokeHistoryListSave()
        {
            var view = new MockBrowserView();
            var history = new MockHistory();
            var config = new MockConfig();
            var presenter = new BrowserPresenter<MockTabPresenter>(
                view,
                history,
                new MockFavorites(),
                config,
                new MockFaviconCache(),
                (tab, favorites, c, favicons, h, tabHistory) => new MockTabPresenter(tab, favorites, c, favicons, h, tabHistory));

            // save mock.com to favorites
            view.InvokeHistoryListSave(0);

            // open mock.com from favorites
            view.InvokeFavoritesListOpen(0);

            Assert.AreEqual(config.Home, history.GetViewModel()[history.GetViewModel().Count - 1].GetUrl());
        }

        /// <summary>
        /// The invoke home changed event.
        /// </summary>
        [TestMethod]
        public void InvokeHomeChanged()
        {
            var config = new MockConfig();
            var favorites = new MockFavorites();
            var view = new MockBrowserView();
            var presenter = new BrowserPresenter<MockTabPresenter>(
                view,
                new MockHistory(),
                favorites,
                config,
                new MockFaviconCache(),
                (tab, f, c, favicons, history, tabHistory) => new MockTabPresenter(tab, f, c, favicons, history, tabHistory));
            view.InvokeFavoritesListOpen(0);

            // easiest way to check if the url has changed
            var oldHome = config.Home;
            view.InvokeHomeChanged();
            Assert.AreNotEqual(oldHome, config.Home);
        }

        /// <summary>
        /// The invoke new incognito tab event.
        /// </summary>
        [TestMethod]
        public void InvokeNewIncognitoTab()
        {
            var history = new MockHistory();
            var favorites = new MockFavorites();
            var view = new MockBrowserView();
            var presenter = new BrowserPresenter<MockTabPresenter>(
                view,
                history,
                favorites,
                new MockConfig(), 
                new MockFaviconCache(),
                (tab, f, c, favicons, h, tabHistory) => new MockTabPresenter(tab, f, c, favicons, h, tabHistory));

            var url = Url.FromString("www.myfavorite.com");
            var url2 = Url.FromString("www.myfavorite2.com");

            favorites.GetOrCreate(url);
            favorites.GetOrCreate(url2);
            view.InvokeFavoritesListOpen(0);
            view.InvokeFavoritesListOpen(1);

            // open and switch to an incognito tab
            view.InvokeNewIncognitoTab();

            var list = history.GetViewModel().Count;
            view.InvokeFavoritesListOpen(0);
            Assert.AreEqual(list, history.GetViewModel().Count);
        }

        /// <summary>
        /// The invoke next tab event.
        /// </summary>
        [TestMethod]
        public void InvokeNextTab()
        {
            var view = new MockBrowserView();
            var presenter = new BrowserPresenter<MockTabPresenter>(
                view,
                new MockHistory(),
                new MockFavorites(),
                new MockConfig(),
                new MockFaviconCache(),
                (tab, favorites, config, favicons, history, tabHistory) => new MockTabPresenter(tab, favorites, config, favicons, history, tabHistory));

            var firstTab = view.SelectedTab;
            view.InvokeNewTab();
            var secondTab = view.SelectedTab;
            view.InvokeNextTab();

            Assert.AreEqual(firstTab, view.SelectedTab);
            Assert.AreNotEqual(secondTab, view.SelectedTab);
            Assert.AreNotEqual(firstTab, secondTab);
        }

        /// <summary>
        /// The invoke prev tab event.
        /// </summary>
        [TestMethod]
        public void InvokePrevTab()
        {
            var view = new MockBrowserView();
            var presenter = new BrowserPresenter<MockTabPresenter>(
                view,
                new MockHistory(),
                new MockFavorites(),
                new MockConfig(),
                new MockFaviconCache(),
                (tab, favorites, config, favicons, history, tabHistory) => new MockTabPresenter(tab, favorites, config, favicons, history, tabHistory));

            var firstTab = view.SelectedTab;
            view.InvokeNewTab();
            var secondTab = view.SelectedTab;
            view.InvokePrevTab();

            Assert.AreEqual(firstTab, view.SelectedTab);
            Assert.AreNotEqual(secondTab, view.SelectedTab);
            Assert.AreNotEqual(firstTab, secondTab);
        }

        /// <summary>
        /// The invoke tab changed event.
        /// </summary>
        [TestMethod]
        public void InvokeTabChanged()
        {
            var view = new MockBrowserView();
            var presenter = new BrowserPresenter<MockTabPresenter>(
                view,
                new MockHistory(),
                new MockFavorites(),
                new MockConfig(),
                new MockFaviconCache(),
                (tab, favorites, config, favicons, history, tabHistory) => new MockTabPresenter(tab, favorites, config, favicons, history, tabHistory));

            view.InvokeNewTab();
            var currentTab = view.SelectedTab;
            view.InvokeTabChanged(0);
            Assert.AreNotEqual(currentTab, view.SelectedTab);
        }

        /// <summary>
        /// The invoke go home.
        /// </summary>
        [TestMethod]
        public void InvokeGoHome()
        {
            var view = new MockBrowserView();
            var history = new MockHistory();
            var favorites = new MockFavorites();
            var config = new MockConfig();
            var presenter = new BrowserPresenter<MockTabPresenter>(
                view,
                history,
                favorites,
                config,
                new MockFaviconCache(),
                (tab, f, c, favicons, h, tabHistory) => new MockTabPresenter(tab, f, c, favicons, h, tabHistory));

            var url = Url.FromString("www.myfavorite.com");
            var url2 = Url.FromString("www.myfavorite2.com");

            favorites.GetOrCreate(url);
            favorites.GetOrCreate(url2);
            view.InvokeFavoritesListOpen(0);
            view.InvokeFavoritesListOpen(1);
            
            view.InvokeFavoritesListOpen(0);

            Assert.AreEqual(history.GetViewModel()[history.GetViewModel().Count - 1].GetUrl(), url);

            view.InvokeGoHome();

            Assert.AreEqual(history.GetViewModel()[history.GetViewModel().Count - 1].GetUrl(), config.Home);
        }
    }
}