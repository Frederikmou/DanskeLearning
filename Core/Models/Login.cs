namespace Core.Models;

public class Login
{
    public string UserId { get; set; } // A-Nummer
    public string password { get; set; }
    public bool isAdmin { get; set; }
}