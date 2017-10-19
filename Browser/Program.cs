namespace Browser
{
    using System;
    using System.Windows.Forms;

    using Browser.Cache;
    using Browser.Config;
    using Browser.Favorites;
    using Browser.History;
    using Browser.Presenters;
    using Browser.Views;

    using SimpleInjector;
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

            container.Register<IHistory, History.History>(Lifestyle.Singleton);
            container.Register<IFavorites, Favorites.Favorites>(Lifestyle.Singleton);
            container.Register<IConfig, YamlConfig>(Lifestyle.Singleton);
            container.Register<IFaviconCache, FaviconCache>(Lifestyle.Singleton);

            container.Register<IBrowser, Browser>();

            container.Register<BrowserPresenter>(); // IBrowser, IHistory, IFavorites

            // container.Verify();
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

            Application.Run(container.GetInstance<BrowserPresenter>().GetForm());
        }
    }
}