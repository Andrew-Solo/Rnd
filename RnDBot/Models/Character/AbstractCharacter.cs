using RnDBot.Models.Character.Panels;
using RnDBot.View;

namespace RnDBot.Models.Character;

public class AbstractCharacter : ICharacter
{
    public AbstractCharacter(ICharacter character)
    {
        General = character.General;
        Attributes = character.Attributes;
        Pointers = character.Pointers;
    }
    
    public AbstractCharacter(string name)
    {
        General = new General(this, name);
        Attributes = new Attributes(this, 0);
        Pointers = new Pointers(this);
    }

    public General General { get; }
    public Pointers Pointers { get; }
    public Attributes Attributes { get; }
    
    public virtual List<IPanel> Panels => new()
    {
        General,
        Pointers,
        Attributes,
    };
}