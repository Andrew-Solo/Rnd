using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rnd.Api.Client.Controllers;

namespace Rnd.Api.Controllers.Validation;

public static class Extensions
{
    public static async Task ValidateAndFillModelStateAsync<T>(this AbstractValidator<T> validator, T item, ModelStateDictionary modelState)
    {
        var result = await validator.ValidateAsync(item);
        result.AddToModelState(modelState);
    }
    
    public static NotFoundObjectResult NotFound<T>(this ControllerBase controller)
    {
        controller.ModelState.AddModelError(typeof(T).Name, $"{typeof(T).Name} not found");
        return controller.NotFound(controller.ModelState.ToErrors());
    }

    public static Errors ToErrors(this ModelStateDictionary modelState)
    {
        return new Errors(modelState.ToDictionary(
            model => model.Key,
            model => model.Value?.Errors.Select(error => error.ErrorMessage).ToArray()
                     ?? Array.Empty<string>()));
    }
}