using FluentValidation;
using Rnd.Api.Client.Models.Basic.Member;

namespace Rnd.Api.Controllers.Validation.MemberModel;

public class MemberFormModelValidator : AbstractValidator<MemberFormModel> 
{
    public MemberFormModelValidator()
    {
        RuleFor(u => u.Nickname).Length(1, 50);
        
        RuleFor(u => u.ColorHex).Length(6, 32);
        
        RuleFor(u => u.Role);
    }
}