using RnDBot.View;

namespace RnDBot.Models.Character;

public class General : IPanel
{
    public General(Character character, string name)
    {
        Name = name;
        Character = character;
    }

    public Character Character { get; }
    
    public string Name { get; set; }
    public string? Culture { get; set; }
    public string? Age { get; set; }
    public string? Ideals { get; set; }
    public string? Vices { get; set; }
    public string? Traits { get; set; }

    public string Title => Name;
}