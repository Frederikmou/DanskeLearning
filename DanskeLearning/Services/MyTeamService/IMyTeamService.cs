using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace DanskeLearning.Services.MyTeamService;

public interface IMyTeamService
{
    Task<List<Team>> GetMyTeamsAsync();
}