using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RnDBot.Models.Character;
using RnDBot.Models.Character.Panels;
using RnDBot.Models.Glossaries;
using Attribute = RnDBot.Models.Character.Fields.Attribute;

namespace RnDBot.Data;

[Index("UserId")]
[Index("Name")]
[Index("UserId", "Name", IsUnique = true)]
[Index("Selected")]
public class DataCharacter
{
    public DataCharacter(Guid id, ulong userId, string name, 
        string generalJson, string attributesJson, string pointersJson, string domainsJson)
    {
        Id = id;
        UserId = userId;
        Name = name;
        GeneralJson = generalJson;
        AttributesJson = attributesJson;
        PointersJson = pointersJson;
        DomainsJson = domainsJson;
    }

    public DataCharacter(ulong userId, AncorniaCharacter character, DateTime? selected = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Selected = selected;
        
        Name = character.Name;
        GeneralJson = JsonConvert.SerializeObject(character.General);
        AttributesJson = JsonConvert.SerializeObject(character.Attributes);
        PointersJson = JsonConvert.SerializeObject(character.Pointers);
        DomainsJson = JsonConvert.SerializeObject(character.Domains);
    }

    public Guid Id { get; set; }
    
    public ulong UserId { get; set; }

    [Required]
    public string Name { get; set; }

    public DateTime? Selected { get; set; }

    [Required]
    public string GeneralJson { get; set; }
    
    [Required]
    public string AttributesJson { get; set; }
    
    [Required]
    public string PointersJson { get; set; }
    
    [Required]
    public string DomainsJson { get; set; }
    
    [NotMapped]
    public AncorniaCharacter Character
    {
        get => CharacterFactory.AncorniaCharacter(Name,
            JsonConvert.DeserializeObject<General>(GeneralJson) ?? throw new InvalidOperationException(),
            JsonConvert.DeserializeObject<Attributes>(AttributesJson) ?? throw new InvalidOperationException(),
            JsonConvert.DeserializeObject<Pointers>(PointersJson) ?? throw new InvalidOperationException(),
            JsonConvert.DeserializeObject<Domains<AncorniaDomainType, AncorniaSkillType>>(DomainsJson) 
            ?? throw new InvalidOperationException());
        set
        {
            Name = value.Name;
            GeneralJson = JsonConvert.SerializeObject(value.General);
            AttributesJson = JsonConvert.SerializeObject(value.Attributes);
            PointersJson = JsonConvert.SerializeObject(value.Pointers);
            DomainsJson = JsonConvert.SerializeObject(value.Domains);
        }
    }
}