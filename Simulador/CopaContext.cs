using Microsoft.EntityFrameworkCore;

namespace Simulador
{
    public class CopaContext : DbContext
    {
        public CopaContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ServerConection"));
        }

        public DbSet<Team> Teams { get; set; }
    }
}
