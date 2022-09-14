using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Games;
using Rnd.Api.Modules.Basic.Users;

namespace Rnd.Api.Modules.Basic.Members;

public class MemberFactory : IStorableFactory<Data.Entities.Member>
{
    public static Member Create(Data.Entities.Member entity)
    {
        var factory = new MemberFactory();
        return (Member) factory.CreateStorable(entity);
    }
    
    public IStorable<Data.Entities.Member> CreateStorable(Data.Entities.Member entity)
    {
        var result = new Member(GameFactory.Create(entity.Game), UserFactory.Create(entity.User));
        result.Load(entity);
        return result;
    }
}