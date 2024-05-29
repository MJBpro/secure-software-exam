using Microsoft.EntityFrameworkCore;
using SecureTeamSimulator.Core.Entities;

namespace SecureTeamSimulator.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> TodoItems { get; set; }

   
}