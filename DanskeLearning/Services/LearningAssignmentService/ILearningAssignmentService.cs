using Core.Models;

namespace DanskeLearning.Services.LearningAssignmentService;

public interface ILearningAssignmentService
{
    Task<LearningAssignment> CreateAssignmentAsync(LearningAssignment assignment);
    Task<List<LearningAssignment>> GetAllAssignmentsAsync();
    Task<LearningAssignment?> GetAssignmentByIdAsync(int assignmentId);
    Task<List<LearningAssignment>> GetAssignmentsByUserAsync(Guid userId);
    Task<List<LearningAssignment>> GetAssignmentsByStatusAsync(Guid userId, bool status);
    Task DeleteAssignmentAsync(int assignmentId);
} 