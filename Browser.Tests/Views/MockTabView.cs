namespace Browser.Tests.Views
{
    using System;
    using System.Drawing;

    using Browser.History;
    using Browser.Requests;
    using Browser.Tests.History;
    using Browser.Views;

    public class MockTabView : ITab
    {
        private ITabHistory _history;

        public MockTabView()
        {
            this._history = new MockTabHistory();
            this.PageLoads = 0;
        }

        public int FaviconIndex { get; set; }

        public bool Loading { get; set; }

        public string Name { get; set; }

        public bool HasReloaded { get; set; }

        public int PageLoads { get; private set; }

        public void CanGoBack(bool value)
        {
        }

        public void CanGoForward(bool value)
        {
        }

        public void CanReload(bool canReload)
        {
        }

        public bool Contains(Point eLocation)
        {
            return true;
        }

        public int GetIndex()
        {
            return 0;
        }

        public void Display(string title)
        {
            this.PageLoads++;
        }

        public void Display(HttpResponse httpResponse)
        {
            this.PageLoads++;
        }

        public void Show()
        {
        }

        public void Display(HttpResponse response, string title)
        {
            this.PageLoads++;
        }

        #region Events

        public event EventHandler AddFavorite;

        public void InvokeAddFavorite()
        {
            this.AddFavorite?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Back;

        public void InvokeBack()
        {
            this.Back?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Forward;

        public void InvokeForward()
        {
            this.Forward?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler GoHome;

        public void InvokeGoHome()
        {
            this.GoHome?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Reload;

        public void InvokeReload()
        {
            this.Reload?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RenderPage;

        public void InvokeRenderPage()
        {
            this.RenderPage?.Invoke(this, EventArgs.Empty);
        }

        public event UrlUpdateEventHandler Submit;

        public void InvokeSubmit(Url url)
        {
            this.Submit?.Invoke(this, new UrlUpdateEventArgs(url));
        }

        #endregion
    }
}
