using FluentValidation;
using Rnd.Api.Client.Models.Basic.Game;

namespace Rnd.Api.Controllers.Validation.GameModel;

public class GameFormModelValidator : AbstractValidator<GameFormModel> 
{
    public GameFormModelValidator()
    {
        RuleFor(u => u.Name).Matches("^[A-Za-z0-9_]*$").Length(4, 32);
        
        RuleFor(u => u.Title).Length(1, 50);
        
        RuleFor(u => u.Description).Length(0, 200);
    }
}