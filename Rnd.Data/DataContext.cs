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
        Database.EnsureCreated();
    }
    
    public Modules Modules => new(this, RndModules);
    public Users Users => new(this, RndUsers);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Module>().HasData(PredefinedData.Modules);
        modelBuilder.Entity<User>().HasData(PredefinedData.Users);
        
        modelBuilder.Entity<Field>().Property(u => u.Type).HasConversion<string>();
        modelBuilder.Entity<Field>().Property(u => u.Accessibility).HasConversion<string>();
        modelBuilder.Entity<Field>().Property(u => u.Interactivity).HasConversion<string>();
        modelBuilder.Entity<Field>().Property(u => u.Enumerating).HasConversion<string>();
        modelBuilder.Entity<Method>().Property(u => u.Methodology).HasConversion<string>();
        modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>();
        
        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasMany(x => x.Units)
                .WithOne(x => x.Module)
                .HasForeignKey(x => x.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Main)
                .WithOne()
                .HasForeignKey<Module>(x => x.MainId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Method>(entity =>
        {
            entity.HasMany(x => x.Parameters)
                .WithOne(x => x.Method)
                .HasForeignKey(x => x.MethodId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Return)
                .WithOne()
                .HasForeignKey<Method>(x => x.ReturnId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Space>(entity =>
        {
            entity.HasMany(x => x.Members)
                .WithOne(x => x.Space)
                .HasForeignKey(x => x.SpaceId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Owner)
                .WithOne()
                .HasForeignKey<Space>(x => x.OwnerId)
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