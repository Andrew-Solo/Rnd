using FluentValidation;

namespace Rnd.Api.Controllers.Validation.GameModel;

public class GameInsertModelValidator : GameFormModelValidator

{
    public GameInsertModelValidator()
    {
        RuleFor(u => u.Name).NotNull();
    }
}