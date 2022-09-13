using Rnd.Api.Data;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Modules.Basic.Parameters;

public class ParameterFactory : IStorableFactory<Parameter>
{
    public static IParameter Create(Parameter entity)
    {
        throw new NotImplementedException();
    }

    public IStorable<Parameter> CreateStorable(Parameter entity)
    {
        throw new NotImplementedException();
    }
}