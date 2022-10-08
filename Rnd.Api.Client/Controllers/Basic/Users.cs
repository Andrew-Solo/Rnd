﻿using Rnd.Api.Client.Models.Basic.User;

namespace Rnd.Api.Client.Controllers.Basic;

public class Users : Controller<UserModel, UserRegisterModel, UserEditModel, UsersSelector>
{
    public Users(HttpClient client, string path, bool suppressEmbedding = false) : base(client, path, suppressEmbedding) { }
    
    protected override string Name => nameof(Users);

    public override Task<Response<List<UserModel>>> ListAsync()
    {
        throw new NotSupportedException();
    }

    public async Task<Response<UserModel>> LoginAsync(string login, string password)
    {
        var response = await Client.GetAsync(GetUri());
        return await Response<UserModel>.Create(response);
    }
}