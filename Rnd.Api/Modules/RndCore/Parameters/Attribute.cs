using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Attribute : Int32Parameter
{
    public Attribute(AttributeType attributeType) : base(attributeType.ToString())
    {
        AttributeType = attributeType;
    }
    
    public AttributeType AttributeType { get; }
    public override string Path => nameof(Attribute);

    public const int Offset = 10;
    public int PassiveValue => Offset + Value;
}