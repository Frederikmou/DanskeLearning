namespace Core.DTOs;

public class TestQuestionDto
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public List<TestQuestionOptionDto> Options { get; set; } = new();
}