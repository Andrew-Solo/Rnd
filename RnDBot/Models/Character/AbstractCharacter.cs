using RnDBot.Models.Character.Panels;
using RnDBot.View;
using RnDBot.Views;

namespace RnDBot.Models.Character;

public class AbstractCharacter : ICharacter
{
    public AbstractCharacter(ICharacter character)
    {
        Name = character.Name;
        General = character.General;
        Attributes = character.Attributes;
        Pointers = character.Pointers;
    }
    
    public AbstractCharacter(string name)
    {
        Name = name;
        General = new General(this);
        Attributes = new Attributes(this, 0);
        Pointers = new Pointers(this);
    }

    public string Name { get; }
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