using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;

namespace Core.Models;

public class MyGrowth 
{
    public int checkinId { get; set; }
    public Guid userId { get; set; }
    public int month { get; set; }
    public DateTime answerDate { get; set; } = DateTime.Now;
    
    
    [Column("answertext")]
    public string? answerText { get; set; } 
    
    
    [Column("fagligudfordring")]
    public string? FagligUdfordring { get; set; }
    
    [Column("nykompetence")]
    public string? NyKompetence { get; set; }
    
    [Column("motivation")]
    public string? Motivation { get; set; }
    
    [Column("trivsel")]
    public string? Trivsel { get; set; }
}