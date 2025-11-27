using Core.Models;
namespace Server.Repositories.User;

public interface IUserRepo
{
    Core.Models.User? GetUserById(string id);

    Task<Core.Models.User?> Login(string username, string password);
}