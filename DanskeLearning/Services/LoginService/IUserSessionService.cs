using Core.Models;
namespace DanskeLearning.Services.LoginService;

public interface IUserSessionService
{
   event Action? OnChange;
   User? CurrentUser { get; }
   Task InitializeAsync();
   Task SetUserAsync(User user);
   Task LogoutAsync();
}