using RnDBot.Models.Glossaries;
using RnDBot.View;
using ValueType = RnDBot.View.ValueType;

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

    public string Name => Glossary.AttributeNames[AttributeType];
    public ValueType Type => ValueType.Modifier;
    public object Value => Modifier;
    public bool IsInline => true;
}