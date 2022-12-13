using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using Rnd.Model;

namespace Rnd.Entities;

public class User : ValidatableEntity<User.Form, User.UpdateValidator, User.ClearValidator>
{
    [MaxLength(32)] 
    public string Login { get; protected set; }

    [MaxLength(320)]
    public string Email { get; protected set; }

    [MaxLength(256)]
    public string PasswordHash { get; protected set; }

    public DateTimeOffset Registered { get; protected set; }
    
    public readonly record struct Form(
        string? Login = null, 
        string? Email = null, 
        string? Password = null);

    #region Navigation

    //public virtual List<Member> Members { get; protected set; } = new();

    #endregion

    #region Factories

    protected User(
        string login, 
        string email, 
        string passwordHash)
    {
        Login = login;
        Email = email;
        PasswordHash = passwordHash;
    }
    
    public class Factory : ValidatingFactory<User, Form, CreateValidator>
    {
        public override User Create(Form form)
        {
            Guard.Against.NullOrWhiteSpace(form.Email, nameof(form.Email));
            Guard.Against.NullOrWhiteSpace(form.Password, nameof(form.Password));
        
            return new User(form.Login ?? form.Email, form.Email, Hash.GenerateStringHash(form.Password));
        }
    }

    public static Factory New => new();

    #endregion

    #region Updaters

    public override void Update(Form form)
    {
        if (form.Login != null) Login = form.Login;
        if (form.Email != null) Email = form.Email;
        if (form.Password != null) PasswordHash = Hash.GenerateStringHash(form.Password);
    }

    public override void Clear(Form form)
    {
        Guard.Against.Null(form.Login, nameof(form.Login));
        Guard.Against.Null(form.Email, nameof(form.Email));
        Guard.Against.Null(form.Password, nameof(form.Password));
    }

    #endregion

    #region Validators

    public class UpdateValidator : AbstractValidator<Form> 
    {
        public UpdateValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress().WithMessage("Поле email должно быть электронным адресом")
                .MaximumLength(320).WithMessage("Максимальная длинна email – 320 символов, сейчас {TotalLength}");
        
            RuleFor(u => u.Login)
                .Matches("^[A-Za-z0-9_]*$").WithMessage("Логин содержет запрещенные символы, разрешены только латинские буквы, " +
                                                        "цифры и нижнее подчеркивание")
                .Length(4, 32).WithMessage("Длина логина должна быть от 4 до 32 символов, сейчас {TotalLength}");
        
            RuleFor(u => u.Password)
                .Length(4, 32).WithMessage("Длина пароля должна быть от 4 до 32 символов, сейчас {TotalLength}")
                .Matches("[A-Z]").WithMessage("Пароль должен содержать хотя бы одну латинскую букву в верхнем регистре")
                .Matches("[a-z]").WithMessage("Пароль должен содержать хотя бы одну латинскую букву в нижнем регистре")
                .Matches("[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру");
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
            RuleFor(u => u.Email).NotNull().WithMessage("Нельзя очистить Email");
            RuleFor(u => u.Password).NotNull().WithMessage("Нельзя очистить пароль");
        }
    }

    #endregion

    #region Views

    

    #endregion
}