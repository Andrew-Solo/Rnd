using RnDBot.Models.Glossaries;
using RnDBot.View;
using Attribute = RnDBot.Models.CharacterFields.Attribute;

namespace RnDBot.Models.Character;

public class Attributes : IPanel
{
    public Attributes(Character character, List<Attribute>? coreAttributes = null)
    {
        Character = character;
        
        CoreAttributes = coreAttributes ?? new()
        {
            new Attribute(AttributeType.Str, 0),
            new Attribute(AttributeType.End, 0),
            new Attribute(AttributeType.Dex, 0),
            new Attribute(AttributeType.Per, 0),
            new Attribute(AttributeType.Int, 0),
            new Attribute(AttributeType.Wis, 0),
            new Attribute(AttributeType.Cha, 0),
            new Attribute(AttributeType.Det, 0),
        };
        
        FinalAttributes = coreAttributes ?? CoreAttributes;
    }

    public Character Character { get; }
    public List<Attribute> CoreAttributes { get; }
    public List<Attribute> FinalAttributes { get; }

    public string Title => "Атрибуты";
    public List<IField> Fields => FinalAttributes.Select(a => (IField) a).ToList();
    public string Footer => Character.General.Name;
}