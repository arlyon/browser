namespace Browser.Config
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    using Browser.Requests;

    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    /// <inheritdoc cref="IConfig" />
    /// <summary>
    /// The yaml config.
    /// </summary>
    public class YamlConfig : INotifyPropertyChanged, IConfig
    {
        /// <summary>
        /// The home address.
        /// </summary>
        private Url _home;

        /// <summary>
        /// Determines whether the property update event handler should save.
        /// </summary>
        private bool _triggerSave;

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlConfig"/> class. 
        /// </summary>
        public YamlConfig()
        {
            this._triggerSave = true;
            this.PropertyChanged += this.Save;
            this.Load();
        }

        /// <inheritdoc />
        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc />
        /// <summary>
        /// Gets the database.
        /// </summary>
        public string Database { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the home url.
        /// </summary>
        [YamlIgnore]
        public Url Home
        {
            get => this._home;
            set
            {
                this._home = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the history time span.
        /// </summary>
        public TimeSpan HistoryTimeSpan { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to load all history.
        /// </summary>
        public bool LoadAllHistory { get; set; }

        /// <summary>
        /// Gets or sets the serializable version of homeurl which is kept as a string for easy modification.
        /// </summary>
        public string HomeUrl
        {
            get => this._home.ToString();
            set
            {
                this._home = new Url(value);
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Loads the config file from disk.
        /// </summary>
        public void Load()
        {
            var deserializer = new DeserializerBuilder().WithNamingConvention(new PascalCaseNamingConvention()).Build();

            try
            {
                using (var input = new StreamReader("settings.yaml"))
                {
                    this._triggerSave = false;
                    var properties = deserializer.Deserialize<Dictionary<string, string>>(input);
                    this.HomeUrl = properties["HomeUrl"];
                    this.Database = properties["Database"];
                    this.HistoryTimeSpan = TimeSpan.Parse(properties["HistoryTimeSpan"]);
                    this.LoadAllHistory = bool.Parse(properties["LoadAllHistory"]);
                    this._triggerSave = true;
                }
            }
            catch (Exception e)
            {
                this.GenerateDefault();
            }
        }
        
        /// <summary>
        /// Saves the config file to disk.
        /// </summary>
        public void Save()
        {
            if (!this._triggerSave) return;

            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(this);

            using (var sw = new StreamWriter("settings.yaml"))
            {
                sw.WriteLine(yaml);
            }
        }

        /// <summary>
        /// Called when properties change to inform any listening event handlers.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Generates a default configuration file.
        /// </summary>
        private void GenerateDefault()
        {
            this.Home = new Url("https://www.hw.ac.uk/");
            this.Database = "sqlite.db";
            this.HistoryTimeSpan = new TimeSpan(1,0,0,0);
            this.LoadAllHistory = false;
            this.Save();
        }

        /// <summary>
        /// The save event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Save(object sender, PropertyChangedEventArgs e)
        {
            this.Save();
        }
    }
}