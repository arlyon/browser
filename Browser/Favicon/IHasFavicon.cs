namespace Browser.Favicon
{
    using System.Threading.Tasks;

    /// <summary>
    /// Implemented by viewmodels that have a favicon associated with them.
    /// </summary>
    public interface IHasFavicon
    {
        /// <summary>
        /// Gets the favicon ID for the imagelist that it is managing.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        Task<int> GetFaviconId();
    }
}
