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

        public Task<List<MyGrowth>> GetPreviousAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<MyGrowth>> GeEntryByIdAsync(int chekinid)
        {
            throw new NotImplementedException();
        }
    }
}