namespace Rnd.Api.Client.Controllers;

public class EmptySelector<TModel, TFormModel>  : Selector<TModel, TFormModel> 
    where TModel : class 
    where TFormModel : class
{
    public EmptySelector(HttpClient client, Uri path, IController<TModel, TFormModel> controller) 
        : base(client, path, controller) { }
}