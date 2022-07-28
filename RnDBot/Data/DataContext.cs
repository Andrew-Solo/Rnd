using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RnDBot.Models.Character;
using RnDBot.Models.Character.Fields;
using RnDBot.Models.Character.Panels;
using RnDBot.Models.Glossaries;

namespace RnDBot.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<DataCharacter> Characters { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataCharacter>().HasData(
            new DataCharacter( 1, CharacterFactory.AncorniaCharacter("Рафаэль"), DateTime.Now),
            new DataCharacter( 1, CharacterFactory.AncorniaCharacter("Даниэль")),
            new DataCharacter( 1, CharacterFactory.AncorniaCharacter("Монте Карло")),
            new DataCharacter( 1, CharacterFactory.AncorniaCharacter("Конь"))
        );
    }
}