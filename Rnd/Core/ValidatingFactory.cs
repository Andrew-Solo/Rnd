using FluentValidation;

namespace Rnd.Core;

public abstract class ValidatingFactory<TModel, TForm, TValidator> : Factory<TModel, TForm> 
    where TModel : Model<TForm> 
    where TForm : struct
    where TValidator : AbstractValidator<TForm>, new()
{
    public async Task<Result> TryCreateAsync(TForm form)
    {
        var validator = new TValidator();
        var result = await validator.ValidateAsync(form);

        return result.IsValid 
            ? new Result(Create(form)) 
            : new Result(null!, false, result.ToMessage($"{typeof(TModel).Name} creation validation error"));
    }

    public readonly record struct Result(TModel Model, bool Success = true, Message? Errors = null);
}