using RnDBot.Models.Common;
using RnDBot.View;

namespace RnDBot.Models.Character;

public class General : IPanel
{
    public General(ICharacter character, string name, 
        string? culture = null, string? age = null, 
        List<string>? ideals = null, List<string>? vices = null, List<string>? traits = null)
    {
        Name = name;
        Character = character;
        
        Culture = new TextField("Культура", culture);
        Age = new TextField("Возраст", age);
        
        Ideals = new ListField("Идеалы", ideals);
        Vices = new ListField("Пороки", vices);
        Traits = new ListField("Черты", traits);
    }

    public ICharacter Character { get; }
    
    public string Name { get; set; }
    public TextField Culture { get; }
    public TextField Age { get; }
    public ListField Ideals { get; }
    public ListField Vices { get; }
    public ListField Traits { get; }

    public string Title => Name;
    public List<IField> Fields => new()
    {
        Culture,
        Age,
        Ideals,
        Vices,
        Traits
    };
}