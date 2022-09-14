using Rnd.Api.Data;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters.DomainParameters;

public class Domain : Int32Parameter
{
    public Domain(IEntity entity) : base(entity) { }
    
    public Domain(ICharacter character, DomainType domainType, int? value = null) : base(character, domainType.ToString())
    {
        DomainType = domainType;
        Value = value ?? Default;
    }
    
    public DomainType DomainType
    {
        get => EnumHelper.Parse<DomainType>(Name);
        private init => Name = value.ToString();
    }
    
    public override string Path => nameof(Domain);
    
    public const int Default = 4;
    public const int Min = 0;
    public const int Max = 8;
}