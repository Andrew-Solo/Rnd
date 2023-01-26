using FluentValidation;
using Rnd.Results;

namespace Rnd.Core;

public abstract class ValidatingFactory<TModel, TForm, TValidator> : Factory<TModel, TForm> 
    where TModel : FormModel<TModel, TForm> 
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
        return Result.Validated(
            await ValidateAsync(form), 
            () => Create(form), 
            "Объект создан успешно"
        );
    }
}