using Microsoft.EntityFrameworkCore;
using RnDBot.Models.Character;

namespace RnDBot.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }

    public DbSet<DataCharacter> Characters { get; set; } = null!;
}