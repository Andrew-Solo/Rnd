using Newtonsoft.Json;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Common;

public class TextField<T> : IField
{
    public TextField(string name, T value, bool isInline = true)
    {
        Name = name;
        TValue = value;
        IsInline = isInline;
    }

    public string Name { get; set; }
    // ReSharper disable once InconsistentNaming
    public T TValue { get; set; }
    
    
    [JsonIgnore]
    public object? Value => TValue?.ToString();
    
    [JsonIgnore]
    public ValueType Type => ValueType.Text;
    
    public bool IsInline { get; }

    public override string ToString() => TValue?.ToString() ?? "–";
}