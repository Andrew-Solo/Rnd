using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Responses;

namespace Rnd.Api.Controllers.Validation;

public static class Extensions
{
    public static async Task ValidateAndFillModelStateAsync<T>(this AbstractValidator<T> validator, T item, ModelStateDictionary modelState)
    {
        var result = await validator.ValidateAsync(item);
        result.AddToModelState(modelState);
    }

    public static async Task ValidateForm<TValidator, TForm>(this ModelStateDictionary modelState, TForm from)
        where TValidator : AbstractValidator<TForm>, new()
    {
        var validator = new TValidator();
        await validator.ValidateAndFillModelStateAsync(from, modelState);
    }

    public static async Task CheckExist<TEntity>(this ModelStateDictionary modelState,
        DbSet<TEntity> dbSet, Expression<Func<TEntity, bool>> getOverlap) where TEntity : class
    {
        await CheckNotExist(modelState, dbSet, getOverlap, true);
    }
    
    public static async Task CheckNotExist<TEntity>(this ModelStateDictionary modelState, 
        DbSet<TEntity> dbSet, Expression<Func<TEntity,bool>> getOverlap, bool invertResult = false)
        where TEntity : class
    {
        var overlap = await dbSet.AnyAsync(getOverlap);
        
        if (overlap && !invertResult)
        {
            modelState.AddModelError(typeof(TEntity).Name, $"Объект {typeof(TEntity).Name} уже существет");
        }
        
        if (!overlap && invertResult)
        {
            modelState.AddModelError(typeof(TEntity).Name, $"Объект {typeof(TEntity).Name} не существет");
        }
    }
    
    #region Responses

    public static Errors ToErrors(this ModelStateDictionary modelState)
    {
        return new Errors(modelState.ToDictionary(
            model => model.Key,
            model => model.Value?.Errors.Select(error => error.ErrorMessage).ToArray()
                     ?? Array.Empty<string>()));
    }
    
    public static NotFoundObjectResult NotFound<T>(this ControllerBase controller)
    {
        controller.ModelState.AddModelError(typeof(T).Name, $"Объект {typeof(T).Name} не найден");
        return controller.NotFound(controller.ModelState.ToErrors());
    }
    
    public static ObjectResult Forbidden<T>(this ControllerBase controller)
    {
        controller.ModelState.AddModelError(typeof(T).Name, $"У вас нет прав доступа к объекту {typeof(T).Name}");
        return controller.StatusCode(StatusCodes.Status403Forbidden, controller.ModelState.ToErrors());
    }

    #endregion
}