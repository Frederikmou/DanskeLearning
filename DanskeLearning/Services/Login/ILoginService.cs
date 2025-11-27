using Core.Models;
namespace DanskeLearning.Services.Login;
public interface ILoginService
{
   Task<User?> Login(UserLogin login);
}