namespace Core.Models;

public class TestQuestionOption
{
    public int optionId { get; set; }
    public int questionId { get; set; }
    public string optionText { get; set; }
    public bool isCorrect { get; set; }
}