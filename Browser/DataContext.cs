namespace Browser
{
    using Browser.Favorites;
    using Browser.History;

    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DbSet<HistoryLocation> Blogs { get; set; }
        public DbSet<FavoritesLocation> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=sqlite.db");
        }
    }
}
