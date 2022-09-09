using Rnd.Bot.Discord.Models.Character.Panels;
using Rnd.Bot.Discord.Views;

namespace Rnd.Bot.Discord.Models.Character;

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