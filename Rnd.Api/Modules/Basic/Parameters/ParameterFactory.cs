using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;
using Rnd.Api.Localization;
using Rnd.Api.Modules.Basic.Characters;

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
    
    private static BooleanParameter CreateBoolean(Parameter entity)
    {
        return new BooleanParameter(CharacterFactory.Create(entity.Character), PathHelper.GetName(entity.Fullname));
    }
    
    private static DecimalParameter CreateDecimal(Parameter entity)
    {
        return new DecimalParameter(CharacterFactory.Create(entity.Character), PathHelper.GetName(entity.Fullname));
    }
    
    private static Int32Parameter CreateInt32(Parameter entity)
    {
        return new Int32Parameter(CharacterFactory.Create(entity.Character), PathHelper.GetName(entity.Fullname));
    }
}