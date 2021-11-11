using Microsoft.EntityFrameworkCore;
using SaitynasLab.Data.Entities;

namespace SaitynasLab.Data
{
    public class DemoRestContext : DbContext
    {
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Part> Parts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=tcp:saitynaslabdbserver.database.windows.net,1433;Initial Catalog=SaitynasLab_db;User Id=paukul@saitynaslabdbserver;Password=Saitynas123");
        }
    }
}
