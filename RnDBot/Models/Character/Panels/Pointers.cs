using Newtonsoft.Json;
using RnDBot.Models.Character.Fields;
using RnDBot.Models.Glossaries;
using RnDBot.Views;

namespace RnDBot.Models.Character.Panels;

/// <summary>
/// Point Counters
/// </summary>
public class Pointers : IPanel, IValidatable
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

    public void SetCorePointers(int? drama = null, int? ability = null, int? body = null, int? will = null, int? armor = null, 
        int? barrier = null)
    {
        if (drama != null) CorePointers.First(p => p.PointerType == PointerType.Drama).Current = drama.GetValueOrDefault();
        if (ability != null) CorePointers.First(p => p.PointerType == PointerType.Ability).Current = ability.GetValueOrDefault();
        if (body != null) CorePointers.First(p => p.PointerType == PointerType.Body).Current = body.GetValueOrDefault();
        if (will != null) CorePointers.First(p => p.PointerType == PointerType.Will).Current = will.GetValueOrDefault();
        if (armor != null) CorePointers.First(p => p.PointerType == PointerType.Armor).Current = armor.GetValueOrDefault();
        if (barrier != null) CorePointers.First(p => p.PointerType == PointerType.Barrier).Current = barrier.GetValueOrDefault();
    }
    
    //TODO Items
    [JsonIgnore]
    public IReadOnlyCollection<Pointer> FinalPointers => CorePointers;
    
    [JsonIgnore]
    public string Title => "Состояния";
    
    [JsonIgnore]
    public List<IField> Fields => FinalPointers.Select(a => (IField) a).ToList();
    
    [JsonIgnore]
    public string Footer => Character.GetFooter;
    
    [JsonIgnore]
    public bool IsValid
    {
        get
        {
            var valid = true;
            var errors = new List<string>();

            var errorPointers = CorePointers.Where(p => p.Current > p.Max).ToList();

            if (errorPointers.Any())
            {
                valid = false;
                
                var attrJoin = String.Join(", ", 
                    errorPointers.Select(p => $"{p.Name} `{p.Current}/{p.Max}`"));
                
                errors.Add($"Значение счетчиков: {attrJoin} – не могут превышать максимальные.");
            }

            Errors = errors.ToArray();
            return valid;
        }
    }

    [JsonIgnore]
    public string[]? Errors { get; private set; }
}