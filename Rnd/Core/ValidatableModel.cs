using FluentValidation;
using Rnd.Result;

namespace Rnd.Core;

public abstract class ValidatableModel<TForm, TUpdateValidator, TClearValidator> : FormModel<TForm> 
    where TForm : struct
    where TUpdateValidator : AbstractValidator<TForm>, new()
    where TClearValidator : AbstractValidator<TForm>, new()
{
    public async Task<EmptyResult> ValidateUpdateAsync(TForm form)
    {
        var validator = new TUpdateValidator();
        var result = await validator.ValidateAsync(form);
        return result.IsValid 
            ? EmptyResult.Empty(result.ToMessage()) 
            : EmptyResult.Error(result.ToMessage());
    }

    public async Task<EmptyResult> TryUpdateAsync(TForm form)
    {
        var result = await ValidateUpdateAsync(form);

        if (result.IsFailed) return result;
        
        Update(form);
        
        return result;
    } 
    
    public async Task<EmptyResult> ValidateClearAsync(TForm form)
    {
        var validator = new TClearValidator();
        var result = await validator.ValidateAsync(form);
        return result.IsValid 
            ? EmptyResult.Empty(result.ToMessage()) 
            : EmptyResult.Error(result.ToMessage());
    }

    public async Task<EmptyResult> TryClearAsync(TForm form)
    {
        var result = await ValidateClearAsync(form);
        
        if (result.IsFailed) return result;
        
        Clear(form);
        
        return result;
    }
}