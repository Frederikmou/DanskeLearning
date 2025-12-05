using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Models;                     

namespace DanskeLearning.Services.MyGrowthService;

public class MyGrowthServiceHttp : IMyGrowthService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/mygrowth";

    public MyGrowthServiceHttp(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(MyGrowth growth)   
    {
        var response = await _httpClient.PostAsJsonAsync("api/mygrowth", growth);
        response.EnsureSuccessStatusCode();
    }   
}