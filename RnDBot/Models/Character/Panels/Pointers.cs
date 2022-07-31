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

    public void SetPointers(int? drama = null, int? ability = null, int? body = null, int? will = null, int? armor = null, 
        int? barrier = null)
    {
        SetPointer(PointerType.Drama, drama);
        SetPointer(PointerType.Ability, ability);
        SetPointer(PointerType.Body, body);
        SetPointer(PointerType.Will, will);
        SetPointer(PointerType.Armor, armor);
        SetPointer(PointerType.Barrier, barrier);
    }

    public void SetPointer(PointerType type, int? value)
    {
        if (value != null) CorePointers.First(p => p.PointerType == type).Current = value.GetValueOrDefault();
    }
    
    //TODO Items
    [JsonIgnore]
    public IReadOnlyCollection<Pointer> FinalPointers
    {
        get
        {
            var result = new List<Pointer>();

            foreach (var pointer in CorePointers.Select(p => new Pointer(p.PointerType, p.Max, p.Current)))
            {
                foreach (var effect in Character.Effects.CoreEffects)
                {
                    effect.ModifyPointer(pointer);
                }
                
                result.Add(pointer);
            }

            return result;
        }
    }

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

            var errorPointers = FinalPointers.Where(p => p.Current > p.Max).ToList();

            if (errorPointers.Any())
            {
                valid = false;
                
                var attrJoin = String.Join(", ", 
                    errorPointers.Select(p => $"{p.Name} `{p.Current}/{p.Max}`"));
                
                errors.Add($"Значение счетчиков: {attrJoin} – не могут превышать максимальные.");
            }
            
            //TODO отрицательного хп быть не должно

            Errors = errors.ToArray();
            return valid;
        }
    }

    [JsonIgnore]
    public string[]? Errors { get; private set; }
}