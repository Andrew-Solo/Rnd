using Microsoft.AspNetCore.Mvc;
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

    //DIs
    public DataContext Data { get; }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User.View>> Get(Guid id)
    {
        return (await Data.Users.GetAsync(id)).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult<User.View>> Login(string login, string password)
    {
        return (await Data.Users.LoginAsync(login, password)).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<User.View>> Create(User.Form form)
    {
        return (await Data.Users.CreateAsync(form)).ToActionResult();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<User.View>> Update(Guid id, User.Form form)
    {
        return (await Data.Users.UpdateAsync(id, form)).ToActionResult();
    }
}