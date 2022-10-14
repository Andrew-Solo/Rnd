using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rnd.Api.Helpers;

namespace Rnd.Api.Tests.Controllers.Basic;

[TestClass]
public class UsersControllerTests
{
    [TestMethod]
    public async Task GetTest()
    {
        var client = Settings.TestClient;

        var user = client.User;
        
        Assert.AreEqual(Settings.DefaultUser.Email, user.Email);
        Assert.AreEqual(Settings.DefaultUser.Login, user.Login);
        Assert.AreEqual(Hash.GenerateStringHash(Settings.DefaultUser.Password), user.PasswordHash);
    }
    
    [TestMethod]
    public async Task LoginTest()
    {
        
    }
    
    [TestMethod]
    public async Task RegisterTest()
    {
        
    }
    
    [TestMethod]
    public async Task EditTest()
    {
        
    }
    
    [TestMethod]
    public async Task DeleteTest()
    {
        
    }
}