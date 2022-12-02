using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rnd.Api.Client.Clients;
using Rnd.Api.Client.Models.Basic.Game;
using Rnd.Api.Client.Models.Basic.Member;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Tests.Controllers.Basic;

[TestClass]
public class MembersControllerTests
{ 
    static MembersControllerTests() {}
    public static BasicClient Client => Settings.TestClient;

    public static GameModel Game { get; set; } = new();
    public static List<UserModel> Users { get; } = new();
    public static List<MemberModel> Members { get; } = new();

    [ClassInitialize]
    public static async Task ClassInit(TestContext context)
    {
        var gameForm = new GameFormModel {Name = "Name1", Description = "Some description", Title = "Title"};

        Game = await Client.Games.AddOrExceptionAsync(gameForm);
        
        var userForms = new UserFormModel[]
        {
            new() { Email = "test1@test.test", Password = "P@ssw0rd" },
            new() { Email = "test2@test.test", Password = "P@ssw0rd" },
            new() { Email = "test3@test.test", Password = "P@ssw0rd" },
        };

        foreach (var form in userForms)
        {
            var user = await Client.Users.AddOrExceptionAsync(form);
            Users.Add(user);
        }
        
        var forms = new MemberFormModel[]
        {
            new() { Nickname = "SuperOwner", Role = MemberRole.Owner.ToString(), ColorHex = "#ff0000", UserId = Guid.Empty },
            new() { Role = MemberRole.Admin.ToString(), ColorHex = "#000000", UserId = Users[0].Id },
            new() { Nickname = "Nickname1", Role = MemberRole.Guide.ToString(), ColorHex = "#ffffff", UserId = Users[1].Id },
            new() { Role = MemberRole.Player.ToString(), UserId = Users[2].Id },
        };

        foreach (var form in forms)
        {
            var member = await Client.Games[Game.Id].Members.AddOrExceptionAsync(form);
            Members.Add(member);
        }
    }
    
    [ClassCleanup]
    public static async Task ClassCleanup()
    {
        foreach (var member in Members)
        {
            await Client.Games[Game.Id].Members.DeleteAsync(member.Id);
        }
        
        foreach (var user in Users)
        {
            await Client.Users.DeleteAsync(user.Id);
        }

        await Client.Games.DeleteAsync(Game.Id);
    }
    
    [TestMethod]
    public async Task GetTest()
    {
        var expected = Members.First();

        var actual = await Client.Games[Game.Id].Members.GetOrExceptionAsync(expected.Id);
        var actual1 = Client.Games[Game.Id].Members[expected.Id].Value;
        
        AssertExtended.AllPropertiesAreEqual(expected, actual);
        AssertExtended.AllPropertiesAreEqual(expected, actual1);
    }
    
    [TestMethod]
    public async Task ListTest()
    {
        var expectedList = Members;
        
        var actualList = await Client.Games[Game.Id].Members.ListOrExceptionAsync();

        AssertExtended.CollectionAndElements(expectedList.OrderBy(g => g.Id), 
            actualList.OrderBy(g => g.Id));
    }
    
    [TestMethod]
    public async Task ExistTest()
    {
        var existItem = Members.First();

        var actual = await Client.Games[Game.Id].Members.ExistAsync(existItem.Id);
        var actual1 = Client.Games[Game.Id].Members[existItem.Id].Exist;
        
        AssertExtended.AllPropertiesAreEqual(true, actual);
        AssertExtended.AllPropertiesAreEqual(true, actual1);

        var newGuid = Guid.NewGuid();
        var negateActual = await Client.Games[Game.Id].Members.ExistAsync(newGuid);
        var negateActual1 = Client.Games[Game.Id].Members[newGuid].Exist;
        
        AssertExtended.AllPropertiesAreEqual(false, negateActual);
        AssertExtended.AllPropertiesAreEqual(false, negateActual1);
    }
    
    [Ignore]
    [TestMethod]
    public Task ValidateFormTest()
    {
        throw new NotImplementedException();
    }
    
    [Ignore]
    [TestMethod]
    public Task AddTest()
    {
        throw new NotImplementedException();
    }
    
    [TestMethod]
    public async Task EditTest()
    {
        var forms = new MemberFormModel[]
        {
            new() {Nickname = "newNickname"},
            new() {Role = MemberRole.Player.ToString()},
            new() {ColorHex = "#f0f0f0"},
            new() {Nickname = "newNickname1", Role = MemberRole.Admin.ToString(), ColorHex = "#0f0f0f"},
        };

        for (var i = 0; i < forms.Length; i++)
        {
            var form = forms[i];
            var expected = Members[i].Clone();

            if (form.Nickname != null) expected.Nickname = form.Nickname;
            if (form.Role != null) expected.Role = form.Role;
            if (form.ColorHex != null) expected.ColorHex = form.ColorHex;

            var actual = await Client.Games[Game.Id].Members.EditOrExceptionAsync(form, expected.Id);
            
            AssertExtended.AllPropertiesAreEqual(expected, actual);
        }
        
        for (var i = 0; i < forms.Length; i++)
        {
            var expected = Members[i];
            
            var form = new MemberFormModel
            {
                Nickname = expected.Nickname,
                Role = expected.Role,
                ColorHex = expected.ColorHex,
            };

            var actual = await Client.Games[Game.Id].Members[expected.Id].Edit(form);
            
            //TODO способ передачи null в форме?
            //AssertExtended.AllPropertiesAreEqual(expected, actual);
        }
    }
    
    [TestMethod]
    public async Task DeleteTest()
    {
        var form = Members[0].ToForm();
        var form1 = Members[1].ToForm();

        await using var cleanup = new Cleanup(() => Client.Games[Game.Id].Members.AddOrExceptionAsync(form));
        await using var cleanup1 = new Cleanup(() => Client.Games[Game.Id].Members.AddOrExceptionAsync(form1));

        await Client.Games[Game.Id].Members.DeleteAsync(Members[0].Id);
        await Client.Games[Game.Id].Members[Members[1].Id].Delete();
        
        Assert.IsFalse(Client.Games[Game.Id].Members[Members[0].Id].Exist);
        Assert.IsFalse(Client.Games[Game.Id].Members[Members[1].Id].Exist);
    }
}