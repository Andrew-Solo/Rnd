using RnDBot.View;
using ValueType = RnDBot.View.ValueType;

namespace RnDBot.Models.Common;

public class CounterField : IField
{
    public CounterField(string name, int max, int? current = null, bool isInline = true)
    {
        Name = name;
        Max = max;
        Current = current ?? max;
        IsInline = isInline;
    }

    public int Max { get; set; }
    public int Current { get; set; }
    
    public string Name { get; set; }
    public object Value => (Current, Max);
    public ValueType Type => ValueType.Counter;
    public bool IsInline { get; }
}