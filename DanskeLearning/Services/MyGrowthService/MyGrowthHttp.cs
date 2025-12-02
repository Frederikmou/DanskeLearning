using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Models;                     

namespace DanskeLearning.Services.MyGrowthService;

public class MyGrowthHttp : IMyGrowthService
{
    private readonly HttpClient _httpClient;

    public MyGrowthHttp(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(MyGrowth growth)   
    {
        var response = await _httpClient.PostAsJsonAsync("api/mygrowth", growth);
        response.EnsureSuccessStatusCode();
    }
}