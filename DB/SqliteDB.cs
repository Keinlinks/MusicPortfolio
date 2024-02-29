using Microsoft.EntityFrameworkCore;
using PortafolioBack.Model;

namespace PortafolioBack.DB
{
    public class SqliteDB : DbContext
    {
        public SqliteDB(DbContextOptions<SqliteDB> options) : base(options) {
            Database.EnsureCreated();
        }


        public DbSet<Song> SongI { get; set; }

    }
}
