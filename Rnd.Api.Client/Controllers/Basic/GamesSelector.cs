namespace Rnd.Api.Client.Controllers.Basic;

public class GamesSelector : Selector
{
    public GamesSelector(HttpClient client, string path) : base(client, path) { }
    
    public Members Members => new(Client, Path, true);
}