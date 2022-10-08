using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Client.Models.Basic.User;

namespace Rnd.Api.Controllers.Basic;

[ApiController]
[Route("basic/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public Task<ActionResult<UserModel>> Get(Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<ActionResult<UserModel>> Login(string login, string password)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<ActionResult<UserModel>> Register(UserRegisterModel register)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<ActionResult<UserModel>> Edit(UserEditModel edit)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<ActionResult<UserModel>> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}