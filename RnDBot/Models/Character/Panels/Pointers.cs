using Newtonsoft.Json;
using RnDBot.Models.Character.Fields;
using RnDBot.Models.Glossaries;
using RnDBot.Views;

namespace RnDBot.Models.Character.Panels;

/// <summary>
/// Point Counters
/// </summary>
public class Pointers : IPanel
{
    public Pointers(ICharacter character)
    {
        Character = character;

        var ap = Character.Attributes.Power.Max / 10;
        var end = Character.Attributes.FinalAttributes.First(a => a.AttributeType == AttributeType.End).Modifier;
        var det = Character.Attributes.FinalAttributes.First(a => a.AttributeType == AttributeType.Det).Modifier;
        
        CorePointers = new List<Pointer>
        {
            new(PointerType.Armor, 0),
            new(PointerType.Barrier, 0),
            new(PointerType.Drama, 3, 0),
            new(PointerType.Body, 10 + end),
            new(PointerType.Will, 10 + det),
            new(PointerType.Ability, ap),
        };
    }

    [JsonConstructor]
    public Pointers(ICharacter character, List<Pointer> corePointers)
    {
        Character = character;
        CorePointers = corePointers;
    }

    [JsonIgnore]
    public ICharacter Character { get; }
    
    //TODO Индексатор
    public List<Pointer> CorePointers { get; }
    
    //TODO Items
    [JsonIgnore]
    public List<Pointer> FinalPointers => CorePointers;
    
    [JsonIgnore]
    public string Title => "Состояния";
    
    [JsonIgnore]
    public List<IField> Fields => FinalPointers.Select(a => (IField) a).ToList();
    
    [JsonIgnore]
    public string Footer => Character.GetFooter;
}