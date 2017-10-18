namespace Browser.Requests
{
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// The http request.
    /// </summary>
    internal static class HttpRequest
    {
        /// <summary>
        /// The favicon lookup url.
        /// </summary>
        private const string FaviconLookupUrl = "https://www.google.com/s2/favicons?domain=";

        /// <summary>
        /// The get async.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<HttpResponse> GetAsync(Url url)
        {
            url = SanitizeUrl(url);

            var request = (HttpWebRequest)WebRequest.Create(url.ToString());
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)await request.GetResponseAsync();
            }
            catch (WebException e)
            {
                return new HttpResponse
                           {
                               Content = e.Message,
                               Header = e.Response.Headers.ToString(),
                               Url = url,
                               Status = ((HttpWebResponse)e.Response).StatusCode
                           };
            }

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
        /// The get favicon.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<Image> GetFavicon(Url url)
        {
            url = SanitizeUrl(url);

            var request = (HttpWebRequest)WebRequest.Create(FaviconLookupUrl + url.Host);
            var response = await request.GetResponseAsync();

            using (var stream = response.GetResponseStream())
            {
                return Image.FromStream(stream);
            }
        }

        /// <summary>
        /// Sanitizes the url to make sure it is compatible with WebRequest.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The sanitized <see cref="Url"/>.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// Throws exception when no hostname is defined.
        /// </exception>
        private static Url SanitizeUrl(Url url)
        {
            if (url.Host == string.Empty) throw new HttpRequestException();
            if (url.Scheme == string.Empty) url.Scheme = "http://";
            return url;
        }
    }
}