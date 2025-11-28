using Core.Models;

namespace Server.Repositories.UserRepository;
    public interface IUserRepo
    {
        Task<User?> GetUserById(string id);

        Task<User?> Login(string username, string password);
    }