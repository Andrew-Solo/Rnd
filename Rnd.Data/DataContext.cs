using Microsoft.EntityFrameworkCore;
using Rnd.Data.Repositories;
using Rnd.Models;
using Rnd.Models.Nodes;

// EF Proxies

namespace Rnd.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }
    
    public Modules Modules => new(this, RndModules);
    public Users Users => new(this, RndUsers);
    public Units Units => new(this, RndUnits);
    public Fields Fields => new(this, RndFields);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Module>().HasData(PredefinedData.Modules);
        modelBuilder.Entity<Unit>().HasData(PredefinedData.Units);
        modelBuilder.Entity<User>().HasData(PredefinedData.Users);
        modelBuilder.Entity<Field>().HasData(PredefinedData.Fields);
        
        modelBuilder.Entity<Field>().Property(u => u.Type).HasConversion<string>();
        modelBuilder.Entity<Field>().Property(u => u.Accessibility).HasConversion<string>();
        modelBuilder.Entity<Field>().Property(u => u.Interactivity).HasConversion<string>();
        modelBuilder.Entity<Field>().Property(u => u.Enumerating).HasConversion<string>();
        modelBuilder.Entity<Method>().Property(u => u.Methodology).HasConversion<string>();
        modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>();
        
        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasMany(e => e.Units)
                .WithOne(e => e.Module)
                .HasForeignKey(e => e.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Main)
                .WithOne()
                .HasForeignKey<Module>(e => e.MainId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.Version).HasConversion(
                v => v.ToString(),
                v => new Version(v)
            );
        });
        
        modelBuilder.Entity<Method>(entity =>
        {
            entity.HasMany(e => e.Parameters)
                .WithOne(e => e.Method)
                .HasForeignKey(e => e.MethodId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Return)
                .WithOne()
                .HasForeignKey<Method>(e => e.ReturnId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Space>(entity =>
        {
            entity.HasMany(e => e.Members)
                .WithOne(e => e.Space)
                .HasForeignKey(e => e.SpaceId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Owner)
                .WithOne()
                .HasForeignKey<Space>(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
    
    private DbSet<Module> RndModules => Set<Module>();
    private DbSet<Unit> RndUnits => Set<Unit>();
    private DbSet<Field> RndFields => Set<Field>();
    private DbSet<Method> RndMethods => Set<Method>();
    private DbSet<Instance> RndInstances => Set<Instance>();
    private DbSet<User> RndUsers => Set<User>();
    private DbSet<Space> RndSpaces => Set<Space>();
    private DbSet<Member> RndMembers => Set<Member>();
    private DbSet<Group> RndGroups => Set<Group>();
}