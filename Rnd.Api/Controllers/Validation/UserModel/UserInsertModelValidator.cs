using FluentValidation;

namespace Rnd.Api.Controllers.Validation.UserModel;

public class UserInsertModelValidator : UserFormModelValidator
{
    public UserInsertModelValidator()
    {
        RuleFor(u => u.Email).NotNull();
        
        RuleFor(u => u.Password).NotNull();
    }
}