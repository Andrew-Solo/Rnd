using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.Game;
using Rnd.Api.Controllers.Validation;
using Rnd.Api.Controllers.Validation.GameModel;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("users/{userId:guid}/[controller]")]
//TODO возвращать эксепшены на интернал сервер ерор
public class GamesController : ControllerBase
{
    public GamesController(DataContext db, IMapper mapper)
    {
        //DIs
        Db = db;
        Mapper = mapper;
    }

    //DIs
    public DataContext Db { get; set; }
    public IMapper Mapper { get; }
    
    //TODO переделать авторизацию на аттрибуты
    //TODO сделать модель овнера и мембера разными 
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GameModel>> Get(Guid userId, Guid id)
    {
        var game = await Db.Games.FirstOrDefaultAsync(g => g.Id == id);
        if (game == null) return this.NotFound<Game>();

        var member = game.Members.FirstOrDefault(m => m.UserId == userId);
        if (member == null) return this.Forbidden<Game>();

        return Ok(Mapper.Map<GameModel>(game));
    }
    
    [HttpGet]
    //TODO Сделать лукапы на Id пользователей и тд
    public async Task<ActionResult<List<GameModel>>> List(Guid userId)
    {
        var games = await Db.Games
            .Where(g => g.Members.Any(u => u.UserId == userId))
            .ToListAsync();

        if (games.Count == 0) return NoContent();

        return Ok(games.Select(g => Mapper.Map<GameModel>(g)));
    }
    
    
    [HttpGet("[action]/{id:guid}")]
    public async Task<ActionResult> Exist(Guid userId, Guid id)
    {
        var game = await Db.Games.FirstOrDefaultAsync(g => g.Id == id);
        if (game == null) return this.NotFound<Game>();

        var member = game.Members.FirstOrDefault(m => m.UserId == userId);
        if (member == null) return this.Forbidden<Game>();

        return Ok();
    }

    [HttpGet("[action]")]
    //TODO тут будет проблемы с редактированием и присваиванием сущесвтующего значения Name
    public async Task<ActionResult> ValidateForm(Guid userId, [FromQuery] GameFormModel form, bool create = false)
    {
        if (create)
        {
            await ModelState.ValidateForm<GameCreateModelValidator, GameFormModel>(form);
        }
        else
        {
            await ModelState.ValidateForm<GameFormModelValidator, GameFormModel>(form);
        }
        
        if (!ModelState.IsValid) return BadRequest(ModelState.ToErrors());
        
        if (form.Name != null) await ModelState.CheckNotExist(Db.Games, g => g.Name == form.Name);
        
        if (!ModelState.IsValid) return Conflict(ModelState.ToErrors());

        return Ok();
    }
    
    [HttpPost]
    public async Task<ActionResult<GameModel>> Create(Guid userId, GameFormModel form)
    {
        var validation = await ValidateForm(userId, form, true);

        if (!ModelState.IsValid) return validation;

        var user = await Db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return this.NotFound<User>();
        
        var game = Game.Create(user, form);
        
        await Db.Games.AddAsync(game);
        await Db.SaveChangesAsync();
        
        return Ok(Mapper.Map<GameModel>(game));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GameModel>> Edit(Guid userId, Guid id, GameFormModel form)
    {
        var validation = await ValidateForm(userId, form);

        if (!ModelState.IsValid) return validation;
        
        var game = Db.Games.FirstOrDefault(u => u.Id == id);
        if (game == null) return this.NotFound<Game>();
        
        var member = game.Members.FirstOrDefault(m => m.UserId == userId);
        if (member == null || member.Role is not MemberRole.Owner or MemberRole.Admin) return this.Forbidden<Game>();

        game.SetForm(form);
        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<GameModel>(game));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<GameModel>> Delete(Guid userId, Guid id)
    {
        var game = Db.Games.FirstOrDefault(u => u.Id == id);
        if (game == null) return this.NotFound<Game>();
        
        var member = game.Members.FirstOrDefault(m => m.UserId == userId);
        if (member == null || member.Role is not MemberRole.Owner) return this.Forbidden<Game>();
        
        Db.Games.Remove(game);
        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<GameModel>(game));
    }
}