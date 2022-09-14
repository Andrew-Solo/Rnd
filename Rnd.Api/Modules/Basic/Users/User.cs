using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Members;

namespace Rnd.Api.Modules.Basic.Users;

public class User : IStorable<Data.Entities.User>
{
    public User(IEntity entity)
    {
        Id = entity.Id;
        
        Login = null!;
        Email = null!;
        PasswordHash = null!;
        
        Members = new List<Member>();
    }
    
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
    public List<Member> Members { get; private set; }

    #region IStorable

    public IStorable<Data.Entities.User> AsStorable => this;
    
    public Data.Entities.User? Save(Data.Entities.User? entity, Action<IEntity>? setAddedState = null, bool upcome = true)
    {
        entity ??= new Data.Entities.User {Id = Id};
        if (AsStorable.NotSave(entity)) return null;
        
        entity.Login = Login;
        entity.Email = Email;
        entity.PasswordHash = PasswordHash;
        entity.RegistrationDate = RegistrationDate;
        if (upcome) entity.Members.SaveList(Members.Cast<IStorable<Data.Entities.Member>>().ToList(), setAddedState);

        return entity;
    }

    public IStorable<Data.Entities.User>? Load(Data.Entities.User entity, bool upcome = true)
    {
        if (AsStorable.NotLoad(entity)) return null;
 
        Login = entity.Login;
        Email = entity.Email;
        PasswordHash = entity.PasswordHash;
        RegistrationDate = entity.RegistrationDate;
        
        if (upcome) Members = Members
            .Cast<IStorable<Data.Entities.Member>>().ToList()
            .LoadList(entity.Members, new MemberFactory())
            .Cast<Member>().ToList();

        return AsStorable;
    }

    #endregion
}