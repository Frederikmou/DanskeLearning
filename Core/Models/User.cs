namespace Core.Models;

public class User
{
    public string UserId { get; set; }
    public string userName { get; set; } = string.Empty; //a-nummer
    public string firstName { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public string role { get; set; } = string.Empty;
    public double phoneNumber { get; set; }
}