using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Members;

namespace Rnd.Api.Modules.Basic.Users;

public class User : IStorable<Data.Entities.User>
{
    public User(string login, string email, string passwordHash)
    {
        Login = login;
        Email = email;
        PasswordHash = passwordHash;

        Id = Guid.NewGuid();
        RegistrationDate = DateTimeOffset.Now.UtcDateTime;
        Members = new List<Member>();
    }

    public Guid Id { get; } 
    public string Login { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTimeOffset RegistrationDate { get; private set; }
    public List<Member> Members { get; }

    #region IStorable

    public IStorable<Data.Entities.User> AsStorable => this;
    
    public Data.Entities.User? Save(Data.Entities.User? entity)
    {
        entity ??= new Data.Entities.User {Id = Id};
        if (AsStorable.NotSave(entity)) return null;
        
        entity.Login = Login;
        entity.Email = Email;
        entity.PasswordHash = PasswordHash;
        entity.RegistrationDate = RegistrationDate;
        entity.Members.SaveList(Members.Cast<IStorable<Data.Entities.Member>>().ToList());

        return entity;
    }

    public IStorable<Data.Entities.User>? Load(Data.Entities.User entity)
    {
        if (AsStorable.NotLoad(entity)) return null;
 
        Login = entity.Login;
        Email = entity.Email;
        PasswordHash = entity.PasswordHash;
        RegistrationDate = entity.RegistrationDate;
        Members.Cast<IStorable<Data.Entities.Member>>().ToList().LoadList(entity.Members, new MemberFactory());

        return AsStorable;
    }

    #endregion
}