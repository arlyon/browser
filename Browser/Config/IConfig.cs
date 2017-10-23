namespace Browser.Config
{
    using System;

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
        string DatabaseName { get; }

        /// <summary>
        /// Gets or sets the home url.
        /// </summary>
        Url Home { get; set; }

        /// <summary>
        /// Gets or sets the history time span.
        /// </summary>
        TimeSpan HistoryTimeSpan { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether load all history.
        /// </summary>
        bool OverrideAndLoadAllHistory { get; set; }
    }
}