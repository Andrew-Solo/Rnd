using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;
using Attribute = Rnd.Api.Modules.RndCore.Parameters.AttributeParameters.Attribute;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase
{
    public TestController(DataContext db)
    {
        //DIs
        Db = db;
    }
    
    //DIs
    public DataContext Db { get; }
    
    [HttpGet]
    public Task<IActionResult> TestDb()
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}