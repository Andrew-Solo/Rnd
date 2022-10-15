namespace Rnd.Api.Client.Controllers;

public abstract class Selector<TModel, TFormModel> 
    where TModel : class 
    where TFormModel : class
{
    protected Selector(HttpClient client, Uri path, IController<TModel, TFormModel> controller)
    {
        _controller = controller;
        Client = client;
        Path = path;
    }
    
    public virtual TModel Value => _controller.GetOrExceptionAsync(Id).Result;
    
    public virtual bool Exist => _controller.ExistAsync(Id).Result;
    
    public virtual async Task<TModel> Edit(TFormModel form)
    {
        return await _controller.EditOrExceptionAsync(form, Id);
    }
    
    public virtual async Task<TModel> Delete()
    {
        return await _controller.DeleteOrExceptionAsync(Id);
    }
    
    protected HttpClient Client { get; }
    protected Uri Path { get; }
    protected Guid Id => new(Path.Segments.Last().Trim('/'));
    
    private readonly IController<TModel, TFormModel> _controller;
}