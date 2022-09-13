using Rnd.Api.Data;
using Rnd.Api.Localization;

namespace Rnd.Api.Modules.Basic.Users;

public class User : IStorable<Data.Entities.User>
{
    public User(string login, string email, string passwordHash)
    {
        Login = login;
        Email = email;
        PasswordHash = passwordHash;

        Id = Guid.NewGuid();
        RegistrationDate = DateTime.Now;
    }

    public Guid Id { get; } 
    public string Login { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RegistrationDate { get; private set; }
    
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

        return entity;
    }

    public void Load(Data.Entities.User entity)
    {
        if (AsStorable.NotLoad(entity)) return;
 
        Login = entity.Login;
        Email = entity.Email;
        PasswordHash = entity.PasswordHash;
        RegistrationDate = entity.RegistrationDate;
    }

    #endregion
}