namespace Browser.Tests.Config
{
    using System;

    using Browser.Config;
    using Browser.Requests;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <inheritdoc />
    /// <summary>
    /// The mock config.
    /// </summary>
    internal class MockConfig : IConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockConfig"/> class.
        /// </summary>
        public MockConfig()
        {
            this.DatabaseName = "database.sql";
            this.Home = Url.FromString("www.mock.com");
            this.HistoryTimeSpan = new TimeSpan(1, 0, 0, 0);
            this.OverrideAndLoadAllHistory = true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the database name.
        /// </summary>
        public string DatabaseName { get; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the home.
        /// </summary>
        public Url Home { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the history time span.
        /// </summary>
        public TimeSpan HistoryTimeSpan { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a value indicating whether to override and load all history.
        /// </summary>
        public bool OverrideAndLoadAllHistory { get; set; }
    }

    /// <summary>
    /// The mock config test.
    /// </summary>
    [TestClass]
    public class MockConfigTest
    {
    }
}
