using Core.Models.User;
namespace Server.Repositories.User;

public interface IUserRepo
{
    User? GetUserById(int id);

    Task<User?> Login(string username, string password);
}