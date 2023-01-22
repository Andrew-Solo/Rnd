using FluentValidation;
using Rnd.Results;

namespace Rnd.Core;

public abstract class ValidatingFactory<TModel, TForm, TValidator> : Factory<TModel, TForm> 
    where TModel : FormModel<TForm> 
    where TForm : struct
    where TValidator : AbstractValidator<TForm>, new()
{
    public async Task<ValidationResult> ValidateAsync(TForm form)
    {
        var validator = new TValidator();
        var result = await validator.ValidateAsync(form);
        return new ValidationResult(result.IsValid, result.ToMessage());
    }
    
    public async Task<Result> TryCreateAsync(TForm form)
    {
        var result = await ValidateAsync(form);
        return new Result(result.IsValid ? Create(form) : null!, result.IsValid, result.Errors);
    }

    public readonly record struct Result(TModel Model, bool IsValid, Message Errors);
}