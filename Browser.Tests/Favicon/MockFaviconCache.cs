namespace Browser.Tests.Favicon
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Browser.Favicon;
    using Browser.Requests;

    /// <summary>
    /// The mock favicon cache.
    /// </summary>
    internal class MockFaviconCache : IFavicon
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets the favicons.
        /// </summary>
        public ImageList Favicons { get; }

        private Dictionary<Url, int> _links;

        private int index = 0;


        /// <summary>
        /// Initializes a new instance of the <see cref="MockFaviconCache"/> class.
        /// </summary>
        public MockFaviconCache()
        {
            this._links = new Dictionary<Url, int>();
            this.Favicons = new ImageList();
        }

        /// <inheritdoc />
        /// <summary>
        /// Dummy stub for requesting favicons.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" />.
        /// </returns>
        public Task<int> Request(Url url)
        {
            Task<int> task;

            if (this._links.ContainsKey(url))
            {
                task = new Task<int>(() => this._links[url]);
            }
            else
            {
                this._links[url] = this.index++;
                task = new Task<int>(() => this._links[url]);
            }

            task.Start();
            return task;
        }
    }
}
