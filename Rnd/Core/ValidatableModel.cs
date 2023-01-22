using FluentValidation;

namespace Rnd.Core;

public abstract class ValidatableModel<TForm, TUpdateValidator, TClearValidator> : FormModel<TForm> 
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

    public async Task<ValidationResult> TryUpdateAsync(TForm form)
    {
        var result = await ValidateUpdateAsync(form);

        if (!result.IsValid) return result;
        
        Update(form);
        
        return result;
    } 
    
    public async Task<ValidationResult> ValidateClearAsync(TForm form)
    {
        var validator = new TClearValidator();
        var result = await validator.ValidateAsync(form);
        return new ValidationResult(result.IsValid, result.ToMessage());
    }

    public async Task<ValidationResult> TryClearAsync(TForm form)
    {
        var result = await ValidateClearAsync(form);
        
        if (!result.IsValid) return result;
        
        Clear(form);
        
        return result;
    }
}