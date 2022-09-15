using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;
using Rnd.Api.Localization;
using Rnd.Api.Modules.RndCore.Parameters.DomainParameters;
using Rnd.Api.Modules.RndCore.Parameters.SkillParameters;
using Attribute = Rnd.Api.Modules.RndCore.Parameters.AttributeParameters.Attribute;
using Path = Rnd.Api.Helpers.Path;

namespace Rnd.Api.Modules.Basic.Parameters;

public class ParameterFactory : IStorableFactory<Parameter>
{
    public static IParameter Create(Parameter entity)
    {
        var factory = new ParameterFactory();
        return (IParameter) factory.CreateStorable(entity);
    }

    public IStorable<Parameter> CreateStorable(Parameter entity)
    {
        return CreateSimilar(entity).LoadNotNull(entity);
    }
    
    protected virtual IParameter CreateSimilar(Parameter entity)
    {
        return Path.GetPath(entity.Fullname) switch
        {
            nameof(Attribute) => CreateAttribute(entity),
            nameof(Domain) => CreateDomain(entity),
            nameof(Skill) => CreateSkill(entity),
            _ => entity.Type switch
            {
                nameof(Boolean) => CreateBoolean(entity),
                nameof(Decimal) => CreateDecimal(entity),
                nameof(Int32) => CreateInt32(entity),
                _ => throw new ArgumentOutOfRangeException(nameof(entity.Type), entity.Type, 
                    Lang.Exceptions.IStorableFactory.UnknownType)
            }
        };
    }
    
    private static BooleanParameter CreateBoolean(IEntity entity)
    {
        return new BooleanParameter(entity);
    }
    
    private static DecimalParameter CreateDecimal(IEntity entity)
    {
        return new DecimalParameter(entity);
    }
    
    private static Int32Parameter CreateInt32(IEntity entity)
    {
        return new Int32Parameter(entity);
    }
    
    private static Attribute CreateAttribute(IEntity entity)
    {
        return new Attribute(entity);
    }
    
    private static Domain CreateDomain(IEntity entity)
    {
        return new Domain(entity);
    }
    
    private static Skill CreateSkill(IEntity entity)
    {
        return new Skill(entity);
    }
}