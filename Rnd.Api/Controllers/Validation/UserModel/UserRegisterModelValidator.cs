using FluentValidation;
using Rnd.Api.Client.Models.Basic.User;

namespace Rnd.Api.Controllers.Validation.UserModel;

public class UserRegisterModelValidator : AbstractValidator<UserRegisterModel> 
{
    public UserRegisterModelValidator()
    {
        RuleFor(u => u.Email).EmailAddress().MaximumLength(320);
        
        RuleFor(u => u.Login).Matches("^[A-Za-z0-9_]*$").Length(4, 32);
        
        RuleFor(u => u.Password).Length(4, 32)
            .Matches("[A-Z]").Matches("[a-z]").Matches("[0-9]");
    }
}