using System.Net.Http.Json;
using Core.Models;

namespace DanskeLearning.Services.MyGrowthService
{
    public class MyGrowthService : IMyGrowthService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/mygrowth";

        public MyGrowthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAsync(MyGrowth growth)
        {
            await _httpClient.PostAsJsonAsync(BaseUrl, growth);
        }
        
        public async Task<List<MyGrowth>> GetPreviousAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<MyGrowth>>(
                $"{BaseUrl}/{userId}");

            return result ?? new List<MyGrowth>();
        }
        
        public async Task<List<MyGrowth>> GetEntryByIdAsync(int checkinId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<MyGrowth>>(
                $"{BaseUrl}/entry/{checkinId}");

            return result ?? new List<MyGrowth>();
        }
    }
}