using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Common;

public class ModifierField : IField
{
    public ModifierField(string name, int value)
    {
        Name = name;
        IntValue = value;
    }

    public string Name { get; set; }
    public int IntValue { get; set; }
    public object Value => IntValue;
    public ValueType Type => ValueType.Modifier;
    public bool IsInline => true;
}