using FluentValidation;

namespace Rnd.Api.Controllers.Validation.UserModel;

public class UserRegisterModelValidator : UserFormModelValidator
{
    public UserRegisterModelValidator()
    {
        RuleFor(u => u.Email).NotNull();
        
        RuleFor(u => u.Password).NotNull();
    }
}