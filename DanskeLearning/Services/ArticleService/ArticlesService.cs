using System.Net.Http.Json;
using Core.Models;

namespace DanskeLearning.Services.ArticleService;

public class ArticlesService : IArticlesService
{
    private HttpClient _httpClient;
    
    public ArticlesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<Articles>> GetArticlesByIdAsync(int  subjectId)
    {
        return await _httpClient.GetFromJsonAsync<List<Articles>>($"api/articles/subject/{subjectId}");
    }

    public async Task<List<Articles>> GetSingleArticlesAsync(int articleId)
    {
        return await _httpClient.GetFromJsonAsync<List<Articles>>($"api/articles/single/{articleId}");
    }
}