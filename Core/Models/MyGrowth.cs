namespace Core.Models;

public class MyGrowth
{
    public int checkinId { get; set; }
    public Guid userId { get; set; }
    public int month { get; set; }
    public DateTime answerDate { get; set; } = DateTime.Now;
    
    public string? answerText { get; set; }
    public string? questionText { get; set; }
    
    public int answerId { get; set; }
    public int questionId { get; set; }
    
    public string? FagligUdfordring { get; set; }
    public string? NyKompetence { get; set; }
    public string? Motivation { get; set; }
    public string? Trivsel { get; set; }
}