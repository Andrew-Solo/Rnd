using RnDBot.Models.CharacterFields;
using RnDBot.Models.Glossaries;
using RnDBot.View;

namespace RnDBot.Models.Character;

public class Conditions : IPanel
{
    public Conditions(Character character, List<Condition>? coreConditions = null)
    {
        Character = character;
        
        CoreConditions = coreConditions ?? new()
        {
            new Condition(ConditionType.AbilityPoints, 4),
            new Condition(ConditionType.Armor, 0),
            new Condition(ConditionType.Body, 10),
            new Condition(ConditionType.Barrier, 0),
            new Condition(ConditionType.Will, 10),
        };
        
        FinalConditions = coreConditions ?? CoreConditions;
    }

    public Character Character { get; }
    public List<Condition> CoreConditions { get; }
    public List<Condition> FinalConditions { get; }
    
    public string Title => "Состояния";
    public List<IField> Fields => FinalConditions.Select(a => (IField) a).ToList();
    public string Footer => Character.General.Name;
}