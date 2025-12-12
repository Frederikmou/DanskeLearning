using Core.Models;

namespace Server.Repositories.MyTeamsRepo;


public interface IMyTeamsRepo
{
    Task<List<Team>> GetMyTeamsAsync();
}