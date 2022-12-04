using FluentValidation;

namespace Rnd.Api.Controllers.Validation.UserModel;

public class UserCreateModelValidator : UserFormModelValidator
{
    public UserCreateModelValidator()
    {
        RuleFor(u => u.Email).NotNull().WithMessage("Email должен быть указан");
        
        RuleFor(u => u.Password).NotNull().WithMessage("Пароль должен быть указан");
    }
}