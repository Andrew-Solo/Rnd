using Microsoft.EntityFrameworkCore;
using RnDApi.Data.Entities;

namespace RnDApi.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    public DbSet<User> Users { get; set; } = null!;
}