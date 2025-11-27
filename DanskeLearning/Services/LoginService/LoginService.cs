using System.Net.Http.Json;
using Core.Models;

namespace DanskeLearning.Services.LoginService;

public class LoginService
{
    private readonly HttpClient _client;
    private const string LoginEndpoint = "https://localhost:5001/api/login";
    private const string UserEndpoint = "https://localhost:5001/api/user";

    public LoginService(HttpClient client)
    {
        _client = client;
    }

public async Task<User?> Login(UserLogin login)
{
    if (login is null)
    {
        return null;
    }
    var response = await _client.PostAsJsonAsync(LoginEndpoint, login);
    if (!response.IsSuccessStatusCode)
    {
        return null;
    }

    var userId = await response.Content.ReadFromJsonAsync<int>();
    if (userId == 0)
    {
        return null;
    }
    return await _client.GetFromJsonAsync<User>($"{UserEndpoint}/{userId}");
}

}
