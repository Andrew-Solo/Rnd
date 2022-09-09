using Newtonsoft.Json;
using Rnd.Bot.Discord.Views;
using ValueType = Rnd.Bot.Discord.Views.ValueType;

namespace Rnd.Bot.Discord.Models.Common;

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