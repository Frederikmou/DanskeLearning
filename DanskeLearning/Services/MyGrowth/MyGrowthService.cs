
using System.Net.Http.Json;
using Core.Models;

namespace DanskeLearning.Services.MyGrowth;

public class MyGrowthService 
{
    private HttpClient _httpClient;

    public MyGrowthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    
}