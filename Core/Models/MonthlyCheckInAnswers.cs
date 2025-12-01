namespace Core.Models;

public class MonthlyCheckInAnswers
{
    public int answerId { get; set; }
    public int checkInId { get; set; }
    public int userId { get; set; }
    public int questionId { get; set; }
    public string answerText { get; set; }
    public DateTime answerDate { get; set; }
    
}