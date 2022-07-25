using RnDBot.View;
using ValueType = RnDBot.View.ValueType;

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
    public object Value => IntValue.ToString();
    public ValueType Type => ValueType.Text;
    public bool IsInline => true;
}