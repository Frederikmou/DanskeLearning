using Core.Models;
using Blazored.SessionStorage;
namespace DanskeLearning.Services.UserSessionService;

public class UserSessionService  : IUserSessionService
{
    private const string StorageKey = "currentUser";
    private readonly ISessionStorageService _sessionStorage;
    private bool _isInitialized;

    public UserSessionService(ISessionStorageService sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public event Action? OnChange; 

    public User? CurrentUser { get;  private set; }

    public async Task InitializeAsync()
    {
        if (_isInitialized) return;
        CurrentUser = await _sessionStorage.GetItemAsync<User>(StorageKey);
        _isInitialized = true;
        NotifyStateChanged();
    }

    public async Task SetUserAsync(User user)
    {
        CurrentUser = user;
        await _sessionStorage.SetItemAsync(StorageKey, user);
        NotifyStateChanged();
    }

    public async Task LogoutAsync()
    {
        CurrentUser = null;
        await _sessionStorage.RemoveItemAsync(StorageKey);
        NotifyStateChanged();
    }
    private void NotifyStateChanged() => OnChange?.Invoke();
    
}