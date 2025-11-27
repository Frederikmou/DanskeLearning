using Core.Models;
using Blazored.LocalStorage;
namespace DanskeLearning.Services.UserSessionService;

public class UserSessionService  : IUserSessionService
{
    private const string StorageKey = "currentUser";
    private readonly ILocalStorageService _localStorage;
    private bool _isInitialized;

    public UserSessionService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public event Action? OnChange;

    public User? CurrentUser { get;  private set; }

    public async Task InitializeAsync()
    {
        if (_isInitialized) return;
        CurrentUser = await _localStorage.GetItemAsync<User>(StorageKey);
        _isInitialized = true;
        NotifyStateChanged();
    }

    public async Task SetUserAsync(User user)
    {
        CurrentUser = user;
        await _localStorage.SetItemAsync(StorageKey, user);
        NotifyStateChanged();
    }

    public async Task LogoutAsync()
    {
        CurrentUser = null;
        await _localStorage.RemoveItemAsync(StorageKey);
        NotifyStateChanged();
    }
    private void NotifyStateChanged() => OnChange?.Invoke();
    
}