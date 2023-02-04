using Microsoft.EntityFrameworkCore;
using Rnd.Data.Repositories;
using Rnd.Models;

// EF Proxies
// ReSharper disable UnusedAutoPropertyAccessor.Local
#pragma warning disable CS8618

namespace Rnd.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public Characters Characters => new(this, RndCharacters);
    public Games Games => new(this, RndGames);
    public Members Members => new(this, RndMembers);
    public Modules Modules => new(this, RndModules);
    public Tokens Tokens => new(this, RndTokens);
    public Units Units => new(this, RndUnits);
    public Users Users => new(this, RndUsers);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>();
        modelBuilder.Entity<Member>().Property(m => m.Role).HasConversion<string>();
        modelBuilder.Entity<Unit>().Property(u => u.Access).HasConversion<string>();
        modelBuilder.Entity<Unit>().Property(u => u.Type).HasConversion<string>();
        modelBuilder.Entity<Unit>().Property(u => u.ChildrenType).HasConversion<string>();
        modelBuilder.Entity<Unit>().Property(u => u.Role).HasConversion<string>();
        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
    }
    
    private DbSet<Character> RndCharacters { get; set; }
    private DbSet<Game> RndGames { get; set; }
    private DbSet<Member> RndMembers { get; set; }
    private DbSet<Module> RndModules { get; set; }
    private DbSet<Token> RndTokens { get; set; }
    private DbSet<Unit> RndUnits { get; set; }
    private DbSet<User> RndUsers { get; set; }
}