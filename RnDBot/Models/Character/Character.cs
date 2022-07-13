using RnDBot.Models.CharacterFields;

namespace RnDBot.Models.Character;

public class Character
{
    public Character(string name)
    {
        Leveling = new Leveling(this);
        General = new General(this, name);
        Conditions = new Conditions(this);
        Attributes = new Attributes(this);
    }

    public General General { get; }
    public Leveling Leveling { get; }
    public Conditions Conditions { get; }
    public Attributes Attributes { get; }
}