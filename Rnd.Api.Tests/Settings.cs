using Rnd.Api.Client;
using Rnd.Api.Client.Models.Basic.User;

namespace Rnd.Api.Tests;

public static class Settings
{
    public static Uri ApiBaseUri => new("https://localhost:7171/");

    public static UserRegisterModel DefaultUser => new()
    {   
        Email = "test@test.test",
        Login = "TestUser",
        Password = "P@ssw0rd",
    };
    
    public static BasicClient TestClient
    {
        get => _testClient ?? throw new NullReferenceException("Object not initialized");
        set => _testClient = value;
    }

    public static BasicClient GetBasicClient() => new BasicClient(ApiBaseUri);
    
    private static BasicClient? _testClient;
}