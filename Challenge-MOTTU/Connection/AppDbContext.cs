using Challenge_MOTTU.Data.Mappings;
using Challenge_MOTTU.Model;
using Microsoft.EntityFrameworkCore;

namespace Challenge_MOTTU.Connection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pending> Pendings { get; set; }
        public DbSet<Bike> Bikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new PendingMapping());
            modelBuilder.ApplyConfiguration(new BikeMapping());
        }
    }
}
