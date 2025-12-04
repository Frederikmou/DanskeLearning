using System.Net.Http.Json;

namespace DanskeLearning.Services.ArticleReadStatusService;

public class ArticleReadStatusService : IArticleReadStatusService
{
    private readonly HttpClient _httpClient;

    public ArticleReadStatusService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> GetReadStatusAsync(Guid userId, int articleId)
    {
        var response = await _httpClient.GetAsync($"api/articles/readstatus/{userId}/{articleId}");
        if (!response.IsSuccessStatusCode)
            return false;
        
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task SetReadStatusAsync(Guid userId, int articleId, bool isRead)
    {
        await _httpClient.PostAsJsonAsync($"api/articles/readstatus/{userId}/{articleId}", isRead);
    }
}

