namespace Browser.Requests
{
    using System;
    using System.ComponentModel.DataAnnotations;
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
        public Url()
        {
        }

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
        }

        /// <summary>
        /// The hash code.
        /// </summary>
        public int HashCode => this.GetHashCode();

        /// <summary>
        /// Gets or sets the addon.
        /// </summary>
        public string Addon { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the scheme.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Gets or sets the unidentified.
        /// </summary>
        public string Unidentified { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The ==.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// Whether the urls point to the same place.
        /// </returns>
        public static bool operator ==(Url a, Url b)
        {
            // not null and equal to b
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Equals(b);
        }

        /// <summary>
        /// The !=.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// Whether the urls don't point to the same place.
        /// </returns>
        public static bool operator !=(Url a, Url b)
        {
            return !(a == b);
        }

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
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object other)
        {
            return other is Url && this.ToString() == other.ToString();
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Addon != null ? this.Addon.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.Host != null ? this.Host.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Scheme != null ? this.Scheme.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unidentified != null ? this.Unidentified.GetHashCode() : 0);
                return hashCode;
            }
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