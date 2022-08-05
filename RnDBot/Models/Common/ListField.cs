using Newtonsoft.Json;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Common;

public class ListField : IField
{
    public ListField(string name, IEnumerable<string>? values = null, bool inline = false)
    {
        Name = name;
        Values = values != null ? new List<string>(values) : new List<string>();
        IsInline = inline;
    }

    public string Name { get; set; }
    public List<string>? Values { get; set; }
    
    [JsonIgnore]
    public object? Value => Values?.ToArray();
    
    [JsonIgnore]
    public ValueType Type => ValueType.List;
    
    [JsonIgnore]
    public bool IsInline { get; }
}