using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Mladim.Application.Models;
using Mladim.Client.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private ILocalStorageService Storage { get; }
    private StorageKeys Keys { get; }

    public JwtAuthenticationStateProvider(ILocalStorageService storage, IOptions<StorageKeys> keys)
    {
        this.Storage = storage;
        this.Keys = keys.Value;
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (await this.Storage.ContainKeyAsync(this.Keys.AccessToken))
        {
            var tokenAsString = await this.Storage.GetItemAsStringAsync(this.Keys.AccessToken);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(tokenAsString);

            var identity = new ClaimsIdentity(token.Claims, "Bearer");
            var user = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
            return authenticationState;
        }
        return new AuthenticationState(new ClaimsPrincipal());
    }
}