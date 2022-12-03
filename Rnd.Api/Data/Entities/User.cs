using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Helpers;

namespace Rnd.Api.Data.Entities;

[Index(nameof(Login), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(PasswordHash), nameof(Login), nameof(Email))]
[Index(nameof(PasswordHash), nameof(Email), nameof(Login))]
public class User : IEntity
{
    public static User Create(string email, string password, string? login = null)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Login = login ?? email,
            Email = email,
            PasswordHash = Hash.GenerateStringHash(password),
            RegistrationDate = DateTimeOffset.Now.UtcDateTime
        };

        return user;
    }

    public static User Create(UserFormModel form)
    {
        return Create(form.Email!, form.Password!, form.Login);
    }
    
    public Guid Id { get; set; }

    [MaxLength(32)] public string Login { get; set; } = null!;

    [MaxLength(320)]
    public string Email { get; set; } = null!;

    [MaxLength(256)]
    public string PasswordHash { get; set; } = null!;

    public DateTimeOffset RegistrationDate { get; set; }

    #region Navigation

    public virtual List<Member> Members { get; set; } = new();

    #endregion
}