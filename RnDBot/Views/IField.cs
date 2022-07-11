namespace RnDBot.View;

public interface IField
{
    string Name { get; }
    object? Value  => null;
    ValueType Type => ValueType.Text;
    bool IsInline => false;
}