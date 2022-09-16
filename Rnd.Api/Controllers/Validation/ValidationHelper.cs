using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Rnd.Api.Controllers.Validation;

public static class ValidationHelper
{
    public static async Task ValidateAsync<TValidator, TItem>(TItem item, ModelStateDictionary modelState) 
        where TValidator : AbstractValidator<TItem>, new()
    {
        var validator = new TValidator();
        await validator.ValidateAndFillModelStateAsync(item, modelState);
    }
}