using AutoMapper;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;
using Mladim.Domain.IdentityModels;

namespace Mladim.Client.Services.AccountService;

public class AccountService : IAccountService
{

    private IMapper Mapper { get; }
    private MladimApiUrls MladimApiUrls { get; }
    private IGenericHttpService HttpClient { get; }
  

    public AccountService(IGenericHttpService httpClient, IOptions<MladimApiUrls> MladimApiUrls, IMapper mapper)
    {
        this.Mapper = mapper;
        this.HttpClient = httpClient;        
        this.MladimApiUrls = MladimApiUrls.Value;
    }

    public Task<bool> UpdateAccountAsync(AppUserVM appUser)
    {
        var appUserDto = this.Mapper.Map<AppUserDto>(appUser);
        return this.HttpClient.PutAsync(this.MladimApiUrls.AccountCommand, appUserDto);
    }

    public async Task<AppUserVM?> GetAccountByIdAsync(string userId)
    {
        var url = string.Format(this.MladimApiUrls.GetAccountById,userId);
        var appUserDto = await this.HttpClient.GetAsync<AppUserQueryDto>(url);
        return this.Mapper.Map<AppUserVM>(appUserDto);

        
    }

}

