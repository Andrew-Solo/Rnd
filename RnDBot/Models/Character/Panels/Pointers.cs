using RnDBot.Models.Character.Fields;
using RnDBot.Models.Glossaries;
using RnDBot.View;

namespace RnDBot.Models.Character.Panels;

public class Pointers : IPanel
{
    public Pointers(ICharacter character)
    {
        Character = character;

        var ap = Character.Leveling.AbilityPoints.TValue;
        var end = Character.Attributes.FinalAttributes.First(a => a.AttributeType == AttributeType.End).Modifier;
        var det = Character.Attributes.FinalAttributes.First(a => a.AttributeType == AttributeType.Det).Modifier;
        
        CoreConditions = new List<Pointer>
        {
            new(PointerType.DramaPoints, 3, 0),
            new(PointerType.AbilityPoints, ap),
            new(PointerType.Armor, 0),
            new(PointerType.Body, 10 + end),
            new(PointerType.Barrier, 0),
            new(PointerType.Will, 10 + det),
        };
    }

    public ICharacter Character { get; }
    public List<Pointer> CoreConditions { get; }
    
    //TODO Items
    public List<Pointer> FinalConditions => CoreConditions;
    
    public string Title => "Состояния";
    public List<IField> Fields => FinalConditions.Select(a => (IField) a).ToList();
    public string Footer => Character.General.Name;
}