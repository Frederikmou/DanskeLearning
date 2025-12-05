using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Models;

namespace DanskeLearning.Services.MyGrowthService
{
    public class MyGrowthService : IMyGrowthService
    {
        private readonly HttpClient _httpClient;

        public MyGrowthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task CreateAsync(MyGrowth growth)
        {
            await _httpClient.PostAsJsonAsync("api/mygrowth", growth);
        }
        
        public async Task<List<MyGrowthAnswers>> GetByUserAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<MyGrowthAnswers>>(
                $"api/mygrowth/user/{userId}");

            return result ?? new List<MyGrowthAnswers>();
        }
    }
}