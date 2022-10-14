namespace Rnd.Api.Client.Controllers;

public class EmptySelector : Selector
{
    public EmptySelector(HttpClient client, Uri path) : base(client, path) { }
}