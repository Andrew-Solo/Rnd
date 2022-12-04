using FluentValidation;
using Rnd.Api.Client.Models.Basic.User;

namespace Rnd.Api.Controllers.Validation.UserModel;

public class UserFormModelValidator : AbstractValidator<UserFormModel> 
{
    public UserFormModelValidator()
    {
        RuleFor(u => u.Email)
            .EmailAddress().WithMessage("Поле email должно быть электронным адресом")
            .MaximumLength(320).WithMessage("Максимальная длинна email – 320 символов, сейчас {TotalLength}");
        
        RuleFor(u => u.Login)
            .Matches("^[A-Za-z0-9_]*$").WithMessage("Логин содержет запрещенные символы, разрешены только латинские буквы, " +
                                                            "цифры и нижнее подчеркивание.")
            .Length(4, 32).WithMessage("Длина логина должна быть от 4 до 32 символов, сейчас {TotalLength}");
        
        RuleFor(u => u.Password)
            .Length(4, 32).WithMessage("Длина пароля должна быть от 4 до 32 символов, сейчас {TotalLength}")
            .Matches("[A-Z]").WithMessage("Пароль должен содержать хотя бы одну латинскую букву в верхнем регистре")
            .Matches("[a-z]").WithMessage("Пароль должен содержать хотя бы одну латинскую букву в нижнем регистре")
            .Matches("[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру");
    }
}