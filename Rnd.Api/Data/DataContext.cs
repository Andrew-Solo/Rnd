using Microsoft.EntityFrameworkCore;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<Field> Fields { get; set; } = null!;
    public DbSet<Parameter> Parameters { get; set; } = null!;
    public DbSet<Resource> Resource { get; set; } = null!;
    public DbSet<Effect> Effects { get; set; } = null!;
    public DbSet<ParameterEffect> ParameterEffects { get; set; } = null!;
    public DbSet<ResourceEffect> ResourceEffects { get; set; } = null!;

    public void SetAddedState(IEntity entity)
    {
        Entry(entity).State = EntityState.Added;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().Property(m => m.Role).HasConversion<string>();
        modelBuilder.Entity<Field>().Property(m => m.Type).HasConversion<string>();
    }
}