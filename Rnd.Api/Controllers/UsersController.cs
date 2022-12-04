using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Controllers.Validation;
using Rnd.Api.Controllers.Validation.UserModel;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("[controller]")]
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

        if (user == null) return this.NotFound<User>();

        return Ok(Mapper.Map<UserModel>(user));
    }
    
    [HttpGet]
    public async Task<ActionResult<UserModel>> Login(string login, string password)
    {
        var passwordHash = Hash.GenerateStringHash(password);
        
        var user = await Db.Users.FirstOrDefaultAsync(u => u.PasswordHash == passwordHash && (u.Login == login || u.Email == login));

        if (user == null) return this.NotFound<User>();

        return Ok(Mapper.Map<UserModel>(user));
    }
    
    [HttpGet("[action]/{id:guid}")]
    public async Task<ActionResult> Exist(Guid id)
    {
        var exist = await Db.Users.AnyAsync(u => u.Id == id);

        if (!exist) return this.NotFound<User>();

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> ValidateForm([FromQuery] UserFormModel form, bool insert = false)
    {
        //TODO Сделать флюэнт билдер валидации
        
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
        
        await ModelState.CheckNotExist(Db.Users, g => g.Email == form.Email);
        await ModelState.CheckNotExist(Db.Users, g => g.Login == form.Login);
        
        if (!ModelState.IsValid) return Conflict(ModelState.ToErrors());

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<UserModel>> Create(UserFormModel form)
    {
        var validation = await ValidateForm(form, true);

        if (!ModelState.IsValid) return validation;

        var user = Rnd.Api.Data.Entities.User.Create(form);

        await Db.Users.AddAsync(user);
        await Db.SaveChangesAsync();
        
        return Ok(Mapper.Map<UserModel>(user));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserModel>> Edit(Guid id, UserFormModel form)
    {
        var validation = await ValidateForm(form);

        if (!ModelState.IsValid) return validation;
        
        var user = Db.Users.FirstOrDefault(u => u.Id == id);

        if (user == null) return this.NotFound<User>();
        
        Mapper.Map(form, user);
        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<UserModel>(user));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<UserModel>> Delete(Guid id)
    {
        var user = Db.Users.FirstOrDefault(u => u.Id == id);

        if (user == null) return this.NotFound<User>();
        
        Db.Users.Remove(user);
        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<UserModel>(user));
    }
}