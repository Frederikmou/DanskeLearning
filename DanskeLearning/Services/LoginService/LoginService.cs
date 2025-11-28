using System.Net.Http.Json;
using Core.Models;

namespace DanskeLearning.Services.LoginService;

public class LoginService
{
    private readonly HttpClient _client;
    private const string LoginEndpoint = "api/user/login";
    private const string UserEndpoint = "api/user";

    public LoginService(HttpClient client)
    {
        _client = client;
    }

public async Task<User?> Login(Login login)
{
    if (Login is null)
    {
        return null;
    }
    var response = await _client.PostAsJsonAsync(LoginEndpoint, login);
    if (!response.IsSuccessStatusCode)
    {
        return null;
    }

    var UserId = await response.Content.ReadFromJsonAsync<string>();
    if (UserId == 0)
    {
        return null;
    }
    return await _client.GetFromJsonAsync<User>($"{UserEndpoint}/{UserId}");
}

}
