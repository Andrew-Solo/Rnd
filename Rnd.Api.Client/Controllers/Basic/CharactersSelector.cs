namespace Rnd.Api.Client.Controllers.Basic;

public class CharactersSelector : Selector
{
    public CharactersSelector(HttpClient client, string path) : base(client, path) { }
}