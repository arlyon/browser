namespace Browser.Config
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    using Browser.Requests;

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
        /// Initializes a new instance of the <see cref="YamlConfig"/> class. 
        /// </summary>
        public YamlConfig()
        {
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
                    var properties = deserializer.Deserialize<Dictionary<string, string>>(input);
                    this.HomeUrl = properties["HomeUrl"];
                    this.Database = properties["Database"];
                }
            }
            catch (IOException)
            {
                this.GenerateDefault();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Saves the config file to disk.
        /// </summary>
        public void Save()
        {
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
        [NotifyPropertyChangedInvocator]
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