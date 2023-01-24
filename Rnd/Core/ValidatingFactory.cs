using FluentValidation;
using Rnd.Result;

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
    
    public async Task<Result<TModel>> TryCreateAsync(TForm form)
    {
        var result = await ValidateAsync(form);
        return result.IsValid 
            ? Result<TModel>.Ok(Create(form)) 
            : Result<TModel>.Error(result.Errors);
    }
}