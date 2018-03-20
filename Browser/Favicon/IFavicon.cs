namespace Browser.Favicon
{
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Browser.Requests;

    /// <summary>
    /// The Favicon interface.
    /// </summary>
    public interface IFavicon
    {
        /// <summary>
        /// Gets the favicon <see cref="T:System.Windows.Forms.ImageList" />.
        /// </summary>
        ImageList Favicons { get; }

        /// <summary>
        /// Gets the favicon from the given url, caches it, adds it to the
        /// image list in memory, and returns the index for use in the GUI.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/> to get the index.
        /// </returns>
        Task<int> Request(Url url);
    }
}