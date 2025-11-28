using System.Net.Http.Json;
using Core.Models;
namespace DanskeLearning.Services.DashboardService;

public class DashboardServiceHttp : IDashboardService
{
    private HttpClient _httpClient;

    public DashboardServiceHttp(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<List<Subject>> GetSubjectsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Subject>>("api/subject");
    }
}   