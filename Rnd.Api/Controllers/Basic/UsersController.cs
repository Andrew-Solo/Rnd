using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Controllers.Validation;
using Rnd.Api.Controllers.Validation.UserModel;
using Rnd.Api.Data;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Users;

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
        var user = await Db.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return this.NotFound<Data.Entities.User>();

        return Ok(Mapper.Map<UserModel>(user));
    }
    
    [HttpGet]
    public async Task<ActionResult<UserModel>> Login(string login, string password)
    {
        var passwordHash = Hash.GenerateStringHash(password);
        var user = await Db.Users.FirstOrDefaultAsync(u => u.PasswordHash == passwordHash && (u.Login == login || u.Email == login));

        if (user == null) return this.NotFound<Data.Entities.User>();

        return Ok(Mapper.Map<UserModel>(user));
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
            await ValidationHelper.ValidateAsync<UserInsertModelValidator, UserFormModel>(form, ModelState);
        }
        else
        {
            await ValidationHelper.ValidateAsync<UserFormModelValidator, UserFormModel>(form, ModelState);
        }
        
        if (!ModelState.IsValid) return BadRequest(ModelState.ToErrors());

        if (!insert) return Ok();
        
        var overlapEmail = await Db.Users.FirstOrDefaultAsync(u => u.Email == form.Email);
        if (overlapEmail != null) ModelState.AddModelError(nameof(UserFormModel.Email), "Email address exist");
            
        var overlapLogin = await Db.Users.FirstOrDefaultAsync(u => u.Login == form.Login);
        if (overlapLogin != null) ModelState.AddModelError(nameof(UserFormModel.Login), "Login exist");
        
        if (!ModelState.IsValid) return Conflict(ModelState.ToErrors());

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<UserModel>> Register(UserFormModel form)
    {
        var validation = await ValidateForm(form, true);

        if (!ModelState.IsValid) return validation;
        
        var user = new User(form);

        var userEntity = user.AsStorable.SaveNotNull();
        
        await Db.Users.AddAsync(userEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<UserModel>(userEntity));
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