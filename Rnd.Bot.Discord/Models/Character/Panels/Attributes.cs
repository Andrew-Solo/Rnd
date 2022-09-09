using Newtonsoft.Json;
using Rnd.Bot.Discord.Models.Common;
using Rnd.Bot.Discord.Models.Glossaries;
using Rnd.Bot.Discord.Views;
using Attribute = Rnd.Bot.Discord.Models.Character.Fields.Attribute;
using ValueType = Rnd.Bot.Discord.Views.ValueType;

namespace Rnd.Bot.Discord.Models.Character.Panels;

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
    public CounterField Power
    {
        get
        {
            var result = new CounterField("Мощь", MaxPower, Character.GetPower, false);

            foreach (var effect in Character.Effects.FinalEffects)
            {
                effect.ModifyPower(result);
            }
            
            return result;
        }
    }

    //TODO Большой таск на все IField, они должны уметь возвращать свое значение в Math и строку в ToString
    [JsonIgnore]
    public ModifierField Damage => new("Урон", 1 + Level / 16);
    
    public List<Attribute> CoreAttributes { get; }
    
    public void SetAttributes(int? str = null, int? end = null, int? dex = null, int? per = null, int? intl = null, int? wis = null, 
        int? cha = null, int? det = null)
    {
        SetAttribute(AttributeType.Str, str);
        SetAttribute(AttributeType.End, end);
        SetAttribute(AttributeType.Dex, dex);
        SetAttribute(AttributeType.Per, per);
        SetAttribute(AttributeType.Int, intl);
        SetAttribute(AttributeType.Wis, wis);
        SetAttribute(AttributeType.Cha, cha);
        SetAttribute(AttributeType.Det, det);
    }

    public void SetAttribute(AttributeType type, int? value)
    {
        var corePointers = Character.Pointers.CorePointers;

        if (value != null) CoreAttributes.First(p => p.AttributeType == type).Modifier = value.GetValueOrDefault();

        Character.Pointers.UpdateCurrentPoints(corePointers, false);
    }
    
    [JsonIgnore]
    public IReadOnlyCollection<Attribute> FinalAttributes
    {
        get
        {
            var result = new List<Attribute>();

            foreach (var attribute in CoreAttributes.Select(a => new Attribute(a.AttributeType, a.Modifier)))
            {
                foreach (var effect in Character.Effects.FinalEffects)
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

    [JsonIgnore] 
    public int MaxPower => GetMaxPower(Level);
    private int GetMaxPower(int level) => (int) Math.Round(Math.Pow(2, (80 + (double) level) / 16));
    
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