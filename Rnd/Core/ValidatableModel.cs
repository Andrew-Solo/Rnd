using FluentValidation;

namespace Rnd.Core;

public abstract class ValidatableModel<TForm, TUpdateValidator, TClearValidator> : Model<TForm> 
    where TForm : struct
    where TUpdateValidator : AbstractValidator<TForm>, new()
    where TClearValidator : AbstractValidator<TForm>, new()
{
    public async Task<Result> TryUpdateAsync(TForm form)
    {
        var validator = new TUpdateValidator();
        var result = await validator.ValidateAsync(form);

        if (!result.IsValid) return new Result(false, result.ToMessage($"{GetType().Name} update validation error"));
        
        Update(form);
        
        return new Result();
    }
    
    public async Task<Result> TryClearAsync(TForm form)
    {
        var validator = new TClearValidator();
        var result = await validator.ValidateAsync(form);

        if (!result.IsValid) return new Result(false, result.ToMessage($"{GetType().Name} clear validation error"));

        Clear(form);

        return new Result();
    }
    
    public readonly record struct Result(bool Success = true, Message? Message = null);
}