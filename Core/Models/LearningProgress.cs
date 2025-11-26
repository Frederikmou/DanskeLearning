namespace Core.Models;

public class LearningProgress
{
    public int progressId { get; set; }
    public int assignmentId { get; set; }
    public bool status { get; set; }
    public DateTime completedDate { get; set; } = DateTime.Now;
}