using Core.Models;
namespace DanskeLearning.Services.UserSessionService;

public interface IUserSessionService
{
   event Action? OnChange;
   User? CurrentUser { get; }
   Task InitializeAsync();
   Task SetUserAsync(User user);
   Task LogoutAsync();
}