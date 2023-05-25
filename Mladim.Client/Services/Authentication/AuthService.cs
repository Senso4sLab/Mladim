﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Domain.Models.Login;
using Mladim.Domain.Models.Result;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Mladim.Client.Services.Authentication;

public class AuthService : IAuthService
{
    private IGenericHttpService HttpClient { get; }
    private AuthenticationStateProvider AuthStateProvider { get; }
    private ILocalStorageService Storage { get; }

    private MladimApiUrls MladimApiUrls {get;}
    private StorageKeys StorageKeys { get; }

    public AuthService(IGenericHttpService httpClient,
                       ILocalStorageService storage,
                       AuthenticationStateProvider authProvider,
                       IOptions<MladimApiUrls> mladimApiUrls, 
                       IOptions<StorageKeys> storageKeys)
    {
        this.HttpClient = httpClient;
        this.Storage = storage;
        this.AuthStateProvider = authProvider;
        this.MladimApiUrls = mladimApiUrls.Value;
        this.StorageKeys = storageKeys.Value;   

    }
    public async Task<Result<AuthResponse>> LoginAsync(LoginUser loginUser)
    {   
        var response = await this.HttpClient.PostAsync<LoginUser,Result<AuthResponse>>(this.MladimApiUrls.Login, loginUser);   
        
        if (!response.IsSucceed)
            return response;

        await this.Storage.SetItemAsStringAsync(this.StorageKeys.AccessToken, response.Value!.Token);
        await this.AuthStateProvider.GetAuthenticationStateAsync();
        return response;        
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
            await this.AuthStateProvider.GetAuthenticationStateAsync();
        }
    }
}
