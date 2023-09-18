using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.HttpService.Generic;

using System.Net.Http.Json;
using System.Security.Claims;
using Mladim.Client.Models;
using Mladim.Application.Models;
using Mladim.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Mladim.Client.Services.Authentication;

public class AuthService : IAuthService
{
    private IGenericHttpService HttpClient { get; }
    private AuthenticationStateProvider AuthStateProvider { get; }
    private ILocalStorageService Storage { get; }

    private MladimApiUrls MladimApiUrls {get;}
    private StorageKeys StorageKeys { get; }

    public IAuthorizationService AuthorizationService { get; }

    public AuthService(IGenericHttpService httpClient,
                       ILocalStorageService storage,
                       IAuthorizationService authorizationService,
                       AuthenticationStateProvider authProvider,
                       IOptions<MladimApiUrls> mladimApiUrls, 
                       IOptions<StorageKeys> storageKeys)
    {
        this.HttpClient = httpClient;
        this.Storage = storage;
        this.AuthorizationService = authorizationService;
        this.AuthStateProvider = authProvider;
        this.MladimApiUrls = mladimApiUrls.Value;
        this.StorageKeys = storageKeys.Value;   

    }
    public async Task<Result<AuthResponse>> LoginAsync(LoginUser loginUser)
    {   
        var response = await this.HttpClient.PostAsync<LoginUser,Result<AuthResponse>>(this.MladimApiUrls.Login, loginUser);   
        
        if (!response.Succeeded)
            return response;

        await this.Storage.SetItemAsStringAsync(this.StorageKeys.AccessToken, response.Value!.Token);
        await this.AuthStateProvider.GetAuthenticationStateAsync();
        return response;        
    }

   


    public async Task<Result<AuthResponse>> ConfirmRegistrationAsync(string email, string emailToken,  string password)
    {
        var userConfirmation = new UserRegistrationConfirmation { Email = email, EmailToken = emailToken, Password = password };

        var response = await this.HttpClient.PostAsync<UserRegistrationConfirmation, Result<AuthResponse>>(this.MladimApiUrls.ConfirmRegistration, userConfirmation);

        if (!response.Succeeded)
            return response;

        await this.Storage.SetItemAsStringAsync(this.StorageKeys.AccessToken, response.Value!.Token);
        await this.AuthStateProvider.GetAuthenticationStateAsync();
        return response;
    }

   


    public async Task<bool> TryChangePasswordAsync(string userId, string oldPassword, string password)
    {
        string url = string.Format(this.MladimApiUrls.Password, userId, password);

        var changePassword = new ChangePassword { UserId = userId, OldPassword = oldPassword, Password = password };
        var response =  await this.HttpClient.PostAsync<ChangePassword, string>(url, changePassword);

        return response == userId;
    }

    public async Task<bool> IsUserPolicySatisfied(string organizationId, string policyName)
    {
        var state = await AuthStateProvider.GetAuthenticationStateAsync();
        var authorizationResult = await this.AuthorizationService.AuthorizeAsync(state.User, organizationId, policyName);
        return authorizationResult.Succeeded;
    }


    public async Task<string?> GetUserIdentityAsync()
    {

        var authState = await AuthStateProvider.GetAuthenticationStateAsync();


        return authState?.User?.Identity?.IsAuthenticated == true ?
             authState.User.FindFirstValue(ClaimTypes.NameIdentifier) : null;
      
    }



    public async Task LogoutAsync()
    {
        if (await this.Storage.ContainKeyAsync(this.StorageKeys.AccessToken))
        {
            await this.Storage.RemoveItemAsync(this.StorageKeys.AccessToken);
            await this.Storage.RemoveItemAsync(this.StorageKeys.SelectedOrganization);
            await this.AuthStateProvider.GetAuthenticationStateAsync();
        }
    }
}
