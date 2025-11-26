namespace Core.Models;

public class LearningAssignment
{
    public int assignmentId { get; set; }
    public int userId { get; set; }
    public int subjectId { get; set; }
    public DateTime assigned { get; set; } = DateTime.Now;
    
}