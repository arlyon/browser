namespace Browser.Requests
{
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// The http request.
    /// </summary>
    public static class HttpRequest
    {
        /// <summary>
        /// The favicon lookup url.
        /// </summary>
        private const string FaviconLookupUrl = "https://www.google.com/s2/favicons?domain=";

        /// <summary>
        /// Gets a HttpResponse asynchronously.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<HttpResponse> GetAsync(Url url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url.ToString());
            var response = (HttpWebResponse)await request.GetResponseAsync();

            var contentStreamReader = new StreamReader(response.GetResponseStream());
            var content = await contentStreamReader.ReadToEndAsync();

            return new HttpResponse
                       {
                           Content = content,
                           Header = response.SupportsHeaders ? response.Headers.ToString() : null,
                           Url = url,
                           Status = response.StatusCode
                       };
        }

        /// <summary>
        /// Gets the favicon image associated with the given URL.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<Image> GetFaviconAsync(Url url)
        {
            var request = (HttpWebRequest)WebRequest.Create(FaviconLookupUrl + url.Host);
            var response = await request.GetResponseAsync();

            using (var stream = response.GetResponseStream())
            {
                return Image.FromStream(stream);
            }
        }
    }
}