namespace Core.Models;

public class LearningAssignment
{
    public int assignmentId { get; set; }
    public Guid userId { get; set; }
    public int subjectId { get; set; }
    public DateTime assigned { get; set; } = DateTime.Now;
    
}