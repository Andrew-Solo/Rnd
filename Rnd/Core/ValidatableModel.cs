using FluentValidation;
using Rnd.Results;

namespace Rnd.Core;

public abstract class ValidatableModel<TModel, TForm, TUpdateValidator, TClearValidator> : FormModel<TModel, TForm> 
    where TModel : ValidatableModel<TModel, TForm, TUpdateValidator, TClearValidator>
    where TForm : struct
    where TUpdateValidator : AbstractValidator<TForm>, new()
    where TClearValidator : AbstractValidator<TForm>, new()
{
    public async Task<ValidationResult> ValidateUpdateAsync(TForm form)
    {
        var validator = new TUpdateValidator();
        var result = await validator.ValidateAsync(form);
        return new ValidationResult(result.IsValid, result.ToMessage());
    }

    public async Task<Result<TModel>> TryUpdateAsync(TForm form)
    {
        return Result.Validated(
             await ValidateUpdateAsync(form),
            () => Update(form),
            "Объект обновлен успешно");
    } 
    
    public async Task<ValidationResult> ValidateClearAsync(TForm form)
    {
        var validator = new TClearValidator();
        var result = await validator.ValidateAsync(form);
        return new ValidationResult(result.IsValid, result.ToMessage());
    }

    public async Task<Result<TModel>> TryClearAsync(TForm form)
    {
        return Result.Validated(
            await ValidateUpdateAsync(form),
            () => Clear(form),
            "Объект обновлен успешно");
    }
}