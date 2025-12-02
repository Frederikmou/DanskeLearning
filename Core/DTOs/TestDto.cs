namespace Core.DTOs;

public class TestDto
{
    public int TestId { get; set; }
    public int SubjectId { get; set; }
    public string Title { get; set; }
    public string DescriptionText { get; set; }
    public List<TestQuestionDto> Questions { get; set; } = new();
}