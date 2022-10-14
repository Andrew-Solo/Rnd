namespace Rnd.Api.Client.Controllers.Basic;

public class CharactersSelector : Selector
{
    public CharactersSelector(HttpClient client, Uri path) : base(client, path) { }
    
    public Fields Fields => new(Client, Path);
    public Parameters Parameters => new(Client, Path);
    public Resources Resources => new(Client, Path);
    public Effects Effects => new(Client, Path);
}