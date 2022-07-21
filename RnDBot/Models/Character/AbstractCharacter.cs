using RnDBot.Models.Character.Panels;
using RnDBot.View;

namespace RnDBot.Models.Character;

public class AbstractCharacter : ICharacter
{
    public AbstractCharacter(ICharacter character)
    {
        Leveling = character.Leveling;
        General = character.General;
        Attributes = character.Attributes;
        Conditions = character.Conditions;
    }
    
    public AbstractCharacter(string name)
    {
        Leveling = new Leveling(this);
        General = new General(this, name);
        Attributes = new Attributes(this);
        Conditions = new Conditions(this);
    }

    public General General { get; }
    public Leveling Leveling { get; }
    public Conditions Conditions { get; }
    public Attributes Attributes { get; }
    
    public virtual List<IPanel> Panels => new()
    {
        General,
        Leveling,
        Conditions,
        Attributes,
    };
}