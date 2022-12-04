using FluentValidation;
using Rnd.Api.Client.Models.Basic.Game;

namespace Rnd.Api.Controllers.Validation.GameModel;

public class GameFormModelValidator : AbstractValidator<GameFormModel> 
{
    public GameFormModelValidator()
    {
        RuleFor(u => u.Name)
            .Matches("^[A-Za-z0-9_]*$").WithMessage("Имя содержит запрещенные символы, разрешены только латинские буквы, " +
                                                    "цифры и нижнее подчеркивание")
            .Length(4, 32).WithMessage("Длина имени должна быть от 4 до 32 символов, сейчас {TotalLength}");
        
        RuleFor(u => u.Title)
            .Length(1, 50).WithMessage("Длина названия должна быть от 1 до 50 символов, сейчас {TotalLength}");
        
        RuleFor(u => u.Description)
            .Length(0, 200).WithMessage("Длина описания не должна превышать 200 символов, сейчас {TotalLength}");
    }
}