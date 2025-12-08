namespace Core.Models;

public class MyGrowthAnswers
{
    public Guid UserId { get; set; }
    
    
    public string? FagligUdfordring { get; set; }
    public string? NyKompetence { get; set; }
    public string? Motivation { get; set; }
    public string? Trivsel { get; set;  }
    public string AnswerText { get; set; } = string.Empty;
    public DateTime AnswerDate { get; set; }
}