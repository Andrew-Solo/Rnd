using RnDBot.Models.Character.Fields;
using RnDBot.Models.Glossaries;
using RnDBot.View;

namespace RnDBot.Models.Character.Panels;

public class Pointers : IPanel
{
    public Pointers(ICharacter character)
    {
        Character = character;

        var ap = Character.Pointers.FinalPointers.First(c => c.PointerType == PointerType.AbilityPoints).Max;
        var end = Character.Attributes.FinalAttributes.First(a => a.AttributeType == AttributeType.End).Modifier;
        var det = Character.Attributes.FinalAttributes.First(a => a.AttributeType == AttributeType.Det).Modifier;
        
        CorePointers = new List<Pointer>
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
    public List<Pointer> CorePointers { get; }
    
    //TODO Items
    public List<Pointer> FinalPointers => CorePointers;
    
    public string Title => "Состояния";
    public List<IField> Fields => FinalPointers.Select(a => (IField) a).ToList();
    public string Footer => Character.General.Name;
}