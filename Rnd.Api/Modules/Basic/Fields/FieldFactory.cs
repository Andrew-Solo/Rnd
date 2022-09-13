using Rnd.Api.Data;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Modules.Basic.Fields;

public class FieldFactory : IStorableFactory<Field>
{
    public static IField Create(Field entity)
    {
        throw new NotImplementedException();
    }

    public IStorable<Field> CreateStorable(Field entity)
    {
        throw new NotImplementedException();
    }
}