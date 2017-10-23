namespace Browser
{
    using System;
    using System.Windows.Forms;

    using Browser.Config;
    using Browser.Favicon;
    using Browser.Favorites;
    using Browser.History;
    using Browser.Presenters;
    using Browser.Views;

    using SimpleInjector;
    using SimpleInjector.Diagnostics;
    using SimpleInjector.Lifestyles;

    /// <summary>
    /// The main program.
    /// </summary>
    public static class Program
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

            container.Register<IHistory, History.SqliteHistory>(Lifestyle.Singleton);
            container.Register<IFavorites, Favorites.SqliteFavorites>(Lifestyle.Singleton);
            container.Register<IConfig, YamlConfig>(Lifestyle.Singleton);
            container.Register<IFavicon, FaviconCache>(Lifestyle.Singleton);
            container.RegisterSingleton<Func<ITab, IFavorites, IConfig, IFavicon, IHistory, ITabHistory, TabPresenter>>((tab, fav, conf, cache, ghist, thist) => new TabPresenter(tab, fav, conf, cache, ghist, thist));
            container.Register<IBrowser, Browser>();

            container.Register<BrowserPresenter<TabPresenter>>();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BootStrap();

            Application.Run(container.GetInstance<BrowserPresenter<TabPresenter>>().GetForm());
        }
    }
}