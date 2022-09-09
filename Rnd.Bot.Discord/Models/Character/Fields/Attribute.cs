using Newtonsoft.Json;
using Rnd.Bot.Discord.Models.Glossaries;
using Rnd.Bot.Discord.Views;
using ValueType = Rnd.Bot.Discord.Views.ValueType;

namespace Rnd.Bot.Discord.Models.Character.Fields;

public class Attribute : IField
{
    public Attribute(AttributeType attributeType, int modifier)
    {
        AttributeType = attributeType;
        Modifier = modifier;
        Modified = false;
    }
    
    public AttributeType AttributeType { get; set; }
    public int Modifier { get; set; }
    
    [JsonIgnore]
    public bool Modified { get; set; }
    
    [JsonIgnore]
    public string Name => Glossary.AttributeNames[AttributeType] + (Modified ? "*" : "");
    
    [JsonIgnore]
    public ValueType Type => ValueType.Modifier;
    
    [JsonIgnore]
    public object Value => Modifier;
    
    [JsonIgnore]
    public bool IsInline => true;
}