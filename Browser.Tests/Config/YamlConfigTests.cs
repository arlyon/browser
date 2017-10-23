namespace Browser.Tests.Config
{
    using System.IO;

    using Browser.Config;
    using Browser.Requests;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using YamlDotNet.Serialization;

    /// <summary>
    /// The yaml config.
    /// </summary>
    [TestClass]
    public class YamlConfigTests
    {
        /// <summary>
        /// Run after all tests to delete the settings if it exists.
        /// </summary>
        [TestCleanup]
        public void DeleteSettingsFileIfExists()
        {
            if (File.Exists("settings.yaml"))
            {
                File.Delete("settings.yaml");
            }
        }

        /// <summary>
        /// The load test.
        /// </summary>
        [TestMethod]
        public void LoadTest()
        {
            // generate default settings
            var defaultSettings = new
            {
                DatabaseName = "sqlite.db",
                HistoryTimeSpan = "1.00:00:00",
                OverrideAndLoadAllHistory = "False",
                HomeUrl = "http://test.com/"
            };

            var serializer = new SerializerBuilder().EmitDefaults().Build();
            var yaml = serializer.Serialize(defaultSettings);

            using (var sw = new StreamWriter("settings.yaml"))
            {
                sw.WriteLine(yaml);
            }

            // try load settings from disk (or generate new settings)
            var settings = new YamlConfig();

            // assert equality
            Assert.AreEqual(defaultSettings.HomeUrl, settings.HomeUrl);
            Assert.AreEqual(defaultSettings.DatabaseName, settings.DatabaseName);
            Assert.AreEqual(defaultSettings.OverrideAndLoadAllHistory, settings.OverrideAndLoadAllHistory.ToString());
            Assert.AreEqual(defaultSettings.HistoryTimeSpan, settings.HistoryTimeSpan.ToString());
        }

        /// <summary>
        /// The save test.
        /// </summary>
        [TestMethod]
        public void SaveTest()
        {
            // delete file and create new config, saving it
            if (File.Exists("settings.yaml")) File.Delete("settings.yaml");
            new YamlConfig { Home = Url.FromString("test.com") };

            // load config from disk and make sure values are equal
            var conf = new YamlConfig();
            Assert.AreEqual("http://test.com/", conf.HomeUrl);
        }
    }
}