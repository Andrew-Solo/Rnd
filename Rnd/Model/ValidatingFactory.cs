using FluentValidation;

namespace Rnd.Model;

public abstract class ValidatingFactory<TEntity, TForm, TValidator> : Factory<TEntity, TForm> 
    where TEntity : Entity<TForm> 
    where TForm : struct
    where TValidator : AbstractValidator<TForm>, new()
{
    public async Task<Result> TryCreateAsync(TForm form)
    {
        var validator = new TValidator();
        var result = await validator.ValidateAsync(form);

        return result.IsValid 
            ? new Result(Create(form)) 
            : new Result(null!, false, result.ToMessage($"{typeof(TEntity).Name} creation validation error"));
    }

    public readonly record struct Result(TEntity Entity, bool Success = true, Message? Errors = null);
}