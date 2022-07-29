using Newtonsoft.Json;
using RnDBot.Models.Character.Panels;
using RnDBot.Views;

namespace RnDBot.Models.Character;

public class AbstractCharacter : ICharacter
{
    public AbstractCharacter(ICharacter character)
    {
        Name = character.Name;
        General = character.General;
        Attributes = character.Attributes;
        Pointers = character.Pointers;
    }
    
    public AbstractCharacter(string name, int level = 0)
    {
        Name = name;
        General = new General(this);
        Attributes = new Attributes(this, level);
        Pointers = new Pointers(this);
    }

    [JsonConstructor]
    public AbstractCharacter(string name, General general, Attributes attributes, Pointers pointers)
    {
        Name = name;
        General = new General(this, general.Description, general.Culture, general.Age, general.Ideals, general.Vices, general.Traits);
        Attributes = new Attributes(this, attributes.LevelField, attributes.Power, attributes.CoreAttributes);
        Pointers = new Pointers(this, pointers.CorePointers);
    }

    public string Name { get; }
    public General General { get; }
    public Attributes Attributes { get; }
    public Pointers Pointers { get; }

    [JsonIgnore]
    public virtual List<IPanel> Panels => new()
    {
        General,
        Pointers,
        Attributes,
    };
}