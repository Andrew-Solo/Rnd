using FluentValidation;

namespace Rnd.Api.Controllers.Validation.MemberModel;

public class MemberCreateModelValidator : MemberFormModelValidator

{
    public MemberCreateModelValidator()
    {
        RuleFor(u => u.UserId)
            .NotNull().WithMessage("Пользователь должен быть указан");;
    }
}