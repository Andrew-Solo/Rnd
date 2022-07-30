using Newtonsoft.Json;
using RnDBot.Models.Common;
using RnDBot.Models.Glossaries;
using RnDBot.Views;
using Attribute = RnDBot.Models.Character.Fields.Attribute;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Character.Panels;

public class Attributes : IPanel, IValidatable
{
    public Attributes(ICharacter character)
    {
        Character = character;
        
        CoreAttributes = new List<Attribute>
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

    [JsonConstructor]
    public Attributes(ICharacter character, List<Attribute> coreAttributes)
    {
        Character = character;
        CoreAttributes = coreAttributes;
    }

    [JsonIgnore]
    public ICharacter Character { get; }

    [JsonIgnore]
    public int Level => LevelField.TValue;
    
    [JsonIgnore]
    public int MaxAttribute => (int) Math.Floor((double) Level / 8) + 5;

    [JsonIgnore]
    public TextField<int> LevelField => new("Уровень", CoreAttributes.Sum(a => a.Modifier), false);
    
    [JsonIgnore]
    public CounterField Power => new("Мощь", GetMaxPower(Level), Character.GetPower, false);
    
    //TODO Большой таск на все IField, они должны уметь возвращать свое значение в Math и строку в ToString
    [JsonIgnore]
    public ModifierField Damage => new("Урон", 1 + Level / 16);

    //TODO Индексатор
    public List<Attribute> CoreAttributes { get; private set; }
    
    public void SetCoreAttributes(int str, int end, int dex, int per, int intl, int wis, int cha, int det)
    {
        CoreAttributes = new List<Attribute>
        {
            new(AttributeType.Str, str),
            new(AttributeType.End, end),
            new(AttributeType.Dex, dex),
            new(AttributeType.Per, per),
            new(AttributeType.Int, intl),
            new(AttributeType.Wis, wis),
            new(AttributeType.Cha, cha),
            new(AttributeType.Det, det),
        };
    }
    
    //TODO Items
    [JsonIgnore]
    public List<Attribute> FinalAttributes => CoreAttributes;

    [JsonIgnore]
    public string Title => "Атрибуты";

    [JsonIgnore]
    public List<IField> Fields => new()
    {
        LevelField,
        Power,
        Damage,
        FinalAttributes.First(a => a.AttributeType == AttributeType.Str),
        FinalAttributes.First(a => a.AttributeType == AttributeType.End),
        FinalAttributes.First(a => a.AttributeType == AttributeType.Dex),
        FinalAttributes.First(a => a.AttributeType == AttributeType.Per),
        FinalAttributes.First(a => a.AttributeType == AttributeType.Int),
        FinalAttributes.First(a => a.AttributeType == AttributeType.Wis),
        FinalAttributes.First(a => a.AttributeType == AttributeType.Cha),
        FinalAttributes.First(a => a.AttributeType == AttributeType.Det),
    };

    [JsonIgnore]
    public string Footer => Character.GetFooter;

    private int GetMaxPower(int level) => (int) Math.Floor(Math.Pow(2, (80 + (double) level) / 16));
    
    public bool IsValid
    {
        get
        {
            var valid = true;
            var errors = new List<string>();

            var errorAttributes = CoreAttributes.Where(a => a.Modifier > MaxAttribute).ToList();

            if (errorAttributes.Any())
            {
                valid = false;
                
                var attrJoin = String.Join(", ", 
                    errorAttributes.Select(a => $"{a.Name} {EmbedView.Build(a.Modifier, ValueType.InlineModifier)}"));

                var maxAttr = EmbedView.Build(MaxAttribute, ValueType.InlineModifier);
                
                errors.Add($"Атрибуты: {attrJoin} – не могут превышать значение {maxAttr}");
            }

            if (Power.Current > Power.Max)
            {
                valid = false;
                
                errors.Add($"Лимит мощи превышен на `{Power.Current - Power.Max}`. Мощь: `{Power.Current}/{Power.Max}`.");
            }

            Errors = errors.ToArray();
            return valid;
        }
    }

    public string[]? Errors { get; private set; }
}