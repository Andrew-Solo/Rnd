using Rnd.Api.Client.Exceptions;
using Rnd.Api.Client.Responses;

namespace Rnd.Api.Client.Controllers;

public abstract class Controller<TModel, TFormModel, TSelector> 
    where TModel : class
    where TFormModel : class
    where TSelector : Selector
{
    protected Controller(HttpClient client, Uri uri, bool suppressEmbedding)
    {
        Client = client;
        
        _uri = uri;
        _suppressEmbedding = suppressEmbedding;
    }

    public TSelector this[Guid id] => CreateSelector(id);

    #region Api methods

    public async Task<TModel> GetOrExceptionAsync(Guid? id = null)
    {
        return await ExecuteOrExceptionAsync(GetAsync, id);
    }
    
    public virtual async Task<Response<TModel>> GetAsync(Guid? id = null)
    {
        var response = await Client.GetAsync(GetUri(id.GetValueOrDefault(Guid.Empty)));
        return await Response<TModel>.Create(response);
    }
    
    public async Task<List<TModel>> ListOrExceptionAsync()
    {
        return await ExecuteOrExceptionAsync(ListAsync);
    }
    
    public virtual async Task<Response<List<TModel>>> ListAsync()
    {
        var response = await Client.GetAsync(GetUri());
        return await Response<List<TModel>>.Create(response);
    }
    
    public async Task<TModel> AddOrExceptionAsync(TFormModel add)
    {
        return await ExecuteOrExceptionAsync(AddAsync, add);
    }
    
    public virtual async Task<Response<TModel>> AddAsync(TFormModel add)
    {
        var response = await Client.PostAsJsonAsync(GetUri(), add);
        return await Response<TModel>.Create(response);
    }
    
    public async Task<TModel> EditOrExceptionAsync(TFormModel form, Guid? id = null)
    {
        return await ExecuteOrExceptionAsync(EditAsync, form, id);
    }

    public virtual async Task<Response<TModel>> EditAsync(TFormModel form, Guid? id = null)
    {
        var response = await Client.PutAsJsonAsync(GetUri(), form);
        return await Response<TModel>.Create(response);
    }
    
    public async Task<TModel> DeleteOrExceptionAsync(Guid? id = null)
    {
        return await ExecuteOrExceptionAsync(DeleteAsync, id);
    }
    
    public virtual async Task<Response<TModel>> DeleteAsync(Guid? id = null)
    {
        var response = await Client.DeleteAsync(GetUri(id.GetValueOrDefault(Guid.Empty)));
        return await Response<TModel>.Create(response);
    }

    #endregion
    
    protected HttpClient Client { get; }
    protected abstract string Name { get; }

    protected Uri GetUri(Guid? id = null)
    {
        var fullname = new Uri(_uri, $"{Name}/");
        
        return id == null 
            ? fullname 
            : new Uri(fullname, $"{id}/");
    }

    #region ExecuteOrExceptionAsync

    protected async Task<T> ExecuteOrExceptionAsync<T, TArg, TArg1>(Func<TArg, TArg1, Task<Response<T>>> requestFn, TArg arg, TArg1 arg1) 
        where T : class
    {
        var response = await requestFn(arg, arg1);

        if (!response.IsSuccess) throw new NotSuccessResponseException(response.Errors);

        return response.Value ?? throw new NullReferenceException("API request returned null");
    }
    
    protected async Task<T> ExecuteOrExceptionAsync<T, TArg>(Func<TArg, Task<Response<T>>> requestFn, TArg arg) 
        where T : class
    {
        var response = await requestFn(arg);

        if (!response.IsSuccess) throw new NotSuccessResponseException(response.Errors);

        return response.Value ?? throw new NullReferenceException("API request returned null");
    }
    
    protected async Task<T> ExecuteOrExceptionAsync<T>(Func<Task<Response<T>>> requestFn) 
        where T : class
    {
        var response = await requestFn();

        if (!response.IsSuccess) throw new NotSuccessResponseException(response.Errors);

        return response.Value ?? throw new NullReferenceException("API request returned null");
    }

    #endregion

    private readonly Uri _uri;
    private readonly bool _suppressEmbedding;

    private TSelector CreateSelector(Guid id)
    {
        var uri = _suppressEmbedding ? GetBasePath() : GetUri(id);
        
        var selector = Activator.CreateInstance(typeof(TSelector), Client, uri);
        
        return selector as TSelector ?? throw new InvalidOperationException("Error on selector creating");
    }

    private Uri GetBasePath() => new(_uri.GetLeftPart(UriPartial.Authority) + _uri.Segments[0] + _uri.Segments[1]);
}
