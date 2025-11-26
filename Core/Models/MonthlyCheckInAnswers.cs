namespace Core.Models;

public class MonthlyCheckInAnswers
{
    public int answerId { get; set; }
    public int checkinId { get; set; }
    public int questionId { get; set; }
    public string answerText { get; set; }
}