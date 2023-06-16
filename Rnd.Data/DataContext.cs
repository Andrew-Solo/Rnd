using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Models.Nodes;
using Rnd.Models.Nodes.Fields;
using Rnd.Models.Nodes.Methods;
#pragma warning disable CS8618

// EF Proxies
#pragma warning disable CS0649
#pragma warning disable CS0169

namespace Rnd.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<User> Users => _users;
    public DbSet<ObjectField> ObjectFields => _objectFields;
    public DbSet<FunctionMethod> FunctionMethods => _functionMethods;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>();
        
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
        
        modelBuilder.Entity<Field>().Property(u => u.Accessibility).HasConversion<string>();
        modelBuilder.Entity<Field>().Property(u => u.Interactivity).HasConversion<string>();
        modelBuilder.Entity<Field>().Property(u => u.Enumerating).HasConversion<string>();
        
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
    }

    private DbSet<User> _users;
    private DbSet<Space> _spaces;
    private DbSet<Member> _members;
    private DbSet<Group> _groups;
    private DbSet<Plugin> _plugins;
    private DbSet<Module> _modules;
    private DbSet<Unit> _units;
    private DbSet<Field> _fields;
    private DbSet<Method> _methods;
    private DbSet<ObjectField> _objectFields;
    private DbSet<FunctionMethod> _functionMethods;
    private DbSet<ActionMethod> _actionMethods;
}