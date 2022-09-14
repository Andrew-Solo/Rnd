using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Users;

public class UserFactory : IStorableFactory<Data.Entities.User>
{
    public static User Create(Data.Entities.User entity)
    {
        var factory = new UserFactory();
        return (User) factory.CreateStorable(entity);
    }

    public IStorable<Data.Entities.User> CreateStorable(Data.Entities.User entity)
    {
        var result = new User(entity);
        result.Load(entity);
        return result;
    }
}