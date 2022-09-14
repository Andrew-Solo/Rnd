using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Effects;
using Rnd.Api.Modules.Basic.Effects.Parameter;
using Rnd.Api.Modules.Basic.Effects.Resource;
using Rnd.Api.Modules.Basic.Fields;
using Rnd.Api.Modules.Basic.Games;
using Rnd.Api.Modules.Basic.Members;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.Basic.Resources;
using Rnd.Api.Modules.Basic.Users;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase
{
    public TestController(DataContext db)
    {
        //DIs
        Db = db;
    }
    
    //DIs
    public DataContext Db { get; }
    
    [HttpGet]
    public async Task<IActionResult> TestDb()
    {
        var user = new User("login", "email", "hash");
        var game = new Game(user.Id, "Game 1");
        var member = new Member(game, user);
        var character = new Character(member, "character");
        var field1 = new ShortField(character, "name1", value: "value1", path: "path1");
        var field2 = new ShortField(character, "name2", value: "value2", path: "path2");
        var parameter1 = new Int32Parameter( character,"name1") {Value = 1};
        var parameter2 = new Int32Parameter(character,"name2") {Value = 2};
        var resource1 = new Resource(character,"name1") {Value = 1, Min = 1, Max = 1};
        var resource2 = new Resource(character,"name2") {Value = 2, Min = 2, Max = 2};
        var effect1 = new Effect(character, "name1");
        var effect2 = new Effect(character, "name2");
        var resourceEffect1 = new ResourceEffect(effect1, "name1") {ResourceName = "name1", ValueModifier = 1, MinModifier = 1, MaxModifier = 1};
        var resourceEffect2 = new ResourceEffect(effect2,"name2") {ResourceName = "name2", ValueModifier = 2, MinModifier = 2, MaxModifier = 2};
        var parameterEffect1 = new Int32ParameterEffect(effect1, "name1", 1);
        var parameterEffect2 = new Int32ParameterEffect(effect2, "name2", 2);

        var userEntity = user.AsStorable.SaveNotNull(null);

        Db.Users.Add(userEntity);
        await Db.SaveChangesAsync();

        return Ok();
    }
}