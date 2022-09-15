using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;

public class FinalAttributes : Attributes
{
    public FinalAttributes(ICharacter character) : base(character) { }

    public override Attribute Strength => GetAttribute(AttributeType.Strength, true);
    public override Attribute Endurance => GetAttribute(AttributeType.Endurance, true);
    public override Attribute Dexterity => GetAttribute(AttributeType.Dexterity, true);
    public override Attribute Perception => GetAttribute(AttributeType.Perception, true);
    public override Attribute Intellect => GetAttribute(AttributeType.Intellect, true);
    public override Attribute Wisdom => GetAttribute(AttributeType.Wisdom, true);
    public override Attribute Charisma => GetAttribute(AttributeType.Charisma, true);
    public override Attribute Determinism => GetAttribute(AttributeType.Determinism, true);
}