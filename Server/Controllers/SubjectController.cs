using Microsoft.AspNetCore.Mvc;
using Server.Repositories.SubjectRepository;

namespace Server.Controllers;

[ApiController]
[Route("api/subjects/{subjectId}")]


public class SubjectController : ControllerBase
{
    private readonly ISubjectRepo SubjectRepo;

    public SubjectController(ISubjectRepo subjectRepo)
    {
        SubjectRepo = subjectRepo;
    }

}