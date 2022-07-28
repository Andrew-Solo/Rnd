using RnDBot.Models.Character.Panels;
using RnDBot.Views;

namespace RnDBot.Models.Character;

//Все параметры персонажа, которые не зависят от сеттинга
public interface ICharacter : IPanelList
{
    string Name { get; }
    
    General General { get; }
    Pointers Pointers { get; }
    Attributes Attributes { get; }

    string GetFooter => $"{Name}, {Attributes.Level} ур.";
}