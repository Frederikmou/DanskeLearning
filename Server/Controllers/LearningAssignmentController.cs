using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.LearningAssignmentRepo;

namespace Server.Controllers;

[ApiController]
[Route("api/AdminAssign")]
public class LearningAssignmentController : ControllerBase
{
    private readonly ILearningAssignmentRepo _assignmentRepo;

    
    public LearningAssignmentController(ILearningAssignmentRepo assignmentRepo)
    {
        _assignmentRepo = assignmentRepo;
    }
    [HttpPost]
    public async Task<IActionResult> CreateAssignment([FromBody] LearningAssignment assign)
    {
        // Set default values if not provided
        assign.status = false;
        if (assign.assigned == default(DateTime))
        {
            assign.assigned = DateTime.Now;
        }

        var createdAssignment = await _assignmentRepo.CreateAssignmentAsync(assign);
        return Ok(createdAssignment);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAssignments()
    {
        var assignments = await _assignmentRepo.GetAllAssignmentsAsync();
        return Ok(assignments);
    }

    [HttpGet("{assignmentId:int}")]
    public async Task<IActionResult> GetAssignmentById(int assignmentId)
    {
        var assignment = await _assignmentRepo.GetAssignmentByIdAsync(assignmentId);
        if (assignment == null)
        {
            return NotFound();
        }
        return Ok(assignment);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetAssignmentsByUser(Guid userId)
    {
        var assignments = await _assignmentRepo.GetAssignmentsByUserIdAsync(userId);
        return Ok(assignments);
    }

    [HttpGet("user/{userId:guid}/status/{status:bool}")]
    public async Task<IActionResult> GetAssignmentsByStatus(Guid userId, bool status)
    {
        var assignments = await _assignmentRepo.GetAssignmentsByStatusAsync(userId, status);
        return Ok(assignments);
    }

    [HttpDelete("{assignmentId:int}")]
    public async Task<IActionResult> DeleteAssignment(int assignmentId)
    {
        await _assignmentRepo.DeleteAssignmentAsync(assignmentId);
        return Ok();
    }
}