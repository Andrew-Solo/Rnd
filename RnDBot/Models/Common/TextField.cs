using RnDBot.View;
using ValueType = RnDBot.View.ValueType;

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
    public object? Value => TValue?.ToString();
    public ValueType Type => ValueType.Text;
    public bool IsInline { get; }
}