namespace Browser.Cache
{
    using System.Threading.Tasks;

    /// <summary>
    /// Implemented by viewmodels that have a favicon associated with them.
    /// </summary>
    public interface IHasFavicon
    {
        /// <summary>
        /// The get favicon id.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        Task<int> GetFaviconId();
    }
}
