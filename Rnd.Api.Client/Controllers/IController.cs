using Rnd.Api.Client.Responses;

namespace Rnd.Api.Client.Controllers;

// ReSharper disable once TypeParameterCanBeVariant
public interface IController<TModel, TFormModel> 
    where TModel : class
    where TFormModel : class
{
    public Task<TModel> GetOrExceptionAsync(Guid? id = null);
    public Task<Response<TModel>> GetAsync(Guid? id = null);
    public Task<List<TModel>> ListOrExceptionAsync();
    public Task<Response<List<TModel>>> ListAsync();
    public Task<bool> ExistAsync(Guid? id = null);
    public Task<Response<TModel>> ValidateFormAsync(TFormModel form, bool insert = false);
    public Task<TModel> AddOrExceptionAsync(TFormModel form);
    public Task<Response<TModel>> AddAsync(TFormModel form);
    public Task<TModel> EditOrExceptionAsync(TFormModel form, Guid? id = null);
    public Task<Response<TModel>> EditAsync(TFormModel form, Guid? id = null);
    public Task<TModel> DeleteOrExceptionAsync(Guid? id = null);
    public Task<Response<TModel>> DeleteAsync(Guid? id = null);
}