using FluentValidation;
using Rnd.Api.Client.Models.Basic.Member;

namespace Rnd.Api.Controllers.Validation.MemberModel;

public class MemberFormModelValidator : AbstractValidator<MemberFormModel> 
{
    //TODO тут явно валидация не доделана
    public MemberFormModelValidator()
    {
        RuleFor(u => u.Nickname)
            .Length(1, 50).WithMessage("Длина никнейма должна быть от 1 до 50 символов, сейчас {TotalLength}");
        
        RuleFor(u => u.ColorHex)
            .Length(6, 32).WithMessage("Длина цветового кода должна быть от 6 до 32 символов, сейчас {TotalLength}");
        
        RuleFor(u => u.Role);
    }
}