using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.Member;
using Rnd.Api.Controllers.Validation;
using Rnd.Api.Controllers.Validation.MemberModel;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Controllers.Basic;

[ApiController]
[Route("basic/users/{userId:guid}/games/{gameId:guid}/[controller]")]
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
        var memberEntity = await Db.Members.FirstOrDefaultAsync(m => m.Id == id && m.GameId == gameId);

        if (memberEntity == null) return this.NotFound<Member>();

        if (memberEntity.UserId != userId && memberEntity.Game.OwnerId != userId)
        {
            return this.Forbidden<Member>();
        }

        return Ok(Mapper.Map<MemberModel>(memberEntity));
    }
    
    [HttpGet]
    public async Task<ActionResult<List<MemberModel>>> List(Guid userId, Guid gameId)
    {
        var members = await Db.Members
            .Where(g => g.GameId == gameId && g.Game.OwnerId == userId)
            .ToListAsync();

        if (members.Count == 0) return NoContent();

        return Ok(members);
    }
    
    [HttpGet("[action]/{id:guid}")]
    public async Task<ActionResult> Exist(Guid userId, Guid gameId, Guid id)
    {
        var exist = await Db.Members.AnyAsync(g => g.Id == id);

        if (!exist) return this.NotFound<Member>();

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> ValidateForm(Guid userId, Guid gameId, [FromQuery] MemberFormModel form, bool insert = false)
    {
        if (insert)
        {
            await ModelState.ValidateForm<MemberInsertModelValidator, MemberFormModel>(form);
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
    public async Task<ActionResult<MemberModel>> Invite(Guid userId, Guid gameId, MemberFormModel form)
    {
        var validation = await ValidateForm(userId,  gameId, form, true);

        if (!ModelState.IsValid) return validation;

        //TODO
        // var game = await Db.Games.GetModel(gameId, new GameFactory()) as Game;
        // if (game == null) return this.NotFound<Data.Entities.Game>();
        // if (game.OwnerId != userId) return this.Forbidden<Data.Entities.Game>();
        //
        // var user = await Db.Users.GetModel(form.UserId.GetValueOrDefault(userId), new UserFactory()) as User;
        // if (user == null) return this.NotFound<Data.Entities.User>();
        //
        // var member = new Modules.Basic.Members.Member(game, user);
        //
        // Mapper.Map(form, member);
        //
        // var memberEntity = member.Save(new Data.Entities.Member())!;
        //
        // await Db.Members.AddAsync(memberEntity);
        //
        // await Db.SaveChangesAsync();
        //
        // return Ok(Mapper.Map<MemberModel>(memberEntity));

        return Ok();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MemberModel>> Edit(Guid userId, Guid gameId, Guid id, MemberFormModel form)
    {
        var validation = await ValidateForm(userId, gameId, form);

        if (!ModelState.IsValid) return validation;
        
        var memberEntity = Db.Members.FirstOrDefault(m => m.Id == id && m.GameId == gameId);

        if (memberEntity == null) return this.NotFound<Member>();
        
        if (memberEntity.Game.OwnerId != userId) return this.Forbidden<Member>();
        
        Mapper.Map(form, memberEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<MemberModel>(memberEntity));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<MemberModel>> Kick(Guid userId, Guid gameId, Guid id)
    {
        var memberEntity = Db.Members.FirstOrDefault(m => m.Id == id && m.GameId == gameId);

        if (memberEntity == null) return this.NotFound<Member>();
        if (memberEntity.Game.OwnerId != userId) return this.Forbidden<Member>();
        
        Db.Members.Remove(memberEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<MemberModel>(memberEntity));
    }
}