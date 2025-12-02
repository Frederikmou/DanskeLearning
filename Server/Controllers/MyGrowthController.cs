using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.MyGrowth;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MyGrowthController : ControllerBase
{
    private readonly IMyGrowthRepo _repo;

    public MyGrowthController(IMyGrowthRepo repo)
    {
        _repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> Create(MyGrowth growth)
    {
        await _repo.CreateAsync(growth);
        return Ok();
    }
}