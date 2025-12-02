namespace Core.Models;

public class TestQuestion
{
    public int questionId { get; set; }
    public int testId { get; set; }
    public string questionText { get; set; }
    public int order { get; set; }
}