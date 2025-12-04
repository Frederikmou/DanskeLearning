using Core.DTOs;
using Server.Repositories.TestReposi;
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
    
    // GET: api/test/completion/{userId}/{subjectId}
    [HttpGet("completion/{userId:guid}/{subjectId:int}")]
    public async Task<IActionResult> GetTestCompletion(Guid userId, int subjectId)
    {
        var status = await _repo.GetTestCompletionStatusAsync(userId, subjectId);
        return Ok(status);
    }

    // POST: api/test/completion/{userId}/{subjectId}
    [HttpPost("completion/{userId:guid}/{subjectId:int}")]
    public async Task<IActionResult> SetTestCompletion(Guid userId, int subjectId, [FromBody] bool passed)
    {
        await _repo.SetTestCompletionStatusAsync(userId, subjectId, passed);
        return Ok();
    }
}