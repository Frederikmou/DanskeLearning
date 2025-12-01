namespace Core.DTOs;

public class TestSubmitResult
{
    public bool Passed { get; set; }
    public int CorrectAnswers { get; set; }
    public int TotalQuestions { get; set; }
    public int ScorePercent => (int)Math.Round((double)CorrectAnswers / TotalQuestions * 100);
}