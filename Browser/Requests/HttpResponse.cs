namespace Browser.Requests
{
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// The http response.
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// The title match.
        /// </summary>
        private static Regex titleMatch = new Regex("<title.*>(.*)</title>");

        /// <summary>
        /// The title.
        /// </summary>
        private string _title;

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public HttpStatusCode Status { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public Url Url { get; set; }

        /// <summary>
        /// Gets the title asynchronously, on demand.
        /// </summary>
        /// <returns>
        /// The <see cref="string">title</see>.
        /// </returns>
        public async Task<string> GetTitle()
        {
            if (!string.IsNullOrEmpty(this._title)) return this._title;

            this._title = await Task.Run(
                              () =>
                                  {
                                      try
                                      {
                                          return titleMatch.Match(this.Content).Groups[1].Value;
                                      }
                                      catch
                                      {
                                          return this.Url.Host;
                                      }
                                  });

            return this._title;
        }
    }
}