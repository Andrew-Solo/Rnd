using Newtonsoft.Json;
using RnDBot.Models.Glossaries;
using RnDBot.Views;
using Attribute = RnDBot.Models.Character.Fields.Attribute;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Character.Panels.Effect;

public class AttributeEffect : IEffect
{
    public AttributeEffect(string name, AttributeType attributeType, int modifier = 0)
    {
        AttributeType = attributeType;
        Name = name;
        Modifier = modifier;
    }

    public string Name { get; }
    public AttributeType AttributeType { get; }
    public int Modifier { get; }
    
    public void ModifyAttribute(Attribute attribute)
    {
        if (attribute.AttributeType == AttributeType) attribute.Modifier += Modifier;
    }
    
    [JsonIgnore]
    public string View => $"**{Name}** {Glossary.AttributeNames[AttributeType]} " +
                          $"`{EmbedView.Build(Modifier, ValueType.InlineModifier)}`";
}