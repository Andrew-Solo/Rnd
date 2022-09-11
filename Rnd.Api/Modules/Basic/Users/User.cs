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
    
    public void Save(Data.Entities.User entity)
    {
        if (entity.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

        entity.Login = Login;
        entity.Email = Email;
        entity.PasswordHash = PasswordHash;
        entity.RegistrationDate = RegistrationDate;
    }

    public void Load(Data.Entities.User entity)
    {
        if (Id != entity.Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

        Login = entity.Login;
        Email = entity.Email;
        PasswordHash = entity.PasswordHash;
        RegistrationDate = entity.RegistrationDate;
    }

    #endregion
}