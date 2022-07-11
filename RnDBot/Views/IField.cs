namespace RnDBot.View;

public interface IField
{
    string Name { get; }
    object? Value { get; }
    ValueType Type { get; }
    bool IsInline { get; }
}