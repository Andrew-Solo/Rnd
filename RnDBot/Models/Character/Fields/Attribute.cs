using Newtonsoft.Json;
using RnDBot.Models.Glossaries;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Character.Fields;

public class Attribute : IField
{
    public Attribute(AttributeType attributeType, int value)
    {
        AttributeType = attributeType;
        Modifier = value;
    }
    
    public AttributeType AttributeType { get; set; }
    public int Modifier { get; set; }
    
    [JsonIgnore]
    public string Name => Glossary.AttributeNames[AttributeType];
    
    [JsonIgnore]
    public ValueType Type => ValueType.Modifier;
    
    [JsonIgnore]
    public object Value => Modifier;
    
    [JsonIgnore]
    public bool IsInline => true;
}