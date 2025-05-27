using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using inventory_manager.Models;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserDao _userDao;

    public UsersController(UserDao userDao)
    {
        _userDao = userDao;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers(int id)
    {
        var users = await _userDao.GetByIdAsync(id);
        return Ok(users);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User login){
        var user = await _userDao.Login(login.Email, login.Pass);
        if (user == null) return Unauthorized();
        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User register){
        var user = await _userDao.CreateUserAsync(register.Email, register.Pass);
        if (user == null) return BadRequest();
        return Ok(user);
    }

    [HttpPost("recover")]
    public async Task<IActionResult> Recover([FromBody] User recover){
        var user = await _userDao.Recovery(recover.Email, recover.Pass);
        if (user == null) return BadRequest();
        return Ok(user);
    }
    

}
