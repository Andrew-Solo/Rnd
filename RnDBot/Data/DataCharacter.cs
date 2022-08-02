using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RnDBot.Models.Character;

namespace RnDBot.Data;

[Index(nameof(PlayerId))]
[Index(nameof(PlayerId), nameof(Selected))]
[Index(nameof(PlayerId), nameof(GuideId), nameof(Selected))]
[Index(nameof(PlayerId), nameof(Name), IsUnique = true)]
public class DataCharacter
{
    public DataCharacter(Guid id, ulong playerId, string playerName, string name, string characterJson, DateTime? selected, ulong? guideId)
    {
        Id = id;
        PlayerId = playerId;
        PlayerName = playerName;
        Selected = selected;
        GuideId = guideId;

        Name = name;
        CharacterJson = characterJson;
    }

    public DataCharacter(ulong playerId, string playerName, AncorniaCharacter character, DateTime? selected = null, ulong? guideId = null)
    {
        Id = Guid.NewGuid();
        PlayerId = playerId;
        PlayerName = playerName;
        Selected = selected;
        GuideId = guideId;
        
        Name = character.Name;
        CharacterJson = JsonConvert.SerializeObject(character);
    }

    public Guid Id { get; }
    
    public ulong PlayerId { get; }
    
    public string PlayerName { get; }
    
    public ulong? GuideId { get; set; }

    public DateTime? Selected { get; set; }

    [Required]
    public string Name { get; set; }

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