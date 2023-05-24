using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Mladim.Client.Services.AppState;

public class AppStateService : IAppStateService
{
    private AuthenticationStateProvider AuthStateProvider { get; }
    public AppStateService(AuthenticationStateProvider provider)
    {        
        this.AuthStateProvider = provider;
    }
    public async Task<string?> UserIdAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();

        return authState?.User?.Identity?.IsAuthenticated == true ?
             authState.User.FindFirstValue(ClaimTypes.NameIdentifier) : null;
    }
}
