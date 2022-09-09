using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Thin;

[ApiController]
[Route("thin/[controller]")]
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
    public async Task<ActionResult<User>> Get(Guid? id, string? login)
    {
        var user = await Db.Users.FirstOrDefaultAsync(GetUserQuery(id, login));

        if (user == null) return NoContent();
        
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> Insert(User user)
    {
        Db.Users.Add(user);

        await Db.SaveChangesAsync();

        return Ok(user);
    }
    
    [HttpPut]
    public async Task<ActionResult<User>> Update(User user)
    {
        Db.Users.Update(user);

        await Db.SaveChangesAsync();
        
        return Ok(user);
    }
    
    [HttpDelete("{id:guid?}")]
    public async Task<ActionResult<User>> Delete(Guid? id, string? login)
    {
        var user = await Db.Users.FirstOrDefaultAsync(GetUserQuery(id, login));

        if (user == null) return NoContent();

        Db.Users.Remove(user);

        await Db.SaveChangesAsync();
        
        return Ok(user);
    }

    private static Expression<Func<User, bool>> GetUserQuery(Guid? id, string? login)
    {
        return (id, login) switch
        {
            (null, null) => u => true,
            (_, null) => u => u.Id == id,
            (null, _) => u => u.Login == login,
            (_, _) => u => u.Id == id && u.Login == login,
        };
    }
}