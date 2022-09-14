using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Localization;

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
        return entity.Type switch
        {
            nameof(Boolean) => CreateBoolean(entity),
            nameof(Decimal) => CreateDecimal(entity),
            nameof(Int32) => CreateInt32(entity),
            _ => throw new ArgumentOutOfRangeException(nameof(entity.Type), entity.Type, 
                Lang.Exceptions.IStorableFactory.UnknownType)
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
}