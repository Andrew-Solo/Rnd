using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rnd.Api.Client.Clients;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Client.Responses;
using Rnd.Api.Helpers;

namespace Rnd.Api.Tests.Controllers.Basic;

[TestClass]
public class UsersControllerTests
{
    [TestMethod]
    public async Task GetTest()
    {
        var client = Settings.GetBasicClient();

        await client.LoginAsync(Settings.TestClient.User.Id);
        
        AssertExtended.IsReady(client);
        Assert.AreEqual(Settings.TestUser.Email, client.User.Email);
        Assert.AreEqual(Settings.TestUser.Login, client.User.Login);
        Assert.AreEqual(Settings.TestUser.PasswordHash, client.User.PasswordHash);
    }
    
    [TestMethod]
    public async Task LoginTest()
    {
        var client = Settings.GetBasicClient();

        await client.LoginAsync(Settings.TestUser.Login, Settings.TestUserForm.Password!);
        
        AssertExtended.IsReady(client);
        Assert.AreEqual(Settings.TestUser.Email, client.User.Email);
        Assert.AreEqual(Settings.TestUser.Login, client.User.Login);
        Assert.AreEqual(Settings.TestUser.PasswordHash, client.User.PasswordHash);
    }
    
    [TestMethod]
    public async Task LoginWithEmailTest()
    {
        var client = Settings.GetBasicClient();

        await client.LoginAsync(Settings.TestUser.Email, Settings.TestUserForm.Password!);
        
        AssertExtended.IsReady(client);
        Assert.AreEqual(Settings.TestUser.Email, client.User.Email);
        Assert.AreEqual(Settings.TestUser.Login, client.User.Login);
        Assert.AreEqual(Settings.TestUser.PasswordHash, client.User.PasswordHash);
    }
    
    [TestMethod]
    public async Task RegisterTest()
    {
        var client = Settings.GetBasicClient();

        var user = new UserFormModel
        {
            Email = "register@register.register",
            Password = "P@ssw0rd",
        };

        var response = await client.RegisterAsync(user);
        await using var cleanup = new Cleanup(() => client.Users.DeleteAsync(response.Value?.Id));
    
        AssertExtended.IsReady(client);
        Assert.AreEqual(user.Email, client.User.Email);
        Assert.AreEqual(user.Email, client.User.Login);
        Assert.AreEqual(Hash.GenerateStringHash(user.Password), client.User.PasswordHash);
    }
    
    [TestMethod]
    public async Task EditTest()
    {
        var client = Settings.GetBasicClient();

        var user = new UserFormModel
        {
            Email = "register@register.register",
            Password = "P@ssw0rd",
        };

        var response = await client.RegisterAsync(user);
        await using var cleanup = new Cleanup(() => client.Users.DeleteAsync(response.Value?.Id));

        AssertExtended.IsReady(client);
        
        var userEdit = new UserFormModel
        {
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

        var user = new UserFormModel
        {
            Email = "register@register.register",
            Password = "P@ssw0rd",
        };
        
        var response = await client.RegisterAsync(user);
        await using var cleanup = new Cleanup(() => client.Users.DeleteAsync(response.Value?.Id));
    
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

        await client.LoginAsync(Settings.TestUser.Email, Settings.TestUserForm.Password!);
        
        AssertExtended.IsReady(client);

        await client.LogoutAsync();
        
        Assert.AreEqual(ClientStatus.NotAuthorized, client.Status);
    }
}