using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Data;
using Rnd.Models;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    public UsersController(DataContext data)
    {
        //DIs
        Data = data;
    }
    
    [HttpGet]
    public Task<ActionResult> Test()
    {
        var q = Data.Users;
        return Task.FromResult<ActionResult>(Ok());
    }

    //DIs
    public DataContext Data { get; }

}