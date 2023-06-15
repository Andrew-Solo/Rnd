using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using FluentValidation;
using Rnd.Constants;
using Rnd.Core;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace Rnd.Models;

public class User : ValidatableModel<User, User.Form, User.UpdateValidator, User.ClearValidator>
{
    //TODO implement
    
    [MaxLength(TextSize.Tiny)] 
    public string Login { get; protected set; }

    [MaxLength(TextSize.Medium)]
    public string Email { get; protected set; }

    [MaxLength(TextSize.Hash)]
    public string PasswordHash { get; protected set; }
    
    [MaxLength(TextSize.Tiny)] 
    public UserRole Role { get; protected set; }
    
    public ulong? DiscordId { get; protected set; }

    public DateTimeOffset Registered { get; protected set; }

    #region Navigation

    public virtual List<Member> Memberships { get; protected set; } = new();

    #endregion

    #region Factories

    protected User(
        string login, 
        string email, 
        string passwordHash,
        UserRole role,
        ulong? discordId)
    {
        Login = login;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        DiscordId = discordId;
        Registered = Time.Now;
    }
    
    public class Factory : ValidatingFactory<User, Form, CreateValidator>
    {
        public override User Create(Form form)
        {
            Guard.Against.NullOrWhiteSpace(form.Email, nameof(form.Email));
            Guard.Against.NullOrWhiteSpace(form.Password, nameof(form.Password));
        
            return new User(
                form.Login ?? form.Email, 
                form.Email, 
                Hash.GenerateStringHash(form.Password), 
                form.Role.GetValueOrDefault(),
                form.DiscordId
            );
        }
    }

    public static Factory New => new();

    #endregion

    #region Updaters

    public override User Update(Form form)
    {
        if (form.Login != null) Login = form.Login;
        if (form.Email != null) Email = form.Email;
        if (form.Password != null) PasswordHash = Hash.GenerateStringHash(form.Password);
        if (form.Role != null) Role = form.Role.Value;
        if (form.DiscordId != null) DiscordId = form.DiscordId;
        return this;
    }

    public override User Clear(Form form)
    {
        Guard.Against.Null(form.Login, nameof(form.Login));
        Guard.Against.Null(form.Email, nameof(form.Email));
        Guard.Against.Null(form.Password, nameof(form.Password));
        Guard.Against.Null(form.Role, nameof(form.Role));
        if (form.DiscordId == null) DiscordId = null;
        return this;
    }

    #endregion

    #region Validators

    public class UpdateValidator : AbstractValidator<Form> 
    {
        public UpdateValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress().WithMessage("Поле email должно быть электронным адресом")
                .MaximumLength(TextSize.Medium).WithMessage("Максимальная длинна email – {MaxLength} символов, сейчас {TotalLength}");
        
            RuleFor(u => u.Login)
                .Matches("^[A-Za-z0-9_]*$").WithMessage("Логин содержет запрещенные символы, разрешены только латинские буквы, " +
                                                        "цифры и нижнее подчеркивание")
                .Length(TextSize.Word, TextSize.Tiny).WithMessage("Длина логина должна быть от {MinLength} до {MaxLength} символов, " +
                                                                  "сейчас {TotalLength}");
            
            RuleFor(u => u.Password)
                .Length(TextSize.Word, TextSize.Tiny).WithMessage("Длина пароля должна быть от {MinLength} до {MaxLength} символов, " +
                                                                  "сейчас {TotalLength}")
                .Matches("[A-Z]").WithMessage("Пароль должен содержать хотя бы одну латинскую букву в верхнем регистре")
                .Matches("[a-z]").WithMessage("Пароль должен содержать хотя бы одну латинскую букву в нижнем регистре")
                .Matches("[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру");
            
            //TODO Role validation
        }
    }
    
    public class CreateValidator : UpdateValidator
    {
        public CreateValidator()
        {
            RuleFor(u => u.Email).NotNull().WithMessage("Email должен быть указан");
            RuleFor(u => u.Password).NotNull().WithMessage("Пароль должен быть указан");
        }
    }
    
    public class ClearValidator : AbstractValidator<Form> 
    {
        public ClearValidator()
        {
            RuleFor(u => u.Login).NotNull().WithMessage("Нельзя очистить логин");
            RuleFor(u => u.Email).NotNull().WithMessage("Нельзя очистить email");
            RuleFor(u => u.Password).NotNull().WithMessage("Нельзя очистить пароль");
        }
    }

    #endregion

    #region Views

    public record struct Form(
        string? Login = null, 
        string? Email = null, 
        string? Password = null,
        UserRole? Role = null,
        ulong? DiscordId = null
    );
    
    public Form GetForm()
    {
        return new Form(
            Login, 
            Email, 
            PasswordHash,
            Role,
            DiscordId
        );
    } 
    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
    public readonly record struct View(
        Guid _id,
        string Login,
        string Email,
        string Role,
        DateTimeOffset Registered,
        Guid[] _gameIds,
        string[] Games
    );
    
    public View GetView()
    {
        var games = Memberships
            .OrderByDescending(g => g.Selected)
            .Select(m => m.Game)
            .ToDictionary(g => g.Id, g => g.Name);
        
        return new View(
            Id, 
            Login, 
            Email, 
            Role.ToString(),
            Registered, 
            games.Keys.ToArray(), 
            games.Values.ToArray()
        );
    }

    #endregion
}