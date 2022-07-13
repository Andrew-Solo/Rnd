using RnDBot.View;
using ValueType = RnDBot.View.ValueType;

namespace RnDBot.Models.Common;

public class TextField : IField
{
    public TextField(string name, string? text)
    {
        Name = name;
        Text = text;
    }

    public string Name { get; set; }
    public string? Text { get; set; }
    public object? Value => Text;
    public ValueType Type => ValueType.Text;
    public bool IsInline => true;
}