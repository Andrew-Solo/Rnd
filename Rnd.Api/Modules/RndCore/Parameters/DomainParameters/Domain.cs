using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters.DomainParameters;

public class Domain : Int32Parameter
{
    public Domain(DomainType domainType, int? value = null) : base(domainType.ToString())
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