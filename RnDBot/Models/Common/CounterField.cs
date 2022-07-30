using Newtonsoft.Json;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

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
    
    [JsonIgnore]
    public object Value => (Current, Max);
    
    [JsonIgnore]
    public ValueType Type => ValueType.Counter;
    public bool IsInline { get; }
}