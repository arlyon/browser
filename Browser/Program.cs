namespace Browser
{
    using System;
    using System.Windows.Forms;

    static class Program
    {
        /// <summary>
        /// The container.
        /// </summary>
        private static Container container;

        /// <summary>
        /// Sets up the dependency injection.
        /// </summary>
        private static void BootStrap()
        {
            container = new Container();
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();

            container.Register<IHistory, History>(Lifestyle.Singleton);
            container.Register<IFavorites, Favorites>(Lifestyle.Singleton);
            container.Register<IConfig, YamlConfig>(Lifestyle.Singleton);
            container.Register<IFaviconCache, FaviconCache>(Lifestyle.Singleton);

            container.Register<IBrowser, Browser>();

            container.Register<BrowserPresenter>(); // IBrowser, IHistory, IFavorites

            // container.Verify();
        }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BootStrap();

            if (!File.Exists("data.db"))
            {
                SQLiteConnection.CreateFile("data.db");
            }

            UpdateSchema();

            Application.Run(container.GetInstance<BrowserPresenter>().GetForm());
        }

        private static void UpdateSchema()
        {
            using (var conn = new SQLiteConnection("data.db"))
            {
                SQLiteCommand history = new SQLiteCommand(
                    "CREATE TABLE IF NOT EXISTS `History` ( `Id` INTEGER, `Date` TEXT, `Url` TEXT, `Title` TEXT )",
                    conn);
                history.ExecuteNonQuery();

                SQLiteCommand favorites = new SQLiteCommand(
                    "CREATE TABLE IF NOT EXISTS `Favorites` ( `Id` INTEGER, `Name` TEXT, `Url` TEXT, `Title` TEXT )",
                    conn);
                favorites.ExecuteNonQuery();
            }
        }
    }
}