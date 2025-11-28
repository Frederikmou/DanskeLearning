using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Server.Repositories.DashboardRepository;

namespace Server.Controllers;

[ApiController]
[Route("api/subject")]

public class DashboardController : ControllerBase
{
    private readonly IDashboardRepo dashboardRepo;

    public DashboardController(IDashboardRepo DashboardRepo)
    {
        dashboardRepo = DashboardRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSubjects()
    {
        var subjects = await dashboardRepo.GetAllSubjectsAsync();
        return Ok(subjects);
    }
    
}
