using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Users;

public class UserFactory : IStorableFactory<Data.Entities.User>
{
    public static User Create(Data.Entities.User entity)
    {
        throw new NotImplementedException();
    }

    public IStorable<Data.Entities.User> CreateStorable(Data.Entities.User entity)
    {
        throw new NotImplementedException();
    }
}