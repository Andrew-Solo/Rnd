using System.Net.Http.Headers;
using Rnd.Api.Client.Controllers.Basic;
using Rnd.Api.Client.Exceptions;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Client.Responses;

namespace Rnd.Api.Client.Clients;

public class ApiClient
{
    public ApiClient(Uri hostUri)
    {
        Status = ClientStatus.NotAuthorized;

        HostUri = hostUri;
        
        Client = new HttpClient();
        Client.BaseAddress = hostUri;
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    public ClientStatus Status { get; private set; }
    
    public Users Users => new(Client, BaseUri);
    public Games Games => Users[User.Id].Games;
    
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
    
    public async Task<Response<UserModel>> RegisterAsync(UserFormModel form)
    {
        return Authorize(await Users.AddAsync(form));
    }
    
    public async Task<Response<UserModel>> EditAccountAsync(UserFormModel form)
    {
        var response = await Users.EditAsync(form, User.Id);
        return response.IsSuccess 
            ? Authorize(response) 
            : response;
    }
    
    public async Task<Response<UserModel>> DeleteAccountAsync()
    {
        var response = await Users.DeleteAsync(User.Id);

        if (!response.IsSuccess) return response;

        Status = ClientStatus.NotAuthorized;
        _authorization = null;

        return response;
    }
    
    public Task LogoutAsync()
    {
        Status = ClientStatus.NotAuthorized;
        _authorization = null;
        
        return Task.CompletedTask;
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
    
    protected Uri HostUri { get; }
    protected Uri BaseUri => new(HostUri, "/");
    protected HttpClient Client { get; }
}