namespace Rnd.Api.Client.Controllers.Basic;

public class UsersSelector : Selector
{
    public UsersSelector(HttpClient client, string path) : base(client, path) { }

    public Games Games => new(Client, Path);
}