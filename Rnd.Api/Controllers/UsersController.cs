using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;

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
    public DataContext Db { get; }
    
    [HttpGet("{id:guid?}")]
    public async Task<IActionResult> Get(Guid? id, string? email)
    {
        var user = await Db.Users.FirstOrDefaultAsync(GetUserQuery(id, email));

        if (user == null) return NoContent();
        
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> Insert(User user)
    {
        Db.Users.Add(user);

        await Db.SaveChangesAsync();

        return Ok(user);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(User user)
    {
        Db.Users.Update(user);

        await Db.SaveChangesAsync();
        
        return Ok(user);
    }
    
    [HttpDelete("{id:guid?}")]
    public async Task<IActionResult> Delete(Guid? id, string? email)
    {
        var user = await Db.Users.FirstOrDefaultAsync(GetUserQuery(id, email));

        if (user == null) return NoContent();

        Db.Users.Remove(user);

        await Db.SaveChangesAsync();
        
        return Ok(user);
    }

    private static Expression<Func<User, bool>> GetUserQuery(Guid? id, string? email)
    {
        return (id, email) switch
        {
            (null, null) => u => true,
            (_, null) => u => u.Id == id,
            (null, _) => u => u.Email == email,
            (_, _) => u => u.Id == id && u.Email == email,
        };
    }
}