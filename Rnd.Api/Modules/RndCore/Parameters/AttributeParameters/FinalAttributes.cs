using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;

public class FinalAttributes : Attributes
{
    public FinalAttributes(ICharacter character) : base(character) { }

    public override Attribute Strength => Character.Effects
        .Aggregate(GetAttribute(AttributeType.Strength, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Endurance => Character.Effects
        .Aggregate(GetAttribute(AttributeType.Endurance, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Dexterity => Character.Effects
        .Aggregate(GetAttribute(AttributeType.Dexterity, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Perception => Character.Effects
        .Aggregate(GetAttribute(AttributeType.Perception, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Intellect => Character.Effects
        .Aggregate(GetAttribute(AttributeType.Intellect, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Wisdom => Character.Effects
        .Aggregate(GetAttribute(AttributeType.Wisdom, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Charisma => Character.Effects
        .Aggregate(GetAttribute(AttributeType.Charisma, true), (attribute, effect) => effect.ModifyParameter(attribute));
    public override Attribute Determinism => Character.Effects
        .Aggregate(GetAttribute(AttributeType.Determinism, true), (attribute, effect) => effect.ModifyParameter(attribute));
}