using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Resources;

public class ResourceFactory : IStorableFactory<Data.Entities.Resource>
{
    public static IResource Create(Data.Entities.Resource entity)
    {
        throw new NotImplementedException();
    }

    public IStorable<Data.Entities.Resource> CreateStorable(Data.Entities.Resource entity)
    {
        throw new NotImplementedException();
    }
}