using Microsoft.VisualBasic;

namespace Rnd.Api.Client.Controllers;

public abstract class Controller<TModel, TAddModel, TEditModel, TSelector> 
    where TModel : class
    where TAddModel : class
    where TEditModel : class
    where TSelector : Selector
{
    protected Controller(HttpClient client, Uri path, bool suppressEmbedding)
    {
        Client = client;
        
        _path = path;
        _apiType = new Uri(_path.Segments[0]);
        _suppressEmbedding = suppressEmbedding;
    }

    public TSelector this[Guid id] => CreateSelector(id);
    
    public virtual async Task<Response<TModel>> GetAsync(Guid? id = null)
    {
        var response = await Client.GetAsync(GetUri(id.GetValueOrDefault(Guid.Empty)));
        return await Response<TModel>.Create(response);
    }
    
    public virtual async Task<Response<List<TModel>>> ListAsync()
    {
        var response = await Client.GetAsync(GetUri());
        return await Response<List<TModel>>.Create(response);
    }
    
    public virtual async Task<Response<TModel>> AddAsync(TAddModel add)
    {
        var response = await Client.PostAsJsonAsync(GetUri(), add);
        return await Response<TModel>.Create(response);
    }

    public virtual async Task<Response<TModel>> EditAsync(TEditModel edit)
    {
        var response = await Client.PutAsJsonAsync(GetUri(), edit);
        return await Response<TModel>.Create(response);
    }
    
    public virtual async Task<Response<TModel>> DeleteAsync(Guid? id = null)
    {
        var response = await Client.DeleteAsync(GetUri(id.GetValueOrDefault(Guid.Empty)));
        return await Response<TModel>.Create(response);
    }
    
    protected HttpClient Client { get; }
    protected abstract string Name { get; }

    protected Uri GetUri(Guid? id = null)
    {
        var fullname = new Uri(_path, Name.ToLower());
        
        return id == null 
            ? fullname 
            : new Uri(fullname, id.ToString());
    }

    private readonly Uri _apiType;
    private readonly Uri _path;
    private readonly bool _suppressEmbedding;

    private TSelector CreateSelector(Guid id)
    {
        var uri = _suppressEmbedding ? _apiType : GetUri(id);
        
        var selector = Activator.CreateInstance(typeof(TSelector), Client, uri);
        
        return selector as TSelector ?? throw new InvalidOperationException("Error on selector creating");
    }
}
