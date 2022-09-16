using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Rnd.Api.Controllers.Validation;

public static class Extensions
{
    public static async Task ValidateAndFillModelStateAsync<T>(this AbstractValidator<T> validator, T item, ModelStateDictionary modelState)
    {
        var result = await validator.ValidateAsync(item);
        result.AddToModelState(modelState);
    }
}