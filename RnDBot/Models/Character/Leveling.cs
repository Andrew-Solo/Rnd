using RnDBot.Models.Common;
using RnDBot.View;

namespace RnDBot.Models.Character;

public class Leveling : IPanel
{
    public Leveling(Character character, int level = 0, int power = 0, int dramaPoints = 0)
    {
        Character = character;
        
        Level = new NumberField("Уровень", level);
        DramaPoints = new NumberField("Очки драмы", dramaPoints);
        Power = new NumberField("Мощь", power);
    }

    public Character Character { get; }
    
    public NumberField Level { get; }
    public NumberField Power { get; }
    public NumberField DramaPoints { get; }

    public NumberField PowerLimit => new NumberField("Лимит мощи", 0);
    public NumberField Damage => new NumberField("Урон", 1 + Level.Number / 16);
    public NumberField AbilityPoints => new NumberField("Очки способностей", 1 + Power.Number / 10);

    public string Title => "Развитие";
    public string Footer => Character.General.Name;
    public List<IField>? Fields => new()
    {
        Level,
        Power,
        PowerLimit,
        Damage,
        DramaPoints,
        AbilityPoints,
    };
}