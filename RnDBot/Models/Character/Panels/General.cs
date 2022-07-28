using RnDBot.Models.Common;
using RnDBot.Views;

namespace RnDBot.Models.Character.Panels;

public class General : IPanel
{
    public General(ICharacter character, 
        string? culture = null, string? age = null, 
        List<string>? ideals = null, List<string>? vices = null, List<string>? traits = null)
    {
        Character = character;
        
        Culture = new TextField<string?>("Культура", culture);
        Age = new TextField<string?>("Возраст", age);
        
        Ideals = new ListField("Идеалы", ideals);
        Vices = new ListField("Пороки", vices);
        Traits = new ListField("Черты", traits);
    }

    public ICharacter Character { get; }
    
    public TextField<string?> Culture { get; }
    public TextField<string?> Age { get; }
    public ListField Ideals { get; }
    public ListField Vices { get; }
    public ListField Traits { get; }

    public string Title => Character.GetFooter;
    public List<IField> Fields => new()
    {
        Culture,
        Age,
        Ideals,
        Vices,
        Traits
    };
}