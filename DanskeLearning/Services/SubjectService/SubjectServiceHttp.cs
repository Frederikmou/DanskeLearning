using System.Net.Http.Json;
using Core.Models;

namespace DanskeLearning.Services.SubjectService;

public class SubjectServiceHttp : ISubjectService
{

    private HttpClient _httpClient;

    public SubjectServiceHttp(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<Articles>> GetAllArticlesAsync()
    {
      return await _httpClient.GetFromJsonAsync<List<Articles>>($"api/subjects/{{subjectId}}");
    }
}