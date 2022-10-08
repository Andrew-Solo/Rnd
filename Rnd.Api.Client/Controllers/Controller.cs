using Microsoft.VisualBasic;

namespace Rnd.Api.Client.Controllers;

public abstract class Controller<TModel, TAddModel, TEditModel, TSelector> 
    where TModel : class
    where TAddModel : class
    where TEditModel : class
    where TSelector : Selector
{
    protected Controller(HttpClient client, string path)
    {
        Client = client;
        _path = path;
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
        var fullname = Path.Combine(_path, Name);
        var fullnameId = id == null ? fullname : Path.Combine(fullname, id.GetValueOrDefault().ToString());
        return new Uri(fullnameId);
    }

    private readonly string _path;

    private TSelector CreateSelector(Guid id)
    {
        var selector = Activator.CreateInstance(typeof(TSelector), Client, GetUri(id));
        return selector as TSelector ?? throw new InvalidOperationException("Error on selector creating");
    }
}
