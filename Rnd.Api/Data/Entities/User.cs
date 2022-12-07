using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Helpers;

namespace Rnd.Api.Data.Entities;

[Index(nameof(Login), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(PasswordHash), nameof(Login), nameof(Email))]
[Index(nameof(PasswordHash), nameof(Email), nameof(Login))]
public class User
{
    #region Factories

    protected User() { }
    
    public static User Create(string email, string password, string? login = null)
    {
        return new User
        {
            Login = login ?? email,
            Email = email,
            PasswordHash = Hash.GenerateStringHash(password),
        };
    }

    public static User Create(UserFormModel form)
    {
        return Create(
            form.Email ?? throw new InvalidOperationException(), 
            form.Password ?? throw new InvalidOperationException(), 
            form.Login);
    }

    #endregion  
    
    public Guid Id { get; protected set; } = Guid.NewGuid();

    [MaxLength(32)] 
    public string Login { get; protected set; } = null!;

    [MaxLength(320)]
    public string Email { get; protected set; } = null!;

    [MaxLength(256)]
    public string PasswordHash { get; protected set; } = null!;

    public DateTimeOffset RegistrationDate { get; protected set; } = DateTimeOffset.Now.UtcDateTime;

    #region Navigation

    public virtual List<Member> Members { get; set; } = new();

    #endregion

    public void SetForm(UserFormModel form)
    {
        if (form.Login != null) Login = form.Login;
        if (form.Email != null) Email = form.Email;
        if (form.Password != null) PasswordHash = Hash.GenerateStringHash(form.Password);
    }
}