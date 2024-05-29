using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Core.Entities;

namespace SecureTamSimulator.Api.Controllers;


[Route("user")]

public class UserController : Controller
{

    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {


        await _userService.AddUser(Guid.NewGuid(),user.FirstName, user.LastName, user.Address, user.Birthdate);
        
        
        return Ok(new
        {
            Message = "User was added!"
        });
    }

    [HttpGet("all")]
    public List<User> GetUsers()
    {
        return _userService.GetUsers();
    }
    [HttpGet("{id}")]
    public User GetUserById(string id)
    {
        return _userService.GetUserById(Guid.Parse(id));
    }
}