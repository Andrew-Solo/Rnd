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
        PointersCurrent = new Dictionary<PointerType, int>
        {
            [PointerType.Body] = PointersMax[PointerType.Body],
            [PointerType.Will] = PointersMax[PointerType.Will],
            [PointerType.Armor] = PointersMax[PointerType.Armor],
            [PointerType.Barrier] = PointersMax[PointerType.Barrier],
            [PointerType.Ability] = PointersMax[PointerType.Ability],
            [PointerType.Drama] = 0,
        };
    }

    [JsonConstructor]
    public Pointers(ICharacter character, Dictionary<PointerType, int> pointersCurrent)
    {
        Character = character;
        PointersCurrent = pointersCurrent;
    }

    [JsonIgnore]
    public ICharacter Character { get; }
    
    public Dictionary<PointerType, int> PointersCurrent { get; }

    [JsonIgnore]
    public IReadOnlyDictionary<PointerType, int> PointersMax => new Dictionary<PointerType, int>()
    {
        [PointerType.Body] = 10 + Character.Attributes.FinalAttributes.First(a => a.AttributeType == AttributeType.End).Modifier,
        [PointerType.Will] = 10 + Character.Attributes.FinalAttributes.First(a => a.AttributeType == AttributeType.Det).Modifier,
        [PointerType.Armor] = 0,
        [PointerType.Barrier] = 0,
        [PointerType.Ability] = Character.Attributes.Power.Max / 10,
        [PointerType.Drama] = 3,
    };
    
    [JsonIgnore]
    public IReadOnlyCollection<Pointer> CorePointers => new List<Pointer>
    {
        GetCorePointer(PointerType.Armor),
        GetCorePointer(PointerType.Barrier),
        GetCorePointer(PointerType.Drama),
        GetCorePointer(PointerType.Body),
        GetCorePointer(PointerType.Will),
        GetCorePointer(PointerType.Ability),
    };

    private Pointer GetCorePointer(PointerType type) => new Pointer(type, PointersMax[type], PointersCurrent[type]);

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
        if (value != null) PointersCurrent[type] = value.GetValueOrDefault();
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