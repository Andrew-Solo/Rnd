using RnDBot.Models.Common;
using RnDBot.View;

namespace RnDBot.Models.Character.Panels;

public class Leveling : IPanel
{
    public Leveling(ICharacter character, int level = 0, int power = 32)
    {
        Character = character;
        
        Level = new TextField<int>("Уровень", level);
        Power = new TextField<int>("Мощь", power);
    }

    public ICharacter Character { get; }
    
    public TextField<int> Level { get; }
    public TextField<int> Power { get; }

    public TextField<int> PowerLimit => new("Лимит мощи", 0);
    public TextField<int> Damage => new("Урон", 1 + Level.TValue / 16);
    public TextField<int> AbilityPoints => new("Очки способностей", 1 + Power.TValue / 10);

    public string Title => "Развитие";
    public string Footer => Character.General.Name;
    public List<IField> Fields => new()
    {
        Level,
        Power,
        PowerLimit,
        Damage,
        AbilityPoints,
    };
}