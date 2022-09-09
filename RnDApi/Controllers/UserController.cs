using System.Buffers.Text;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RnDApi.Data;
using RnDApi.Data.Entities;

namespace RnDApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public UserController(DataContext db)
    {
        //DIs
        Db = db;
    }
    
    //DIs
    public DataContext Db { get; }
    
    [HttpGet]
    public async Task<ActionResult<User?>> Get(Guid id)
    {
        var user = await Db.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return NotFound();
        
        return Ok(await Db.Users.FirstOrDefaultAsync(u => u.Id == id));
    }
    
    [HttpPost]
    public async Task<ActionResult> Update(User user)
    {
        var dbUser = await Db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

        if (dbUser == null) return NotFound();
        
        dbUser.Login = user.Login;
        dbUser.PasswordHash = user.PasswordHash;

        await Db.SaveChangesAsync();
        
        return Ok();
    }
    
    [HttpPut]
    public async Task<ActionResult> Insert(string login, string password)
    {
        var dbUser = await Db.Users.FirstOrDefaultAsync(u => u.Login == login);

        if (dbUser != null) return Conflict("User with same id already exist.");
        
        //TODO to class
        //TODO use salt
        var hasher = new HMACSHA256();
        var hashCode = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
        var passwordHash = Convert.ToBase64String(hashCode);
        
        Db.Users.Add(new User(login, passwordHash));

        await Db.SaveChangesAsync();

        return Ok();
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete(Guid id)
    {
        var dbUser = await Db.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (dbUser == null) return NotFound();

        Db.Users.Remove(dbUser);
        
        await Db.SaveChangesAsync();

        return Ok();
    }
}