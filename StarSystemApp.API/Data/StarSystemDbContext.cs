using Microsoft.EntityFrameworkCore;
using StarSystemApp.API.Models;

namespace StarSystemApp.API.Data
{
    public class StarSystemDbContext : DbContext
    {
        public StarSystemDbContext(DbContextOptions<StarSystemDbContext> dbContext)
            : base(dbContext) { }


        public DbSet<StarSystem> StarSystems { get; set; }
        public DbSet<SpaceObject> SpaceObjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=pokonch811");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StarSystem>()
                .HasMany(s => s.SpaceObjects)
                .WithOne(o => o.StarSystem)
                .HasForeignKey(o => o.StarSystemId);

            modelBuilder.Entity<StarSystem>()
                .HasOne(s => s.MassCenter)
                .WithOne()
                .HasForeignKey<StarSystem>(s => s.MassCenterId);
        }
    }
}
