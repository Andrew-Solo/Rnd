namespace RnDBot.Models.Character;

public interface ICharacter
{
    public General General { get; }
    public Leveling Leveling { get; }
    public Conditions Conditions { get; }
    public Attributes Attributes { get; }
}