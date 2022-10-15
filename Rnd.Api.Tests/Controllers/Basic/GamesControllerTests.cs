using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rnd.Api.Client.Clients;
using Rnd.Api.Client.Models.Basic.Game;

namespace Rnd.Api.Tests.Controllers.Basic;

[TestClass]
public class GamesControllerTests
{ 
    static GamesControllerTests() {}
    public static BasicClient Client => Settings.TestClient;
    public static List<GameModel> Games { get; } = new();

    [ClassInitialize]
    public static async Task ClassInit(TestContext context)
    {
        var forms = new GameFormModel[]
        {
            new() { Name = "Name1", Description = "Some description", Title = "Title" },
            new() { Name = "Name2", Title = "Title" },
            new() { Name = "Name3", Description = "Some description" },
            new() { Name = "Name4" },
        };

        foreach (var form in forms)
        {
            var game = await Client.Games.AddOrExceptionAsync(form);
            Games.Add(game);
        }
    }
    
    [ClassCleanup]
    public static async Task ClassCleanup()
    {
        foreach (var game in Games)
        {
            await Client.Games.DeleteAsync(game.Id);
        }
    }
    
    [TestMethod]
    public async Task GetTest()
    {
        var expected = Games.First();

        var actual = await Client.Games.GetOrExceptionAsync(expected.Id);
        var actual1 = Client.Games[expected.Id].Value;
        
        AssertExtended.AllPropertiesAreEqual(expected, actual);
        AssertExtended.AllPropertiesAreEqual(expected, actual1);
    }
    
    [TestMethod]
    public async Task ListTest()
    {
        var expectedList = Games;
        
        var actualList = await Client.Games.ListOrExceptionAsync();

        AssertExtended.CollectionAndElements(expectedList.OrderBy(g => g.Created), 
            actualList.OrderBy(g => g.Created));
    }
    
    [TestMethod]
    public async Task ExistTest()
    {
        var existItem = Games.First();

        var actual = await Client.Games.ExistAsync(existItem.Id);
        var actual1 = Client.Games[existItem.Id].Exist;
        
        AssertExtended.AllPropertiesAreEqual(true, actual);
        AssertExtended.AllPropertiesAreEqual(true, actual1);

        var newGuid = Guid.NewGuid();
        var negateActual = await Client.Games.ExistAsync(newGuid);
        var negateActual1 = Client.Games[newGuid].Exist;
        
        AssertExtended.AllPropertiesAreEqual(false, negateActual);
        AssertExtended.AllPropertiesAreEqual(false, negateActual1);
    }
    
    [Ignore]
    [TestMethod]
    public async Task ValidateFormTest()
    {
        
    }
    
    [Ignore]
    [TestMethod]
    public async Task AddTest()
    {
        
    }
    
    [TestMethod]
    public async Task EditTest()
    {
        var forms = new GameFormModel[]
        {
            new() {Name = "newName"},
            new() {Description = "newDescription"},
            new() {Title = "newTitle"},
            new() {Name = "newName1", Description = "newDescription1", Title = "newTitle1"},
        };

        for (var i = 0; i < forms.Length; i++)
        {
            var form = forms[i];
            var expected = Games[i].Clone();

            if (form.Name != null) expected.Name = form.Name;
            if (form.Description != null) expected.Description = form.Description;
            if (form.Title != null) expected.Title = form.Title;

            var actual = await Client.Games.EditOrExceptionAsync(form, expected.Id);
            
            AssertExtended.AllPropertiesAreEqual(expected, actual);
        }
        
        for (var i = 0; i < forms.Length; i++)
        {
            var expected = Games[i];
            
            var form = new GameFormModel
            {
                Title = expected.Title,
                Name = expected.Name,
                Description = expected.Description,
            };

            var actual = await Client.Games[expected.Id].Edit(form);
            
            //TODO способ передачи null в форме?
            //AssertExtended.AllPropertiesAreEqual(expected, actual);
        }
    }
    
    [TestMethod]
    public async Task DeleteTest()
    {
        var form = new GameFormModel {Name = "DeleteName"};
        var form1 = new GameFormModel {Name = "DeleteName1"};

        var created = await Client.Games.AddOrExceptionAsync(form);
        var created1 = await Client.Games.AddOrExceptionAsync(form1);

        await Client.Games.DeleteAsync(created.Id);
        await Client.Games[created1.Id].Delete();
        
        Assert.IsFalse(Client.Games[created.Id].Exist);
        Assert.IsFalse(Client.Games[created1.Id].Exist);
    }
}