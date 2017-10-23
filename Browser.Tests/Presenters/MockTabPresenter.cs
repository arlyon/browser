using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser.Tests.Presenters
{
    using Browser.Config;
    using Browser.Favicon;
    using Browser.Favorites;
    using Browser.History;
    using Browser.Presenters;
    using Browser.Requests;
    using Browser.Views;

    /// <summary>
    /// The mock tab presenter.
    /// </summary>
    class MockTabPresenter : ITabPresenter
    {

        public MockTabPresenter(ITab tab, IFavorites favorites, IConfig config, IFavicon favicons, IHistory history, ITabHistory tabHistory)
        {
        }

        public string Name { get; set; }

        private Url url;

        public void Push(Url url)
        {
            this.url = url;
        }

        public void Reload()
        {
        }

        public Url Url()
        {
            return this.url;
        }
    }
}
