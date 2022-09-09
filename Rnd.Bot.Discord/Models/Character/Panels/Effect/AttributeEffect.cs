using Newtonsoft.Json;
using Rnd.Bot.Discord.Models.Glossaries;
using Rnd.Bot.Discord.Views;
using Attribute = Rnd.Bot.Discord.Models.Character.Fields.Attribute;
using ValueType = Rnd.Bot.Discord.Views.ValueType;

namespace Rnd.Bot.Discord.Models.Character.Panels.Effect;

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
        if (attribute.AttributeType != AttributeType) return;
        
        attribute.Modifier += Modifier;
        attribute.Modified = true;
    }
    
    [JsonIgnore]
    public string View => $"**{Name}** {Glossary.AttributeNames[AttributeType]} " +
                          $"`{EmbedView.Build(Modifier, ValueType.InlineModifier)}`";
}