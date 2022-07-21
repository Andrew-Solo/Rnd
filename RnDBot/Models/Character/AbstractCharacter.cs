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
        Pointers = character.Pointers;
    }
    
    public AbstractCharacter(string name)
    {
        Leveling = new Leveling(this);
        General = new General(this, name);
        Attributes = new Attributes(this);
        Pointers = new Pointers(this);
    }

    public General General { get; }
    public Leveling Leveling { get; }
    public Pointers Pointers { get; }
    public Attributes Attributes { get; }
    
    public virtual List<IPanel> Panels => new()
    {
        General,
        Leveling,
        Pointers,
        Attributes,
    };
}