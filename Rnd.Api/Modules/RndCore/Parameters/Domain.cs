using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Domain : Int32Parameter
{
    public Domain(DomainType domainType) : base(domainType.ToString())
    {
        DomainType = domainType;
    }
    
    public DomainType DomainType { get; }
    public override string Path => nameof(Domain);
}