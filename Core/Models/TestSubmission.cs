namespace Core.Models;

public class TestSubmission
{
    public int submissionId { get; set; }
    public int userId { get; set; }
    public int testId { get; set; }
    public DateTime subtmittedAt { get; set; } = DateTime.Now;
    public int questionId { get; set; }
    public int optionId { get; set; }
}