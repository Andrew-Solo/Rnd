namespace Rnd.Bot.Discord.Models.Character;

public interface IValidatable
{
    bool IsValid { get; }
    string[]? Errors { get; }
}