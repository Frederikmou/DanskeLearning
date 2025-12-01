namespace Core.Models;

public class MyGrowth
{
    public int checkinId { get; set; }
    public int userId { get; set; }
    public int month { get; set; }
    public DateTime createdAt { get; set; } = DateTime.Now;
    
    public string? FagligUdfordring { get; set; }
    public string? NyKompetence { get; set; }
    public string? Motivation { get; set; }
    public string? Trivsel { get; set; }
}