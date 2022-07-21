using RnDBot.Models.Character.Fields;
using RnDBot.Models.Glossaries;
using RnDBot.View;

namespace RnDBot.Models.Character.Panels;

public class Conditions : IPanel
{
    public Conditions(ICharacter character, List<Condition>? coreConditions = null)
    {
        Character = character;

        var ap = Character.Leveling.AbilityPoints.TValue;
        var end = Character.Attributes.FinalAttributes.First(a => a.AttributeType == AttributeType.End).Modifier;
        var det = Character.Attributes.FinalAttributes.First(a => a.AttributeType == AttributeType.Det).Modifier;
        
        CoreConditions = coreConditions ?? new List<Condition>
        {
            new(ConditionType.AbilityPoints, ap),
            new(ConditionType.Armor, 0),
            new(ConditionType.Body, 10 + end),
            new(ConditionType.Barrier, 0),
            new(ConditionType.Will, 10 + det),
        };
    }

    public ICharacter Character { get; }
    public List<Condition> CoreConditions { get; }
    
    //TODO Items
    public List<Condition> FinalConditions => CoreConditions;
    
    public string Title => "Состояния";
    public List<IField> Fields => FinalConditions.Select(a => (IField) a).ToList();
    public string Footer => Character.General.Name;
}