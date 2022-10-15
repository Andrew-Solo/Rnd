namespace Rnd.Api.Client.Controllers;

public abstract class Selector
{
    protected Selector(HttpClient client, Uri path)
    {
        Client = client;
        Path = path;
    }
    
    //TODO
    // public virtual TModel Value { get; }
    // public virtual bool Exist { get; }
    //
    // public async virtual Task<TModel> Edit(TFormModel form)
    // {
    //     
    // }
    //
    // public async virtual Task<TModel> Delete()
    // {
    //     
    // }
    
    protected HttpClient Client { get; }
    protected Uri Path { get; }
}