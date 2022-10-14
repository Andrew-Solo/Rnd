namespace Rnd.Api.Client.Controllers.Basic;

public class UsersSelector : Selector
{
    public UsersSelector(HttpClient client, Uri path) : base(client, path) { }

    public Games Games => new(Client, Path, true);
}