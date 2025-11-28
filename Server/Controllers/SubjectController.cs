using Microsoft.AspNetCore.Mvc;
using Server.Repositories.SubjectRepository;

namespace Server.Controllers;

[ApiController]
[Route("api/subjects/{subjectId}")]


public class SubjectController : ControllerBase
{
    private readonly ISubjectRepo _subjectRepo;

    public SubjectController(ISubjectRepo subjectRepo)
    {
        _subjectRepo = subjectRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetArticlesByIdAsync(int subjectId)
    {
        var articles = await _subjectRepo.GetAllArticleByIdAsync(subjectId);
        return Ok(articles);
    }

}