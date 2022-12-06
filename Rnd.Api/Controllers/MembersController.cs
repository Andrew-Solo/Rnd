using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.Member;
using Rnd.Api.Controllers.Validation;
using Rnd.Api.Controllers.Validation.MemberModel;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("users/{userId:guid}/games/{gameId:guid}/[controller]")]
//TODO распозновать gameId == empty как текущую игру
public class MembersController : ControllerBase
{
    public MembersController(DataContext db, IMapper mapper)
    {
        //DIs
        Db = db;
        Mapper = mapper;
    }

    //DIs
    public DataContext Db { get; set; }
    public IMapper Mapper { get; }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MemberModel>> Get(Guid userId, Guid gameId, Guid id)
    {
        var member = await Db.Members.FirstOrDefaultAsync(m => m.Id == id && m.GameId == gameId);

        if (member == null) return this.NotFound<Member>();

        if (member.UserId != userId && member.Game.FounderId != userId)
        {
            return this.Forbidden<Member>();
        }

        return Ok(Mapper.Map<MemberModel>(member));
    }
    
    [HttpGet]
    //TODO выводить игры для мемберов тоже
    public async Task<ActionResult<List<MemberModel>>> List(Guid userId, Guid gameId)
    {
        var members = await Db.Members
            .Where(g => g.GameId == gameId && g.Game.FounderId == userId)
            .ToListAsync();

        if (members.Count == 0) return NoContent();

        return Ok(members);
    }
    
    [HttpGet("[action]/{id:guid}")]
    //TODO выводить игры только доступные пользователю
    public async Task<ActionResult> Exist(Guid userId, Guid gameId, Guid id)
    {
        var exist = await Db.Members.AnyAsync(g => g.Id == id);

        if (!exist) return this.NotFound<Member>();

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> ValidateForm(Guid userId, Guid gameId, [FromQuery] MemberFormModel form, bool create = false)
    {
        if (create)
        {
            await ModelState.ValidateForm<MemberCreateModelValidator, MemberFormModel>(form);
        }
        else
        {
            await ModelState.ValidateForm<MemberFormModelValidator, MemberFormModel>(form);
        }
        
        if (!ModelState.IsValid) return BadRequest(ModelState.ToErrors());

        await ModelState.CheckNotExist(Db.Members, m => m.UserId == form.UserId && m.GameId == gameId);
        
        if (!ModelState.IsValid) return Conflict(ModelState.ToErrors());

        return Ok();
    }
    
    [HttpPost]
    public async Task<ActionResult<MemberModel>> Create(Guid userId, Guid gameId, MemberFormModel form)
    {
        var validation = await ValidateForm(userId,  gameId, form, true);

        if (!ModelState.IsValid) return validation;


        var game = await Db.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null) return this.NotFound<Game>();
        if (game.FounderId != userId) return this.Forbidden<Game>();
        
        var user = await Db.Users.FirstOrDefaultAsync(u => u.Id == form.UserId);
        if (user == null) return this.NotFound<User>();

        var member = Member.Create(gameId, form);
        
        await Db.Members.AddAsync(member);
        await Db.SaveChangesAsync();
        
        return Ok(Mapper.Map<MemberModel>(member));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MemberModel>> Edit(Guid userId, Guid gameId, Guid id, MemberFormModel form)
    {
        var validation = await ValidateForm(userId, gameId, form);

        if (!ModelState.IsValid) return validation;
        
        var member = Db.Members.FirstOrDefault(m => m.Id == id && m.GameId == gameId);
        if (member == null) return this.NotFound<Member>();
        if (member.Game.FounderId != userId) return this.Forbidden<Member>();
        
        Mapper.Map(form, member);
        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<MemberModel>(member));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<MemberModel>> Kick(Guid userId, Guid gameId, Guid id)
    {
        var member = Db.Members.FirstOrDefault(m => m.Id == id && m.GameId == gameId);
        if (member == null) return this.NotFound<Member>();
        if (member.Game.FounderId != userId) return this.Forbidden<Member>();
        
        Db.Members.Remove(member);
        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<MemberModel>(member));
    }
}