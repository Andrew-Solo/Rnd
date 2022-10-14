using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rnd.Api.Client;
using Rnd.Api.Client.Clients;
using Rnd.Api.Client.Controllers;
using Rnd.Api.Client.Models.Basic.Game;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Client.Responses;
using Rnd.Api.Helpers;

namespace Rnd.Api.Tests.Controllers.Basic;

[TestClass]
public class GamesControllerTests
{
    public BasicClient Client => Settings.TestClient;
    public List<GameModel> Games => new();

    [ClassInitialize]
    public static async Task ClassInit(TestContext context)
    {
        var models = new GameFormModel[]
        {
            new() {  }
        };
    }
    
    [ClassCleanup]
    public static async Task ClassCleanup()
    {
        
    }
    
    [TestMethod]
    public async Task GetTest()
    {
        var game = await Client.Games.GetOrExceptionAsync();
        
        
    }
}