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
    public List<Attribute> CoreAttributes { get; }
    
    public void SetCoreAttributes(int? str = null, int? end = null, int? dex = null, int? per = null, int? intl = null, int? wis = null, 
        int? cha = null, int? det = null)
    {
        if (str != null) CoreAttributes.First(p => p.AttributeType == AttributeType.Str).Modifier = str.GetValueOrDefault();
        if (end != null) CoreAttributes.First(p => p.AttributeType == AttributeType.End).Modifier = end.GetValueOrDefault();
        if (dex != null) CoreAttributes.First(p => p.AttributeType == AttributeType.Dex).Modifier = dex.GetValueOrDefault();
        if (per != null) CoreAttributes.First(p => p.AttributeType == AttributeType.Per).Modifier = str.GetValueOrDefault();
        if (intl != null) CoreAttributes.First(p => p.AttributeType == AttributeType.Int).Modifier = intl.GetValueOrDefault();
        if (wis != null) CoreAttributes.First(p => p.AttributeType == AttributeType.Wis).Modifier = wis.GetValueOrDefault();
        if (cha != null) CoreAttributes.First(p => p.AttributeType == AttributeType.Cha).Modifier = cha.GetValueOrDefault();
        if (det != null) CoreAttributes.First(p => p.AttributeType == AttributeType.Det).Modifier = det.GetValueOrDefault();
    }
    
    //TODO Items
    [JsonIgnore]
    public IReadOnlyCollection<Attribute> FinalAttributes
    {
        get
        {
            var result = new List<Attribute>();

            foreach (var attribute in CoreAttributes.Select(a => new Attribute(a.AttributeType, a.Modifier)))
            {
                foreach (var effect in Character.Effects.CoreEffects)
                {
                    effect.ModifyAttribute(attribute);
                }
                
                result.Add(attribute);
            }

            return result;
        }
    }

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
    
    [JsonIgnore]
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

    [JsonIgnore]
    public string[]? Errors { get; private set; }
}