using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Mladim.Client.Services.HttpService.Generic;
using System.Security.Claims;
using Mladim.Client.Models;
using Mladim.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

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

        if(response.Value.Mladim1ka)
        {
            await UserLogin1kaAsync(loginUser.Email, loginUser.Password); 
        }

        await this.Storage.SetItemAsStringAsync(this.StorageKeys.AccessToken, response.Value!.Token);
        await this.AuthStateProvider.GetAuthenticationStateAsync();
        return response;        
    }


    public async Task<Result> ResetPasswordAsync(UserEmail email)
    {
      
       return await this.HttpClient.PostAsync<UserEmail, Result>(this.MladimApiUrls.ResetPassword, email);
       
    }

    public async Task<Result> ConfirmResetPasswordAsync(NewPasswordUser user)
    {
        return await this.HttpClient.PostAsync<NewPasswordUser, Result>(this.MladimApiUrls.ConfirmResetPassword, user);
    }




    public async Task<Result<AuthResponse>> ConfirmRegistrationAsync(string name, string email, string emailToken,  string password, bool mladim1ka)
    {
        var userConfirmation = new UserRegistrationConfirmation { Name = name, Email = email, EmailToken = emailToken, Password = password, Mladim1ka = mladim1ka };

        var response = await this.HttpClient.PostAsync<UserRegistrationConfirmation, Result<AuthResponse>>(this.MladimApiUrls.ConfirmRegistration, userConfirmation);

        if (!response.Succeeded)
            return response;

        await this.Storage.SetItemAsStringAsync(this.StorageKeys.AccessToken, response.Value!.Token);
        await this.AuthStateProvider.GetAuthenticationStateAsync();
        return response;
    }

   


    public async Task<Result> TryChangePasswordAsync(string userId, string oldPassword, string password)
    {
        string url = string.Format(this.MladimApiUrls.Password, userId, password);

        var changePassword = new ChangePassword { UserId = userId, OldPassword = oldPassword, Password = password };        

        var response =  await this.HttpClient.PostAsync<ChangePassword, Result>(url, changePassword);

        return response;
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


    private async Task<bool> UserLogin1kaAsync(string email, string geslo)
    {       

        var user = new Login1kaUser(email, geslo, "Prijava");

        var userJson = JsonSerializer.Serialize(user);    

        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://mladim1ka.azurewebsites.net/1ka/frontend/api/api.php?action=login")
        {
            Content = new StringContent(userJson),
        };      
        
        message.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        message.Content.Headers.Add("Content-Length", $"{userJson.Length}");

        HttpClient client = new HttpClient();

        var response = await HttpClient.SendAsync(message);

        var responseCode = response.IsSuccessStatusCode;

        client?.Dispose();

        return responseCode;
    }


    public record Login1kaUser(string email, string pass, string submit);











}
