using Newtonsoft.Json;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Common;

public class ListField : IField
{
    public ListField(string name, IEnumerable<string>? values = null)
    {
        Name = name;
        Values = values != null ? new List<string>(values) : new List<string>();
    }

    public string Name { get; set; }
    public List<string>? Values { get; set; }
    
    [JsonIgnore]
    public object? Value => Values?.ToArray();
    
    [JsonIgnore]
    public ValueType Type => ValueType.List;
    
    [JsonIgnore]
    public bool IsInline => false;
}