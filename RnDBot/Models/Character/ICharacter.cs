using RnDBot.Models.Character.Panels;

namespace RnDBot.Models.Character;

//Все параметры персонажа, которые не зависят от сеттинга
public interface ICharacter
{
    public General General { get; }
    public Pointers Pointers { get; }
    public Attributes Attributes { get; }
}