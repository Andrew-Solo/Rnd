using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        var member = await Db.Members.FirstOrDefaultAsync(m => m.Id == id);
        if (member == null) return this.NotFound<Member>();

        if (member.UserId == userId) return Ok(Mapper.Map<MemberModel>(member));
        
        var superior = await Db.Members.FirstOrDefaultAsync(m => m.GameId == gameId && m.UserId == userId );
        if (superior == null || superior.Role == MemberRole.Player) return this.Forbidden<Member>();

        return Ok(Mapper.Map<MemberModel>(member));
    }
    
    [HttpGet]
    public async Task<ActionResult<List<MemberModel>>> List(Guid userId, Guid gameId)
    {
        var members = await Db.Members
            .Where(m => m.GameId == gameId && m.Game.Members.Any(im => im.UserId == userId))
            .ToListAsync();

        if (members.Count == 0) return NoContent();

        return Ok(members.Select(m => Mapper.Map<MemberModel>(m)));
    }
    
    [HttpGet("[action]/{id:guid}")]
    public async Task<ActionResult> Exist(Guid userId, Guid gameId, Guid id)
    {
        var member = await Db.Members.FirstOrDefaultAsync(g => g.Id == id);
        if (member == null) return this.NotFound<Member>();

        if (member.UserId == userId) return Ok(Mapper.Map<MemberModel>(member));
        
        var superior = await Db.Members.FirstOrDefaultAsync(m => m.GameId == gameId && m.UserId == userId );
        if (superior == null || superior.Role == MemberRole.Player) return this.Forbidden<Member>();

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
        
        var superior = await Db.Members.FirstOrDefaultAsync(m => m.GameId == gameId && m.UserId == userId );
        if (superior == null || superior.Role == MemberRole.Player) return this.Forbidden<Member>();

        if (form.Role != null && !ControlAllowed(superior.Role, JsonConvert.DeserializeObject<MemberRole>($"\"{form.Role}\"")))
        {
            return this.Forbidden<Member>();
        }
        
        var user = await Db.Users.FirstOrDefaultAsync(u => u.Id == form.UserId);
        if (user == null) return this.NotFound<User>();
        
        form.Nickname ??= user.Login;

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
        
        var superior = await Db.Members.FirstOrDefaultAsync(m => m.GameId == gameId && m.UserId == userId );
        if (superior == null || !ControlAllowed(superior.Role, member.Role)) return this.Forbidden<Member>();
        
        if (form.Role != null && !ControlAllowed(superior.Role, JsonConvert.DeserializeObject<MemberRole>($"\"{form.Role}\"")))
        {
            return this.Forbidden<Member>();
        }

        member.SetForm(form);
        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<MemberModel>(member));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<MemberModel>> Delete(Guid userId, Guid gameId, Guid id)
    {
        var member = Db.Members.FirstOrDefault(m => m.Id == id && m.GameId == gameId);
        if (member == null) return this.NotFound<Member>();
        
        var superior = await Db.Members.FirstOrDefaultAsync(m => m.GameId == gameId && m.UserId == userId );
        if (superior == null || !ControlAllowed(superior.Role, member.Role)) return this.Forbidden<Member>();

        Db.Members.Remove(member);
        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<MemberModel>(member));
    }

    private bool ControlAllowed(MemberRole executor, MemberRole target)
    {
        return (executor, target) switch
        {
            (MemberRole.Owner, _) => true,
            (MemberRole.Admin, MemberRole.Owner) => false,
            (MemberRole.Admin, _) => true,
            (MemberRole.Guide, MemberRole.Player) => true,
            (MemberRole.Guide, _) => false,
            (MemberRole.Player, _) => false,
            _ => throw new ArgumentOutOfRangeException()
        };
    } 
}