namespace Rnd.Api.Client.Controllers;

public abstract class Selector
{
    protected Selector(HttpClient client, Uri path)
    {
        Client = client;
        Path = path;
    }
    
    protected HttpClient Client { get; }
    protected Uri Path { get; }
}