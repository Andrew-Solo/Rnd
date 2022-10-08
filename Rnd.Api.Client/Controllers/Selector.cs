namespace Rnd.Api.Client.Controllers;

public abstract class Selector
{
    protected Selector(HttpClient client, string path)
    {
        Client = client;
        Path = path;
    }
    
    protected HttpClient Client { get; }
    protected string Path { get; }
}