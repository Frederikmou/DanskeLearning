using Core.Models;
namespace DanskeLearning.Services.LoginService;
public interface ILoginService
{
   Task<User> Login(UserLogin login);
}