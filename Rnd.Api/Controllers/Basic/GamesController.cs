using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.Game;
using Rnd.Api.Controllers.Validation;
using Rnd.Api.Controllers.Validation.GameModel;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Controllers.Basic;

[ApiController]
[Route("basic/users/{userId:guid}/[controller]")]
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

        if (game.OwnerId != userId && game.Members.All(m => m.UserId != userId))
        {
            return this.Forbidden<Game>();
        }

        return Ok(Mapper.Map<GameModel>(game));
    }
    
    [HttpGet]
    //TODO выводить игры где юзер мембер тоже
    public async Task<ActionResult<List<GameModel>>> List(Guid userId)
    {
        var games = await Db.Games
            .Where(g => g.OwnerId == userId)
            .ToListAsync();

        if (games.Count == 0) return NoContent();

        return Ok(games);
    }
    
    [HttpGet("[action]/{id:guid}")]
    //TODO выводить игры только доступные пользователю
    public async Task<ActionResult> Exist(Guid userId, Guid id)
    {
        var exist = await Db.Games.AnyAsync(g => g.Id == id);

        if (!exist) return this.NotFound<Game>();

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> ValidateForm(Guid userId, [FromQuery] GameFormModel form, bool insert = false)
    {
        if (insert)
        {
            await ModelState.ValidateForm<GameInsertModelValidator, GameFormModel>(form);
        }
        else
        {
            await ModelState.ValidateForm<GameFormModelValidator, GameFormModel>(form);
        }
        
        if (!ModelState.IsValid) return BadRequest(ModelState.ToErrors());

        if (!insert) return Ok();
        
        await ModelState.CheckNotExist(Db.Games, g => g.Name == form.Name);
        
        if (!ModelState.IsValid) return Conflict(ModelState.ToErrors());

        return Ok();
    }
    
    [HttpPost]
    public async Task<ActionResult<GameModel>> Create(Guid userId, GameFormModel form)
    {
        var validation = await ValidateForm(userId, form, true);

        if (!ModelState.IsValid) return validation;
        
        var game = Game.Create(userId, form);
        
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
        if (game.OwnerId != userId) return this.Forbidden<Game>();
        
        Mapper.Map(form, game);
        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<GameModel>(game));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<GameModel>> Delete(Guid userId, Guid id)
    {
        var game = Db.Games.FirstOrDefault(u => u.Id == id);

        if (game == null) return this.NotFound<Game>();
        if (game.OwnerId != userId) return this.Forbidden<Game>();
        
        Db.Games.Remove(game);
        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<GameModel>(game));
    }
}