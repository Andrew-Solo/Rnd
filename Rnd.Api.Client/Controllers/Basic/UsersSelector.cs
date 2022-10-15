using Rnd.Api.Client.Models.Basic.User;

namespace Rnd.Api.Client.Controllers.Basic;

public class UsersSelector : Selector<UserModel, UserFormModel>
{
    public UsersSelector(HttpClient client, Uri path, IController<UserModel, UserFormModel> controller) 
        : base(client, path, controller) { }

    public Games Games => new(Client, Path);
}