using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rnd.Api.Client;
using Rnd.Api.Client.Clients;
using Rnd.Api.Client.Controllers;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Client.Responses;
using Rnd.Api.Helpers;

namespace Rnd.Api.Tests.Controllers.Basic;

[TestClass]
public class GamesControllerTests
{
    public BasicClient Client => Settings.TestClient;
    
    [ClassInitialize]
    public static async Task ClassInit(TestContext context)
    {
        
    }
    
    [ClassCleanup]
    public static async Task ClassCleanup()
    {
        
    }
    
    [TestMethod]
    public async Task GetTest()
    {
        var response = await Client.Games.GetAsync();

        if (!response.IsSuccess)
        {
            throw new Exception(response.Errors?.ToString());
        }
        
        
    }
    
    [TestMethod]
    public async Task LoginTest()
    {
        var client = Settings.GetBasicClient();

        await client.LoginAsync(Settings.DefaultUser.Login ?? Settings.DefaultUser.Email, 
            Settings.DefaultUser.Password);
        
        AssertExtended.IsReady(client);
        Assert.AreEqual(Settings.DefaultUser.Email, client.User.Email);
        Assert.AreEqual(Settings.DefaultUser.Login, client.User.Login);
        Assert.AreEqual(Hash.GenerateStringHash(Settings.DefaultUser.Password), client.User.PasswordHash);
    }
    
    [TestMethod]
    public async Task LoginWithEmailTest()
    {
        var client = Settings.GetBasicClient();

        await client.LoginAsync(Settings.DefaultUser.Email, Settings.DefaultUser.Password);
        
        AssertExtended.IsReady(client);
        Assert.AreEqual(Settings.DefaultUser.Email, client.User.Email);
        Assert.AreEqual(Settings.DefaultUser.Login, client.User.Login);
        Assert.AreEqual(Hash.GenerateStringHash(Settings.DefaultUser.Password), client.User.PasswordHash);
    }
    
    [TestMethod]
    public async Task RegisterTest()
    {
        var client = Settings.GetBasicClient();

        var user = new UserRegisterModel
        {
            Email = "register@register.register",
            Password = "P@ssw0rd",
        };

        await using var cleanup = new Cleanup(() => client.DeleteAccountAsync());
        await client.RegisterAsync(user);
    
        AssertExtended.IsReady(client);
        Assert.AreEqual(user.Email, client.User.Email);
        Assert.AreEqual(user.Email, client.User.Login);
        Assert.AreEqual(Hash.GenerateStringHash(user.Password), client.User.PasswordHash);
    }
    
    [TestMethod]
    public async Task EditTest()
    {
        var client = Settings.GetBasicClient();

        var user = new UserRegisterModel
        {
            Email = "register@register.register",
            Password = "P@ssw0rd",
        };

        await using var cleanup = new Cleanup(() => client.DeleteAccountAsync());
        await client.RegisterAsync(user);

        AssertExtended.IsReady(client);
        
        var userEdit = new UserEditModel
        {
            Id = client.User.Id,
            Login = "NewTestLogin",
            Email = "new@new.new",
            Password = "NewP@ssw0rd",
        };

        await client.EditAccountAsync(userEdit);
        
        AssertExtended.IsReady(client);

        await client.LoginAsync(userEdit.Email, userEdit.Password);
    
        AssertExtended.IsReady(client);
        Assert.AreEqual(userEdit.Email, client.User.Email);
        Assert.AreEqual(userEdit.Login, client.User.Login);
        Assert.AreEqual(Hash.GenerateStringHash(userEdit.Password), client.User.PasswordHash);
    }
    
    [TestMethod]
    public async Task DeleteTest()
    {
        var client = Settings.GetBasicClient();

        var user = new UserRegisterModel
        {
            Email = "register@register.register",
            Password = "P@ssw0rd",
        };
        
        await client.RegisterAsync(user);
    
        AssertExtended.IsReady(client);

        await client.DeleteAccountAsync();
        
        Assert.AreEqual(ClientStatus.NotAuthorized, client.Status);

        var result = await client.LoginAsync(user.Email, user.Password);
        
        Assert.AreEqual(ClientStatus.AuthorizationError, client.Status);
        Assert.AreEqual(ResponseStatus.NotFound, result.Status);
    }
    
    [TestMethod]
    public async Task LogoutTest()
    {
        var client = Settings.GetBasicClient();

        await client.LoginAsync(Settings.DefaultUser.Email, Settings.DefaultUser.Password);
        
        AssertExtended.IsReady(client);

        await client.LogoutAsync();
        
        Assert.AreEqual(ClientStatus.NotAuthorized, client.Status);
    }
}