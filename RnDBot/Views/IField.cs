namespace RnDBot.Views;

public interface IField
{
    string Name { get; }
    object? Value { get; }
    ValueType Type { get; }
    bool IsInline { get; }
}