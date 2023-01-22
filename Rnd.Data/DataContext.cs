using Microsoft.EntityFrameworkCore;
using Rnd.Models;

// EF Proxies
#pragma warning disable CS8618

namespace Rnd.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    public DbSet<Game> Games { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().Property(m => m.Role).HasConversion<string>();
    }
}