using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Controllers.Validation;
using Rnd.Api.Controllers.Validation.UserModel;
using Rnd.Api.Data;
using Rnd.Api.Helpers;

namespace Rnd.Api.Controllers.Basic;

[ApiController]
[Route("basic/[controller]")]
public class UsersController : ControllerBase
{
    public UsersController(DataContext db, IMapper mapper)
    {
        //DIs
        Db = db;
        Mapper = mapper;
    }

    //DIs
    public DataContext Db { get; set; }
    public IMapper Mapper { get; }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserModel>> Get(Guid id)
    {
        var userEntity = await Db.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (userEntity == null) return this.NotFound<Data.Entities.User>();

        return Ok(Mapper.Map<UserModel>(userEntity));
    }
    
    [HttpGet]
    public async Task<ActionResult<UserModel>> Login(string login, string password)
    {
        var passwordHash = Hash.GenerateStringHash(password);
        var userEntity = await Db.Users.FirstOrDefaultAsync(u => u.PasswordHash == passwordHash && (u.Login == login || u.Email == login));

        if (userEntity == null) return this.NotFound<Data.Entities.User>();

        return Ok(Mapper.Map<UserModel>(userEntity));
    }
    
    [HttpGet("[action]/{id:guid}")]
    public async Task<ActionResult> Exist(Guid id)
    {
        var exist = await Db.Users.AnyAsync(u => u.Id == id);

        if (!exist) return this.NotFound<Data.Entities.User>();

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> ValidateForm([FromQuery] UserFormModel form, bool insert = false)
    {
        if (insert)
        {
            await ModelState.ValidateForm<UserInsertModelValidator, UserFormModel>(form);
        }
        else
        {
            await ModelState.ValidateForm<UserFormModelValidator, UserFormModel>(form);
        }
        
        if (!ModelState.IsValid) return BadRequest(ModelState.ToErrors());

        if (!insert) return Ok();
        
        await ModelState.CheckOverlap(Db.Users, g => g.Email == form.Email);
        await ModelState.CheckOverlap(Db.Users, g => g.Login == form.Login);
        
        if (!ModelState.IsValid) return Conflict(ModelState.ToErrors());

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<UserModel>> Register(UserFormModel form)
    {
        var validation = await ValidateForm(form, true);

        if (!ModelState.IsValid) return validation;
        
        //TODO
        // var user = new User(form);

        //var userEntity = user.AsStorable.SaveNotNull();
        
        // await Db.Users.AddAsync(userEntity);
        
        // await Db.SaveChangesAsync();
        
        // return Ok(Mapper.Map<UserModel>(userEntity));

        return Ok();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserModel>> Edit(Guid id, UserFormModel form)
    {
        var validation = await ValidateForm(form);

        if (!ModelState.IsValid) return validation;
        
        var userEntity = Db.Users.FirstOrDefault(u => u.Id == id);

        if (userEntity == null) return this.NotFound<Data.Entities.User>();
        
        Mapper.Map(form, userEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<UserModel>(userEntity));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<UserModel>> Delete(Guid id)
    {
        var userEntity = Db.Users.FirstOrDefault(u => u.Id == id);

        if (userEntity == null) return this.NotFound<Data.Entities.User>();
        
        Db.Users.Remove(userEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<UserModel>(userEntity));
    }
}