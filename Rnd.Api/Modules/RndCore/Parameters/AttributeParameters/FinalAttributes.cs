using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;

public class FinalAttributes : Attributes
{
    public FinalAttributes(Character character)
    {
        Character = character;
    }
    
    public Character Character { get; }

    public override Attribute Strength => Character.Effects
        .Aggregate(new FinalAttribute(base.Strength), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Endurance => Character.Effects
        .Aggregate(new FinalAttribute(base.Endurance), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Dexterity => Character.Effects
        .Aggregate(new FinalAttribute(base.Dexterity), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Perception => Character.Effects
        .Aggregate(new FinalAttribute(base.Perception), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Intellect => Character.Effects
        .Aggregate(new FinalAttribute(base.Intellect), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Wisdom => Character.Effects
        .Aggregate(new FinalAttribute(base.Wisdom), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Charisma => Character.Effects
        .Aggregate(new FinalAttribute(base.Charisma), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Determinism => Character.Effects
        .Aggregate(new FinalAttribute(base.Determinism), (attribute, effect) => effect.ModifyParameter(attribute));
}