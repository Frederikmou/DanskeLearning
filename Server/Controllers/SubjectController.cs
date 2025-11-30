using Microsoft.AspNetCore.Mvc;
using Server.Repositories.SubjectRepository;

namespace Server.Controllers;

[ApiController]
[Route("api/articles")]


public class SubjectController : ControllerBase
{
    private readonly ISubjectRepo _subjectRepo;

    public SubjectController(ISubjectRepo subjectRepo)
    {
        _subjectRepo = subjectRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetArticlesByIdAsync()
    {
        var articles = await _subjectRepo.GetAllArticleByIdAsync();
        return Ok(articles);
    }

}