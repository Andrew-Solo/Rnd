using RnDBot.Models.Glossaries;
using RnDBot.View;
using Attribute = RnDBot.Models.CharacterFields.Attribute;

namespace RnDBot.Models.Character;

public class Attributes : IPanel
{
    public Attributes(Character character, List<Attribute>? coreAttributes = null)
    {
        Character = character;
        
        CoreAttributes = coreAttributes ?? new List<Attribute>
        {
            new(AttributeType.Str, 0),
            new(AttributeType.End, 0),
            new(AttributeType.Dex, 0),
            new(AttributeType.Per, 0),
            new(AttributeType.Int, 0),
            new(AttributeType.Wis, 0),
            new(AttributeType.Cha, 0),
            new(AttributeType.Det, 0),
        };
    }

    public Character Character { get; }
    public List<Attribute> CoreAttributes { get; }
    
    //TODO Items
    public List<Attribute> FinalAttributes => CoreAttributes;

    public string Title => "Атрибуты";
    public List<IField> Fields => FinalAttributes.Select(a => (IField) a).ToList();
    public string Footer => Character.General.Name;
}