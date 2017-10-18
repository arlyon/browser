namespace Browser.Config
{
    using Browser.Presenters;
    using Browser.Requests;

    /// <inheritdoc />
    /// <summary>
    /// The Config interface.
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Gets the database name.
        /// </summary>
        string Database { get; }

        /// <summary>
        /// Gets or sets the home url.
        /// </summary>
        Url Home { get; set; }
    }
}