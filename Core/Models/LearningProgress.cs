namespace Core.Models;

public class LearningProgress
{
    public int progressId { get; set; }
    public int assignmentId { get; set; }
    public bool status { get; set; } //false = assgined, true = completed
    public int progressPercent { get; set; } //0-100
    public DateTime? completedDate { get; set; }
}