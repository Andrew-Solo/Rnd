using RnDBot.Models.Character.Panels;
using RnDBot.Views;

namespace RnDBot.Models.Character;

//Все параметры персонажа, которые не зависят от сеттинга
public interface ICharacter : IPanelList, IValidatable
{
    string Name { get; }
    
    General General { get; }
    Attributes Attributes { get; }
    Pointers Pointers { get; }
    Effects Effects { get; }
    Traumas Traumas { get; }
    Backstory Backstory { get; }

    int GetPower { get; }

    string GetFooter => $"{Name}, {Attributes.Level} ур.";
}