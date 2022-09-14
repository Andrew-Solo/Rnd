using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters.DomainParameters;

public class Domain : Int32Parameter
{
    public Domain(ICharacter character, DomainType domainType, int? value = null) : base(character, domainType.ToString())
    {
        DomainType = domainType;
        Value = value ?? Default;
    }
    
    public DomainType DomainType { get; }
    public override string Path => nameof(Domain);
    
    public const int Default = 4;
    public const int Min = 0;
    public const int Max = 8;
}