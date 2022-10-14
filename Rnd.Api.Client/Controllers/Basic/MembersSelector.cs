namespace Rnd.Api.Client.Controllers.Basic;

public class MembersSelector : Selector
{
    public MembersSelector(HttpClient client, Uri path) : base(client, path) { }
    
    public Characters Characters => new(Client, Path);
}