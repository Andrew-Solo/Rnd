namespace RnDBot.Models.Character;

public interface IValidatable
{
    bool IsValid { get; }
    string[]? Errors { get; }
}