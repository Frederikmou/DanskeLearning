using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.UserRepository;
using Core.Models;

namespace Server.Controllers;


[ApiController]
[Route("api/User")]

public class UserController : ControllerBase
{
    private readonly IUserRepo userRepo;

    public UserController(IUserRepo userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Login login)
    {
        var user = await userRepo.Login(login.userName, login.password);

        if (user == null)
        {
            return Unauthorized();
        }

        return Ok(user.UserId);
    }
}