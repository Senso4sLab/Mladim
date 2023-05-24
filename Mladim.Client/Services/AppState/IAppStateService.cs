namespace Mladim.Client.Services.AppState;

public interface IAppStateService
{
    Task<string?> UserIdAsync();
}
