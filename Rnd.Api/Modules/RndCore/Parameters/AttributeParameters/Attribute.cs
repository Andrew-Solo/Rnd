using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;

public class Attribute : Int32Parameter
{
    public Attribute(ICharacter character, AttributeType attributeType, int? value = null) : base(character, attributeType.ToString())
    {
        AttributeType = attributeType;
        Value = value ?? Default;
    }
    
    public AttributeType AttributeType { get; }
    public override string Path => nameof(Attribute);

    public const int Default = 0;
    public const int Offset = 10;
    public int PassiveValue => Offset + Value;
}