using FluentValidation;

namespace Rnd.Api.Controllers.Validation.GameModel;

public class GameCreateModelValidator : GameFormModelValidator

{
    public GameCreateModelValidator()
    {
        RuleFor(u => u.Name).NotNull().WithMessage("Имя игры должно быть указано");
    }
}