using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Models;

namespace DanskeLearning.Services.MyTeamService;

public class MyTeamService : IMyTeamService
{
    private readonly HttpClient _httpClient;

    public MyTeamService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Team>> GetMyTeamsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Team>>("api/MyTeams");
    }
}