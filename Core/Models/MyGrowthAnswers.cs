namespace Core.Models;

public class MyGrowthAnswers
{
    public int AnswerId { get; set; }
    public Guid UserId { get; set; }
    public int QuestionId { get; set; }
    public string AnswerText { get; set; } = string.Empty;
    public DateTime AnswerDate { get; set; }
}