using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.MyGrowthRepo;

namespace Server.Controllers;

[ApiController]
[Route("api/mygrowth")]
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

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetPreviousAsync(Guid userId)
    {
        var results = await _repo.GetPreviousAsync(userId);
        return Ok(results);
    }
}