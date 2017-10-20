namespace Browser
{
    using Browser.Favorites;
    using Browser.History;
    using Browser.Requests;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The data context.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Gets or sets the history.
        /// </summary>
        public DbSet<HistoryLocation> History { get; set; }

        /// <summary>
        /// Gets or sets the favorites.
        /// </summary>
        public DbSet<FavoritesLocation> Favorites { get; set; }

        /// <summary>
        /// Gets or sets the urls.
        /// </summary>
        public DbSet<Url> Urls { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        public DataContext()
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Sets the database in the OnConfiguring stage to use sqlite.
        /// </summary>
        /// <param name="optionsBuilder">
        /// The options builder.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=sqlite.db");
        }

        /// <summary>
        /// Run when the model is being build to set the Primary Keys.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
