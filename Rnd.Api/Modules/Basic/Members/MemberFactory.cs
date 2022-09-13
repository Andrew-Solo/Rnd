using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Members;

public class MemberFactory : IStorableFactory<Data.Entities.Member>
{
    public static Member Create(Data.Entities.Member entity)
    {
        throw new NotImplementedException();
    }
    
    public IStorable<Data.Entities.Member> CreateStorable(Data.Entities.Member entity)
    {
        throw new NotImplementedException();
    }
}