namespace Core.Models;

public class User
{
    public Guid UserId { get; set; }
    public string userName { get; set; } //a-nummer
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string role { get; set; }
    public string phoneNumber { get; set; }
}