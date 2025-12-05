using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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

    [HttpGet("User/{userId:guid}")]
    public async Task<ActionResult<List<MyGrowthAnswers>>> GetByUserAsync(Guid userId)
    {
        var answers = await _repo.GetByUserAsync(userId);
        return Ok(answers);
    }
}