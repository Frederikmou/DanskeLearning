using System.Net.Http.Json;
using Core.Models;

namespace DanskeLearning.Services.LoginService;

public class LoginService : ILoginService
{
    private readonly HttpClient _client;
    private const string LoginEndpoint = "http://localhost:5231/api/User/login";
    private const string UserEndpoint = "http://localhost:5231/api/User";

    public LoginService(HttpClient client)
    {
        _client = client;
    }

public async Task<User?> Login(Login login)
{
    if (Login == null)
    {
        return null;
    }
    var response = await _client.PostAsJsonAsync(LoginEndpoint, login);
    if (!response.IsSuccessStatusCode)
    {
        return null;
    }

    var UserId = await response.Content.ReadFromJsonAsync<string>();
    if (UserId == "0")
    {
        return null;
    }
    return await _client.GetFromJsonAsync<User>($"{UserEndpoint}/{UserId}");
}

}
