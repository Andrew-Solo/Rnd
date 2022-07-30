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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataCharacter>().HasData(
            new DataCharacter( 327382594935062529, CharacterFactory.AncorniaCharacter("Рафаэль"), DateTime.Now),
            new DataCharacter( 327382594935062529, CharacterFactory.AncorniaCharacter("Даниэль")),
            new DataCharacter( 327382594935062529, CharacterFactory.AncorniaCharacter("Монте Карло")),
            new DataCharacter( 327382594935062529, CharacterFactory.AncorniaCharacter("Конь"))
        );
    }
}