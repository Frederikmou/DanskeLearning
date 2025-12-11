using Core.Models;

namespace Server.Repositories.LearningAssignmentRepo;


public interface ILearningAssignmentRepo
{
    Task<LearningAssignment> CreateAssignmentAsync(LearningAssignment assignment);
    
    Task<LearningAssignment?> GetAssignmentByIdAsync(int assignmentId);
    
    Task<List<LearningAssignment>> GetAssignmentsByUserIdAsync(Guid userId);
    
    Task<List<LearningAssignment>> GetAllAssignmentsAsync();
    
    Task DeleteAssignmentAsync(int assignmentId);
    
    Task<List<LearningAssignment>> GetAssignmentsByStatusAsync(Guid userId, bool status);
}