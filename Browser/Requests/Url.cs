namespace Browser.Requests
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The url.
    /// </summary>
    public class Url
    {
        /// <summary>
        /// The url test.
        /// </summary>
        private static readonly Regex UrlTest = new Regex(@"^(.*:\/\/)?([\p{L}\p{M}\.\:\@0-9]*)([\/\#]?.*)?");

        /// <summary>
        /// Initializes a new instance of the <see cref="Url"/> class.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        public Url(string url)
        {
            var urlMatch = UrlTest.Match(url);

            if (IsUrl(urlMatch, url))
            {
                // if it is a Url, return the three sections
                this.Scheme = urlMatch.Groups[1].Value;
                this.Host = urlMatch.Groups[2].Value;
                this.Addon = urlMatch.Groups[3].Value;
                this.Unidentified = string.Empty;
            }
            else
            {
                // if it isnt a Url, then only return the plain Url
                this.Scheme = string.Empty;
                this.Host = string.Empty;
                this.Addon = string.Empty;
                this.Unidentified = url;
            }

            this.ID = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Url"/> class.
        /// </summary>
        /// <param name="scheme">
        /// The scheme.
        /// </param>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="addon">
        /// The addon.
        /// </param>
        /// <param name="unidentified">
        /// The unidentified.
        /// </param>
        public Url(string scheme, string host, string addon, string unidentified)
        {
            this.Scheme = scheme;
            this.Host = host;
            this.Addon = addon;
            this.Unidentified = unidentified;
            this.ID = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the addon.
        /// </summary>
        public string Addon { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the scheme.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Gets or sets the unidentified.
        /// </summary>
        public string Unidentified { get; set; }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return this.Scheme + this.Host + this.Addon + this.Unidentified;
        }

        /// <summary>
        /// The is url.
        /// </summary>
        /// <param name="urlMatch">
        /// The url match.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsUrl(Match urlMatch, string url)
        {
            return urlMatch.Groups[1].Value + urlMatch.Groups[2].Value + urlMatch.Groups[3].Value == url;
        }
    }
}