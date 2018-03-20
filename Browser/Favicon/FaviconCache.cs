namespace Browser.Favicon
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Browser.Properties;
    using Browser.Requests;

    /// <inheritdoc />
    /// <summary>
    /// A cache used to store favicons for the favorites bar and tab menu.
    /// When an image is fetched it is stored in the cache folder, by the
    /// hash of the hostname.
    /// </summary>
    public class FaviconCache : IFavicon
    {
        /// <summary>
        /// The loaded favicons.
        /// </summary>
        private readonly Dictionary<string, int> _loadedFavicons;

        /// <summary>
        /// Initializes a new instance of the <see cref="FaviconCache"/> class.
        /// </summary>
        public FaviconCache()
        {
            this.Favicons = new ImageList { TransparentColor = Color.Blue };

            // required for the loading animation and places without a favicon
            this.Favicons.Images.Add(Resources.nofavicon);
            this.Favicons.Images.Add(Resources.Frame_1);
            this.Favicons.Images.Add(Resources.Frame_2);
            this.Favicons.Images.Add(Resources.Frame_3);
            this.Favicons.Images.Add(Resources.Frame_4);
            this.Favicons.Images.Add(Resources.Frame_5);
            this.Favicons.Images.Add(Resources.Frame_6);
            this.Favicons.Images.Add(Resources.Frame_7);
            this.Favicons.Images.Add(Resources.Frame_8);
            this.Favicons.Images.Add(Resources.Frame_9);
            this.Favicons.Images.Add(Resources.Frame_10);
            this.Favicons.Images.Add(Resources.Frame_11);
            this.Favicons.Images.Add(Resources.Frame_12);
            this.Favicons.Images.Add(Resources.Frame_13);
            this.Favicons.Images.Add(Resources.Frame_14);
            this.Favicons.Images.Add(Resources.Frame_15);
            this.Favicons.Images.Add(Resources.Frame_16);

            this._loadedFavicons = new Dictionary<string, int>();
        }

        /// <inheritdoc />
        public ImageList Favicons { get; }

        /// <inheritdoc />
        public async Task<int> Request(Url url)
        {
            var hash = url.Host.GetHashCode().ToString("X");

            // check if favicon is already in memory
            if (this._loadedFavicons.ContainsKey(hash)) return this._loadedFavicons[hash];

            // make sure dir exists (will not create if not there)
            Directory.CreateDirectory("cache");
            var filename = "./cache/" + hash + ".png";

            // check for file and either load it or fetch new favicon
            if (File.Exists(filename))
            {
                this.Favicons.Images.Add(Image.FromFile(filename));
            }
            else
            {
                var favicon = await HttpRequest.GetFaviconAsync(url);
                favicon.Save(filename);
                this.Favicons.Images.Add(favicon);
            }

            // add to Dict and return index
            var index = this.Favicons.Images.Count - 1;
            this._loadedFavicons[hash] = index;

            return index;
        }
    }
}