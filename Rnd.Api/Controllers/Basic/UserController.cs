using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Controllers.Validation;
using Rnd.Api.Controllers.Validation.UserModel;
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
        await ValidationHelper.ValidateAsync<UserRegisterModelValidator, UserRegisterModel>(register, ModelState);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var overlapEmail = await Db.Users.FirstOrDefaultAsync(u => u.Email == register.Email);
        if (overlapEmail != null) ModelState.AddModelError(nameof(UserRegisterModel.Email), "Email address exist");
        var overlapLogin = await Db.Users.FirstOrDefaultAsync(u => u.Login == register.Login);
        if (overlapLogin != null) ModelState.AddModelError(nameof(UserRegisterModel.Login), "Login exist");
        
        if (!ModelState.IsValid) return Conflict(ModelState);
        
        var user = new User(register.Login ?? register.Email, register.Email, Hash.GenerateStringHash(register.Password));

        var userEntity = user.AsStorable.SaveNotNull();

        await Db.Users.AddAsync(userEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<UserModel>(userEntity));
    }
    
    [HttpPut]
    public async Task<IActionResult> Edit(UserEditModel edit)
    {
        await ValidationHelper.ValidateAsync<UserEditModelValidator, UserEditModel>(edit, ModelState);

        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var userEntity = Db.Users.FirstOrDefault(u => u.Id == edit.Id);
        if (userEntity == null) ModelState.AddModelError(nameof(Data.Entities.User), "User not found");

        if (!ModelState.IsValid) return NotFound(ModelState);
        
        Mapper.Map(edit, userEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<UserModel>(userEntity));
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userEntity = Db.Users.FirstOrDefault(u => u.Id == id);
        
        if (userEntity == null)
        {
            ModelState.AddModelError(nameof(Data.Entities.User), "User not found");
            return NotFound(ModelState);
        }
        
        Db.Users.Remove(userEntity);

        await Db.SaveChangesAsync();

        return Ok(Mapper.Map<UserModel>(userEntity));
    }
}