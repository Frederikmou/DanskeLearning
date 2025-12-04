namespace Core.Models;

public class TestCompletionStatus
{
    public Guid UserId { get; set; }
    public int SubjectId { get; set; }
    public bool Passed { get; set; }
}