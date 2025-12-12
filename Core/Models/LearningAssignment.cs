namespace Core.Models;


public class LearningAssignment
{
    public int assignmentId { get; set; }
    public Guid userId { get; set; }
    public int? articleId { get; set; }
    public int? testId { get; set; }
    public int subjectId { get; set; }
    public bool status { get; set; }
    public DateTime assigned { get; set; } = DateTime.Now;
    public DateTime? completedDate { get; set; }
} 