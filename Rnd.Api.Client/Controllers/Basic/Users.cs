using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Client.Responses;

namespace Rnd.Api.Client.Controllers.Basic;

public class Users : Controller<UserModel, UserFormModel, UsersSelector>
{
    public Users(HttpClient client, Uri uri) : base(client, uri) { }
    
    protected override string Name => nameof(Users);

    public override Task<Response<List<UserModel>>> ListAsync()
    {
        throw new NotSupportedException();
    }

    public async Task<Response<UserModel>> LoginAsync(string login, string password)
    {
        var response = await Client.GetAsync(GetUri().WithParameters(new {Login = login, Password = password}));
        return await Response<UserModel>.Create(response);
    }
}