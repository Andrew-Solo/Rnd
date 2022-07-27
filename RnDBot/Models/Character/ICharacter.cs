using RnDBot.Models.Character.Panels;

namespace RnDBot.Models.Character;

//Все параметры персонажа, которые не зависят от сеттинга
public interface ICharacter
{
    string Name { get; }
    
    General General { get; }
    Pointers Pointers { get; }
    Attributes Attributes { get; }
}