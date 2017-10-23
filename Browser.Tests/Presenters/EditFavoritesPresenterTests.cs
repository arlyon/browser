namespace Browser.Tests.Presenters
{
    using System;
    using System.Linq;

    using Browser.Favorites;
    using Browser.Presenters;
    using Browser.Requests;
    using Browser.Tests.Favorites;
    using Browser.Views;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The edit favorites presenter tests.
    /// </summary>
    [TestClass]
    public class EditFavoritesPresenterTests
    {
        /// <summary>
        /// The edit favorites presenter test.
        /// </summary>
        [TestMethod]
        public void InvokeCancelTest()
        {
            var win = new MockFavoritesView();
            var fav = new MockFavorites();
            var url = Url.FromString("Test");
            EditFavoritesPresenter pres = new EditFavoritesPresenter(win, url, fav);

            win.InvokeCancelPage();

            Assert.AreEqual(fav.GetViewModel().Count, 1);
            Assert.AreEqual(fav.GetViewModel().Single(obj => obj.Url == url.ToString()).Url, url.ToString());
        }

        /// <summary>
        /// The edit favorites presenter test.
        /// </summary>
        [TestMethod]
        public void InvokeDeleteTest()
        {
            var win = new MockFavoritesView();
            var fav = new MockFavorites();
            var url = Url.FromString("Test");
            EditFavoritesPresenter pres = new EditFavoritesPresenter(win, url, fav);

            win.InvokeDeletePage();

            Assert.AreEqual(0, fav.GetViewModel().Count);
        }

        /// <summary>
        /// The edit favorites presenter test.
        /// </summary>
        [TestMethod]
        public void InvokeSaveTest()
        {
            var win = new MockFavoritesView();
            var fav = new MockFavorites();
            var url = Url.FromString("Test");
            EditFavoritesPresenter pres = new EditFavoritesPresenter(win, url, fav);

            win.InvokeSavePage();

            Assert.AreEqual(fav.GetViewModel().Count, 1);
            Assert.IsNotNull(fav.GetViewModel().SingleOrDefault(
                obj => obj.Url == Url.FromString("www.updatedurl.com").ToString()));
        }
    }

    /// <summary>
    /// The mock favorites window.
    /// </summary>
    public class MockFavoritesView : IEditFavorites
    {
        /// <summary>
        /// The save button clicked.
        /// </summary>
        public event EventHandler SaveButtonClicked;

        /// <summary>
        /// The cancel button clicked.
        /// </summary>
        public event EventHandler CancelButtonClicked;

        /// <summary>
        /// The delete button clicked.
        /// </summary>
        public event EventHandler DeleteButtonClicked;

        public void InvokeSavePage()
        {
            this.SaveButtonClicked?.Invoke(this, new EventArgs());
        }

        public void InvokeCancelPage()
        {
            this.CancelButtonClicked?.Invoke(this, new EventArgs());
        }

        public void InvokeDeletePage()
        {
            this.DeleteButtonClicked?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// The display favorites location.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        public void DisplayFavoritesLocation(string name, string url)
        {
        }

        /// <summary>
        /// The show.
        /// </summary>
        public void Show()
        {
        }

        /// <summary>
        /// The close.
        /// </summary>
        public void Close()
        {
        }

        /// <summary>
        /// The get update.
        /// </summary>
        /// <returns>
        /// The <see cref="FavoritesLocation"/>.
        /// </returns>
        public FavoritesLocation GetUpdate()
        {
            return new FavoritesLocation()
                       {
                           Name = "Updated Text",
                           Url = Url.FromString("www.updatedurl.com")
                       };
        }
    }
}