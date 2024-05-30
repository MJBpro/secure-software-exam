using Microsoft.EntityFrameworkCore;
using SecureTeamSimulator.Core.Entities;

namespace SecureTeamSimulator.Infrastructure.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User?> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.EncryptionIV).IsRequired();
                entity.Property(e => e.EncryptionKey).IsRequired();
            });
        }
    }
}