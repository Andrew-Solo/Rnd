using RnDBot.View;

namespace RnDBot.Models.Character;

public class Leveling : IPanel
{
    public Leveling(Character character, int level = 0, int power = 0, int dramaPoints = 0)
    {
        Character = character;
        Level = level;
        DramaPoints = dramaPoints;
        Power = power;
    }

    public Character Character { get; }
    
    public int Level { get; set; }
    public int Power { get; set; }
    public int DramaPoints { get; set; }
    
    public int PowerLimit => 0;
    public int Damage => 0;
    public int AbilityPoints => 0;

    public string Title => "Развитие";
    public string Footer => Character.General.Name;
}