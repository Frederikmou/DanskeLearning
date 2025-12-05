namespace Core.Models;

public class MyGrowthAnswers
{
    public int AnswerId { get; set; }
    public Guid UserId { get; set; }
    public int QuestionId { get; set; }
    
    public string? FagligUdfordring { get; set; }
    public string? NyKompetence { get; set; }
    public string? Motivation { get; set; }
    public string? Trivsel { get; set;  }
    public string AnswerText { get; set; } = string.Empty;
    public DateTime AnswerDate { get; set; }
}