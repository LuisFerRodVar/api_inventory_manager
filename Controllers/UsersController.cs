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
}
