using RnDBot.View;
using ValueType = RnDBot.View.ValueType;

namespace RnDBot.Models.Common;

public class NumberField : IField
{
    public NumberField(string name, int? number)
    {
        Name = name;
        Number = number;
    }

    public string Name { get; set; }
    public int? Number { get; set; }
    public object? Value => Number?.ToString();
    public ValueType Type => ValueType.Mono;
    public bool IsInline => true;
}