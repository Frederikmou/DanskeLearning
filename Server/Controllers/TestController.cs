using Core.DTOs;
using Server.Repositories.TestRepo;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly ITestRepo _repo;

    public TestController(ITestRepo repo)
    {
        _repo = repo;
    }

    [HttpGet("subject/{subjectId:int}")]
    public async Task<IActionResult> GetTestBySubject(int subjectId)
    {
        var t = await _repo.GetTestBySubjectAsync(subjectId);
        if (t == null) return NotFound();
        return Ok(t);
    }

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitTest([FromBody] TestSubmitRequest req)
    {
        var r = await _repo.SubmitTestAsync(req);
        return Ok(r);
    }
}