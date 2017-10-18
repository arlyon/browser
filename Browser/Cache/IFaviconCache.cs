namespace Browser.Cache
{
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Browser.Requests;

    /// <summary>
    /// The FaviconCache interface.
    /// </summary>
    public interface IFaviconCache
    {
        /// <summary>
        /// Gets the favicon image list.
        /// </summary>
        ImageList Favicons { get; }

        /// <summary>
        /// Requests a favicon corresponding to the url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<int> Request(Url url);
    }
}