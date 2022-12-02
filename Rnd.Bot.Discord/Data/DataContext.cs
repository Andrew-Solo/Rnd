using Microsoft.EntityFrameworkCore;

namespace Rnd.Bot.Discord.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }

    public DbSet<Account> Accounts { get; set; } = null!;
}