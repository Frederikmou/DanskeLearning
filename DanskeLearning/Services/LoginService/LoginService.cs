using System.Net.Http.Json;
using Core.Models;

namespace DanskeLearning.Services.LoginService;

public class LoginService : ILoginService
{
    private readonly HttpClient _client;
    private const string LoginEndpoint = "api/User/login";

    public LoginService(HttpClient client)
    {
        _client = client;
    }

public async Task<User?> Login(Login login)
{
    if (login == null)
    {
        return null;
    }
    var response = await _client.PostAsJsonAsync(LoginEndpoint, login);
    
    if (!response.IsSuccessStatusCode)
    {
        return null;
    }

    var user = await response.Content.ReadFromJsonAsync<User>();
    return user;
}

}
