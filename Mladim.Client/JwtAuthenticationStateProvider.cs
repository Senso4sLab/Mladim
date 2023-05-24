using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private ILocalStorageService Storage { get; }

    public JwtAuthenticationStateProvider(ILocalStorageService storage)
    {
        this.Storage = storage;
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (await this.Storage.ContainKeyAsync("access_token"))
        {
            var tokenAsString = await this.Storage.GetItemAsStringAsync("access_token");
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(tokenAsString);

            var identity = new ClaimsIdentity(token.Claims, "Bearer");
            var user = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
            return authenticationState;
        }
        return new AuthenticationState(new System.Security.Claims.ClaimsPrincipal());
    }
}