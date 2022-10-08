namespace Rnd.Api.Client.Controllers;

public class EmptySelector : Selector
{
    public EmptySelector(HttpClient client, string path) : base(client, path) { }
}