using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Data;
using Rnd.Api.Helpers;
using Rnd.Api.Models.User;
using Rnd.Api.Modules.Basic.Users;

namespace Rnd.Api.Controllers.Basic;

[ApiController]
[Route("basic/[controller]/[action]")]
public class UserController : ControllerBase
{
    public UserController(DataContext db, IMapper mapper)
    {
        //DIs
        Db = db;
        Mapper = mapper;
    }
    
    //DIs
    public DataContext Db { get; }
    public IMapper Mapper { get; }
    
    [HttpGet]
    public async Task<IActionResult> Login(string login, string password)
    {
        var passwordHash = Hash.GenerateStringHash(password);
        var user = await Db.Users.FirstOrDefaultAsync(u => u.PasswordHash == passwordHash && (u.Login == login || u.Email == login));

        if (user == null) return NotFound();

        return Ok(Mapper.Map<UserModel>(user));
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterModel register)
    {
        var overlapEmail = await Db.Users.FirstOrDefaultAsync(u => u.Email == register.Email);

        if (overlapEmail != null) return Conflict();
        
        var overlapLogin = await Db.Users.FirstOrDefaultAsync(u => u.Login == register.Login);
        
        if (overlapLogin != null) return Conflict();
        
        var user = new User(register.Login ?? register.Email, register.Email, Hash.GenerateStringHash(register.Password));

        var userEntity = user.AsStorable.SaveNotNull();

        await Db.Users.AddAsync(userEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<UserModel>(userEntity));
    }
    
    [HttpPut]
    public async Task<IActionResult> Edit(UserEditModel edit)
    {
        var userEntity = Db.Users.FirstOrDefault(u => u.Id == edit.Id);

        if (userEntity == null) return NotFound();

        if (edit.Email != null) userEntity.Email = edit.Email;
        if (edit.Login != null) userEntity.Login = edit.Login;
        if (edit.Password != null) userEntity.PasswordHash = Hash.GenerateStringHash(edit.Password);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<UserModel>(userEntity));
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userEntity = Db.Users.FirstOrDefault(u => u.Id == id);
        
        if (userEntity == null) return NotFound();

        Db.Users.Remove(userEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<UserModel>(userEntity));
    }
}