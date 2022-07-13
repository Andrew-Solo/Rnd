using RnDBot.View;
using ValueType = RnDBot.View.ValueType;

namespace RnDBot.Models.Common;

public class ListField : IField
{
    public ListField(string name, List<string>? values = null)
    {
        Name = name;
        Values = values;
    }

    public string Name { get; set; }
    public List<string>? Values { get; }
    public object? Value => Values?.ToArray();
    public ValueType Type => ValueType.List;
    public bool IsInline => false;
}