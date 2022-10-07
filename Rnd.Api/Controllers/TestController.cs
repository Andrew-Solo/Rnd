// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Rnd.Api.Data;
// using Rnd.Api.Modules.Basic.Effects.Parameter;
// using Rnd.Api.Modules.Basic.Fields;
// using Rnd.Api.Modules.Basic.Parameters;
// using Rnd.Api.Modules.Basic.Users;
// using Rnd.Api.Modules.RndCore.Effects;
// using Character = Rnd.Api.Modules.Basic.Characters.Character;
// using Effect = Rnd.Api.Modules.Basic.Effects.Effect;
// using Game = Rnd.Api.Modules.Basic.Games.Game;
// using Member = Rnd.Api.Modules.Basic.Members.Member;
// using Resource = Rnd.Api.Modules.Basic.Resources.Resource;
// using ResourceEffect = Rnd.Api.Modules.Basic.Effects.Resource.ResourceEffect;
// using User = Rnd.Api.Modules.Basic.Users.User;
//
// namespace Rnd.Api.Controllers;
//
// [ApiController]
// [Route("[controller]/[action]")]
// public class TestController : ControllerBase
// {
//     public TestController(DataContext db)
//     {
//         //DIs
//         Db = db;
//     }
//     
//     //DIs
//     public DataContext Db { get; }
//     
//     [HttpGet]
//     public async Task<IActionResult> TestDb()
//     {
//         var user = new User("login", "email", "hash");
//         var game = new Game(user.Id, "Game 1");
//         var member = new Member(game, user);
//         var character = new Character(member, "character");
//         var field1 = new ShortField(character, "name1", value: "value1", path: "path1");
//         var field2 = new ShortField(character, "name2", value: "value2", path: "path2");
//         var parameter1 = new Int32Parameter( character,"name1") {Value = 1};
//         var parameter2 = new Int32Parameter(character,"name2") {Value = 2};
//         var resource1 = new Resource(character,"name1") {Value = 1, Min = 1, Max = 1};
//         var resource2 = new Resource(character,"name2") {Value = 2, Min = 2, Max = 2};
//         var effect1 = new Effect(character, "name1");
//         var effect2 = new Effect(character, "name2");
//         var resourceEffect1 = new ResourceEffect(effect1, "name1") {ResourceName = "name1", ValueModifier = 1, MinModifier = 1, MaxModifier = 1};
//         var resourceEffect2 = new ResourceEffect(effect2,"name2") {ResourceName = "name2", ValueModifier = 2, MinModifier = 2, MaxModifier = 2};
//         var parameterEffect1 = new Int32ParameterEffect(effect1, "name1", 1);
//         var parameterEffect2 = new Int32ParameterEffect(effect2, "name2", 2);
//
//         var userEntity = user.AsStorable.SaveNotNull();
//
//         Db.Users.Add(userEntity);
//         await Db.SaveChangesAsync();
//
//         var newUser = await Db.Users.FirstAsync();
//
//         var loadedUser = UserFactory.Create(newUser);
//
//         var c = loadedUser.Members.First().Characters.First();
//         
//         c.Parameters.First().Value = 123;
//          
//         c.Fields.Remove(c.Fields.First());
//
//         var newParameter = new Int32Parameter(c, "NewParameter");
//
//         loadedUser.Save(newUser, Db.SetAddedState);
//         
//         await Db.SaveChangesAsync();
//
//         return Ok();
//     }
//
//     [HttpGet]
//     public async Task<IActionResult> TestRnd()
//     {
//         var user = new User("login", "email", "hash");
//         var game = new Game(user.Id, "Game 1");
//         var member = new Member(game, user);
//         var character = new Modules.RndCore.Characters.Character(member, "character");
//         var effect1 = new Custom(character, "effect1");
//         var effect2 = new Custom(character, "effect2");
//         var resourceEffect1 = new ResourceEffect(effect1, "name1") {ResourceName = "name1", ValueModifier = 1, MinModifier = 1, MaxModifier = 1};
//         var resourceEffect2 = new ResourceEffect(effect2,"name2") {ResourceName = "name2", ValueModifier = 2, MinModifier = 2, MaxModifier = 2};
//         var parameterEffect1 = new Int32ParameterEffect(effect1, "name1", 1);
//         var parameterEffect2 = new Int32ParameterEffect(effect2, "name2", 2);
//         
//         character.CreateItems();
//         character.CreateItems();
//         character.Final.CreateItems();
//         character.Final.DeleteItems();
//         character.Final.CreateItems();
//         
//         var userEntity = user.AsStorable.SaveNotNull();
//
//         Db.Users.Add(userEntity);
//         await Db.SaveChangesAsync();
//
//         var newUser = await Db.Users.FirstAsync();
//
//         var loadedUser = UserFactory.Create(newUser);
//
//         var newCharacter = (Modules.RndCore.Characters.Character) loadedUser.Members.First().Characters.First();
//
//         newCharacter.Attributes.Strength.Value++;
//
//         newCharacter.Effects.Remove(newCharacter.CustomEffects.First());
//         
//         var newEffect = new Custom(newCharacter, "effect3");
//         
//         loadedUser.Save(newUser, Db.SetAddedState);
//         
//         await Db.SaveChangesAsync();
//
//         return Ok();
//     }
// }