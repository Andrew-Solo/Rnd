using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.Game;
using Rnd.Api.Controllers.Validation;
using Rnd.Api.Controllers.Validation.GameModel;
using Rnd.Api.Data;
using Rnd.Api.Modules.Basic.Games;

namespace Rnd.Api.Controllers.Basic.Users;

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
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GameModel>> Get(Guid userId, Guid id)
    {
        var game = await Db.Games.FirstOrDefaultAsync(g => g.Id == id);

        if (game == null) return this.NotFound<Data.Entities.Game>();

        if (game.OwnerId != userId && game.Members.All(m => m.UserId != userId))
        {
            return this.Forbidden<Data.Entities.Game>();
        }

        return Ok(Mapper.Map<GameModel>(game));
    }
    
    [HttpGet]
    public async Task<ActionResult<List<GameModel>>> List(Guid userId)
    {
        var games = await Db.Games
            .Where(g => g.OwnerId == userId)
            .ToListAsync();

        if (games.Count == 0) return NoContent();

        return Ok(games);
    }
    
    [HttpGet("[action]/{id:guid}")]
    public async Task<ActionResult> Exist(Guid userId, Guid id)
    {
        var exist = await Db.Games.AnyAsync(g => g.Id == id);

        if (!exist) return this.NotFound<Data.Entities.Game>();

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> ValidateForm(Guid userId, [FromQuery] GameFormModel form, bool insert = false)
    {
        if (insert)
        {
            await ValidationHelper.ValidateAsync<GameInsertModelValidator, GameFormModel>(form, ModelState);
        }
        else
        {
            await ValidationHelper.ValidateAsync<GameFormModelValidator, GameFormModel>(form, ModelState);
        }
        
        if (!ModelState.IsValid) return BadRequest(ModelState.ToErrors());

        if (!insert) return Ok();
        
        var overlapName = await Db.Games.FirstOrDefaultAsync(u => u.Name == form.Name);
        if (overlapName != null) ModelState.AddModelError(nameof(GameFormModel.Name), "Name already exist");
        
        if (!ModelState.IsValid) return Conflict(ModelState.ToErrors());

        return Ok();
    }
    
    [HttpPost]
    public async Task<ActionResult<GameModel>> Create(Guid userId, GameFormModel form)
    {
        var validation = await ValidateForm(userId, form, true);

        if (!ModelState.IsValid) return validation;
        
        var game = new Game(userId, form);

        //Owner member?
        var gameEntity = game.AsStorable.SaveNotNull();
        
        await Db.Games.AddAsync(gameEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<GameModel>(gameEntity));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GameModel>> Edit(Guid userId, Guid id, GameFormModel form)
    {
        var validation = await ValidateForm(userId, form);

        if (!ModelState.IsValid) return validation;
        
        var gameEntity = Db.Games.FirstOrDefault(u => u.Id == id);

        if (gameEntity == null) return this.NotFound<Data.Entities.Game>();
        
        if (gameEntity.OwnerId != userId) return this.Forbidden<Data.Entities.Game>();
        
        Mapper.Map(form, gameEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<GameModel>(gameEntity));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<GameModel>> Delete(Guid userId, Guid id)
    {
        var gameEntity = Db.Games.FirstOrDefault(u => u.Id == id);

        if (gameEntity == null) return this.NotFound<Data.Entities.Game>();
        
        if (gameEntity.OwnerId != userId) return this.Forbidden<Data.Entities.Game>();
        
        Db.Games.Remove(gameEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<GameModel>(gameEntity));
    }
}