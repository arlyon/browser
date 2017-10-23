namespace Browser.Requests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The url class, a "subset" of the URI class that is only compatible with http. Immutable.
    /// </summary>
    public class Url
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Url"/> class from a string.
        /// </summary>
        /// <param name="url">
        /// The url string.
        /// </param>
        /// <returns>
        /// The <see cref="Url"/>.
        /// </returns>
        public static Url FromString(string url)
        {
            Uri uri;

            try { uri = new UriBuilder(url).Uri; }
            catch { throw new InvalidUrlException("Could not identify the url."); }

            if (uri.IsFile) throw new InvalidUrlException("Url is file.");
            if (!uri.Scheme.Equals("http") && !uri.Scheme.Equals("https")) throw new InvalidUrlException("Url is not http protocol.");

            return new Url(uri.Scheme, uri.Host, uri.AbsolutePath);
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
        /// <param name="path">
        /// The path.
        /// </param>
        public Url(string scheme, string host, string path)
        {
            scheme = scheme.Replace("://", string.Empty);
            if (!scheme.Equals("http") && !scheme.Equals("https")) throw new InvalidUrlException("Url is not http protocol.");
            
            this.Scheme = scheme + "://";
            this.Host = host;
            this.Path = path;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Url"/> class from being created.
        /// </summary>
        private Url()
        {
        }

        /// <summary>
        /// The hash code.
        /// </summary>
        public int HashCode => this.GetHashCode();

        /// <summary>
        /// Gets the path.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets the host.
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// Gets the scheme.
        /// </summary>
        public string Scheme { get; private set; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        [Key]
        public int Id { get; private set; }

        /// <summary>
        /// Overrides the == operator to check for equality.
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
        /// Overrides the != operator to check for equality.
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
        /// Converts the url to a string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return this.Scheme + this.Host + this.Path;
        }

        /// <summary>
        /// Overrides object.equals to check for equality.
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
                var hashCode = this.Path != null ? this.Path.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.Host != null ? this.Host.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Scheme != null ? this.Scheme.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}