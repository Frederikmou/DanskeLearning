using System.Net.Http.Json;
using Core.DTOs;

namespace DanskeLearning.Services.TestService;

public class TestService : ITestService
{
    private readonly HttpClient _http;

    public TestService(HttpClient http)
    {
        _http = http;
    }

    public async Task<TestDto?> GetTest(int subjectId)
    {
        return await _http.GetFromJsonAsync<TestDto>($"api/test/subject/{subjectId}");
    }

    public async Task<TestSubmitResult?> Submit(TestSubmitRequest req)
    {
        var res = await _http.PostAsJsonAsync("api/test/submit", req);
        if (!res.IsSuccessStatusCode) return null;
        return await res.Content.ReadFromJsonAsync<TestSubmitResult>();
    }
}