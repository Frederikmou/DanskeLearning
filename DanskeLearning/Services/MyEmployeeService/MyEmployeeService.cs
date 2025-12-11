using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Models;

namespace DanskeLearning.Services.MyEmployeeService;

public class MyEmployeeService : IMyEmployeeService
{
    private readonly HttpClient _httpClient;

    public MyEmployeeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MyEmployee>> GetMyEmployeesAsync()
    {
        var employees =
            await _httpClient.GetFromJsonAsync<List<MyEmployee>>("api/myemployee");

        if (employees == null)
        {
            return new List<MyEmployee>();
        }

        return employees;
    }
}