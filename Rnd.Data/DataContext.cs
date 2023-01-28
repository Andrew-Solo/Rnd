using Microsoft.EntityFrameworkCore;
using Rnd.Data.Repositories;
using Rnd.Models;

// EF Proxies
#pragma warning disable CS8618

namespace Rnd.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public Games Games => new(this, GamesData);
    public Members Members => new(this, MembersData);
    public Users Users => new(this, UsersData);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().Property(m => m.Role).HasConversion<string>();
    }
    
    private DbSet<Game> GamesData { get; set; }
    private DbSet<Member> MembersData { get; set; }
    private DbSet<User> UsersData { get; set; }
}