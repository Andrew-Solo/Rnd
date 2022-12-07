using Microsoft.EntityFrameworkCore;
using Rnd.Api.Data.Entities;
using Type = Rnd.Api.Data.Entities.Type;

namespace Rnd.Api.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }
    
    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<CharacterDefinition> CharacterDefinitions { get; set; } = null!;
    public DbSet<CharacterInstance> CharacterInstances { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<Module> Modules { get; set; } = null!;
    public DbSet<ParameterDefinition> ParameterDefinitions { get; set; } = null!;
    public DbSet<ParameterInstance> ParameterInstances { get; set; } = null!;
    public DbSet<Type> Types { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().Property(m => m.Role).HasConversion<string>();
        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<ParameterDefinition>(entity =>
            entity.HasOne(x => x.Type)
                .WithMany(x => x.UsingParameters)
                .HasForeignKey(x => x.TypeId)
        );
        modelBuilder.Entity<Type>(entity =>
            entity.HasMany(x => x.Parameters)
                .WithMany(x => x.UsingTypes)
        );
    }
}