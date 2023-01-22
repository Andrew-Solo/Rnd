using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Data;
using Rnd.Models;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    public UsersController(DataContext db)
    {
        //DIs
        Db = db;
    }

    //DIs
    public DataContext Db { get; set; }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User.View>> Get(Guid id)
    {
        return (await Db.Users.TryGetByIdAsync(id)).OnSuccess(m => m.GetView()).Send();
    }
    
    [HttpGet]
    public async Task<ActionResult> Login(string login, string password)
    {
        var passwordHash = Hash.GenerateStringHash(password);
        
        var user = await Db.Users.FirstOrDefaultAsync(u => u.PasswordHash == passwordHash && (u.Login == login || u.Email == login));
        if (user == null) return this.NotFound<User>();

        return Ok(user.GetView());
    }
    
    [HttpGet("[action]/{id:guid}")]
    public async Task<ActionResult<bool>> Exist(Guid id)
    {
        var exist = await Db.Users.AnyAsync(u => u.Id == id);
        return Ok(exist);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> ValidateCreate([FromQuery] User.Form form)
    {
        var result = await Rnd.Models.User.New.ValidateAsync(form);

        if (!result.IsValid) return BadRequest(result.Errors);
        
        // await ModelState.CheckNotExist(Db.Users, g => g.Email == form.Email);
        // await ModelState.CheckNotExist(Db.Users, g => g.Login == form.Login);

        return Ok();
    }
    
    [HttpGet("[action]/{id:guid}")]
    public async Task<ActionResult> ValidateUpdate(Guid id, [FromQuery] User.Form form)
    {
        var user = await Db.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return this.NotFound<User>();

        var result = await user.ValidateUpdateAsync(form);

        if (!result.IsValid) return BadRequest(result.Errors);
        
        // await ModelState.CheckNotExist(Db.Users, g => g.Email == form.Email);
        // await ModelState.CheckNotExist(Db.Users, g => g.Login == form.Login);
        
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<UserModel>> Create(User.Form form)
    {
        var validation = await ValidateCreate(form);

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

        user.SetForm(form);
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