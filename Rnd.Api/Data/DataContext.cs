using Microsoft.EntityFrameworkCore;
using Rnd.Api.Data.Entities;
using Attribute = Rnd.Api.Data.Entities.Attribute;

namespace Rnd.Api.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<Attribute> Attributes { get; set; } = null!;
    public DbSet<Domain> Domains { get; set; } = null!;
    public DbSet<Skill> Skills { get; set; } = null!;
    public DbSet<State> States { get; set; } = null!;
    public DbSet<Parameter> Parameters { get; set; } = null!;
    public DbSet<Resource> Resource { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().Property(m => m.Role).HasConversion<string>();
        modelBuilder.Entity<Attribute>().Property(m => m.Type).HasConversion<string>();
        modelBuilder.Entity<Domain>().Property(m => m.Type).HasConversion<string>();
        modelBuilder.Entity<Skill>().Property(m => m.Type).HasConversion<string>();
        modelBuilder.Entity<State>().Property(m => m.Type).HasConversion<string>();
    }
}