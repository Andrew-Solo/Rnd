using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RnDBot.Models.Character;

namespace RnDBot.Data;

[Index(nameof(GuildId))]
[Index(nameof(GuildId), nameof(PlayerId))]
[Index(nameof(GuildId), nameof(PlayerId), nameof(Selected))]
[Index(nameof(GuildId), nameof(PlayerId), nameof(GuideId), nameof(Selected))]
[Index(nameof(GuildId), nameof(PlayerId), nameof(Name), IsUnique = true)]
public class DataCharacter
{
    public DataCharacter(Guid id, ulong guildId, ulong playerId, bool isLocked, ulong? guideId, DateTime? selected, string name, string characterJson)
    {
        Id = id;
        GuildId = guildId;
        PlayerId = playerId;
        GuideId = guideId;
        IsLocked = isLocked;
        Selected = selected;

        Name = name;
        CharacterJson = characterJson;
    }

    public DataCharacter(ulong playerId, ulong guildId, AncorniaCharacter character, DateTime? selected = null, ulong? guideId = null, bool isLocked = false)
    {
        Id = Guid.NewGuid();
        PlayerId = playerId;
        GuildId = guildId;
        IsLocked = isLocked;
        GuideId = guideId;
        Selected = selected;
        
        Name = character.Name;
        CharacterJson = JsonConvert.SerializeObject(character);
    }

    public Guid Id { get; set; }
    
    public ulong GuildId { get; set; }
    
    public ulong PlayerId { get; set; }
    
    public bool IsLocked { get; set; }
    
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