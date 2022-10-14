﻿using System.Net.Http.Headers;
using Rnd.Api.Client.Controllers;
using Rnd.Api.Client.Controllers.Basic;
using Rnd.Api.Client.Models.Basic.User;

namespace Rnd.Api.Client;

public class BasicClient
{
    public BasicClient(Uri address)
    {
        Status = ClientStatus.NotAuthorized;
        
        ApiType = _apiType;
        
        Client = new HttpClient();
        Client.BaseAddress = address;
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    public ClientStatus Status { get; private set; }

    public Games Games => new(Client, ApiType);
    public Members Members => new(Client, ApiType);
    
    #region Authorization

    public Response<UserModel> Authorization
    {
        get => _authorization ?? throw new NotReadyException();
        private set => _authorization = value;
    }

    public UserModel User => Authorization.Value ?? throw new NotReadyException();

    public async Task<Response<UserModel>> LoginAsync(Guid id)
    {
        return Authorize(await Users.GetAsync(id));
    }
    
    public async Task<Response<UserModel>> LoginAsync(string login, string password)
    {
        return Authorize(await Users.LoginAsync(login, password));
    }
    
    public async Task<Response<UserModel>> RegisterAsync(UserRegisterModel register)
    {
        return Authorize(await Users.AddAsync(register));
    }
    
    public async Task<Response<UserModel>> EditAccountAsync(UserEditModel edit)
    {
        return Authorize(await Users.EditAsync(edit));
    }
    
    public async Task<Response<UserModel>> DeleteAccountAsync()
    {
        var response = await Users.DeleteAsync(User.Id);

        if (!response.IsSuccess) return response;

        Status = ClientStatus.NotAuthorized;
        _authorization = null;

        return response;
    }

    private Response<UserModel>? _authorization;
    
    private Response<UserModel> Authorize(Response<UserModel> response)
    {
        Authorization = response;
        
        Status = response.IsSuccess 
            ? ClientStatus.Ready 
            : ClientStatus.AuthorizationError;

        return response;
    }

    #endregion

    protected virtual Users Users => new(Client, ApiType);
    protected virtual Uri ApiType { get; }
    protected HttpClient Client { get; }
    
    private readonly Uri _apiType = new(nameof(Controllers.Basic).ToLower());
}