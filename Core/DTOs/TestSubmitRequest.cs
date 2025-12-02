namespace Core.DTOs;

public class TestSubmitRequest
{
    public int TestId { get; set; }
    public Guid UserId { get; set; }
    public List<TestSubmitAnswer> Answers { get; set; } = new();
}