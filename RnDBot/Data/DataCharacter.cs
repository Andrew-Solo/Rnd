using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RnDBot.Models.Character;

namespace RnDBot.Data;

[Index("UserId")]
[Index("Name")]
[Index("UserId", "Name", IsUnique = true)]
[Index("Selected")]
public class DataCharacter
{
    public DataCharacter(Guid id, ulong userId, string name, string characterJson)
    {
        Id = id;
        UserId = userId;
        Name = name;
        CharacterJson = characterJson;
    }

    public DataCharacter(ulong userId, AncorniaCharacter character, DateTime? selected = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Selected = selected;
        
        Name = character.Name;
        CharacterJson = JsonConvert.SerializeObject(character);
    }

    public Guid Id { get; set; }
    
    public ulong UserId { get; set; }

    [Required]
    public string Name { get; set; }

    public DateTime? Selected { get; set; }

    [Required]
    public string CharacterJson { get; set; }
    
    [NotMapped]
    public AncorniaCharacter Character
    {
        get => JsonConvert.DeserializeObject<AncorniaCharacter>(CharacterJson) ?? throw new InvalidOperationException();
        set
        {
            Name = value.Name;
            CharacterJson = JsonConvert.SerializeObject(value);
        }
    }
}