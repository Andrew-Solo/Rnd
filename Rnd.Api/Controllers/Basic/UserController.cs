using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Data;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Users;

namespace Rnd.Api.Controllers.Basic;

[ApiController]
[Route("basic/[controller]/[action]")]
public class UserController : ControllerBase
{
    public UserController(DataContext db)
    {
        //DIs
        Db = db;
    }
    
    //DIs
    public DataContext Db { get; }
    
    public const string Salt = "48004582409e4b72b7254fa8eb89b5c5";
    
    [HttpGet]
    public async Task<IActionResult> Login(string login, string password)
    {
        var passwordHash = Hash.GenerateStringHash(password);
        var user = await Db.Users.FirstOrDefaultAsync(u => u.PasswordHash == passwordHash && (u.Login == login || u.Email == login));

        if (user == null) return NotFound();

        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(string email, string password, string? login)
    {
        var overlapEmail = await Db.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (overlapEmail != null) return Conflict();
        
        var overlapLogin = await Db.Users.FirstOrDefaultAsync(u => u.Login == login);
        
        if (overlapLogin != null) return Conflict();
        
        var user = new User(login ?? email, email, Hash.GenerateStringHash(password));

        var userEntity = user.AsStorable.SaveNotNull();

        await Db.Users.AddAsync(userEntity);

        return Ok(userEntity);
    }
    
    [HttpPut]
    public async Task<IActionResult> Edit(User user)
    {
        var userEntity = Db.Users.FirstOrDefault(u => u.Id == user.Id);

        if (userEntity == null) return NotFound();

        userEntity.Email = user.Email;
        userEntity.Login = user.Login;
        userEntity.PasswordHash = Hash.GenerateStringHash(user.PasswordHash);

        await Db.SaveChangesAsync();

        return Ok(userEntity);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(User user)
    {
        var userEntity = Db.Users.FirstOrDefault(u => u.Id == user.Id);
        
        if (userEntity == null) return NotFound();

        Db.Users.Remove(userEntity);

        await Db.SaveChangesAsync();

        return Ok();
    }
}