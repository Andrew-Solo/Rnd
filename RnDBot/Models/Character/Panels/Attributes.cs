using Newtonsoft.Json;
using RnDBot.Models.Common;
using RnDBot.Models.Glossaries;
using RnDBot.Views;
using Attribute = RnDBot.Models.Character.Fields.Attribute;

namespace RnDBot.Models.Character.Panels;

public class Attributes : IPanel
{
    public Attributes(ICharacter character, int level, List<Attribute>? coreAttributes = null)
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

        LevelField = new TextField<int>("Уровень", level, false);
        //TODO считать макс мощь
        Power = new CounterField("Мощь", 32, 0, false);
    }

    [JsonConstructor]
    public Attributes(ICharacter character, TextField<int> levelField, CounterField power, List<Attribute> coreAttributes)
    {
        Character = character;
        LevelField = levelField;
        Power = power;
        CoreAttributes = coreAttributes;
    }

    [JsonIgnore]
    public ICharacter Character { get; }

    [JsonIgnore]
    public int Level 
    { 
        get => LevelField.TValue;
        set => LevelField.TValue = value;
    }
    
    public TextField<int> LevelField { get; }
    public CounterField Power { get; }
    
    //TODO Большой таск на все IField, они должны уметь возвращать свое значение в Math и строку в ToString
    [JsonIgnore]
    public ModifierField Damage => new("Урон", 1 + Level / 16);

    //TODO Индексатор
    public List<Attribute> CoreAttributes { get; }
    
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
}