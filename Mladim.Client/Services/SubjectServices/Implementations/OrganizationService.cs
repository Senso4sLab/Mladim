using AutoMapper;
using Blazored.LocalStorage;
using Microsoft.Extensions.Options;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Dtos;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Mladim.Client.Models;
using Mladim.Domain.Models;
using Mladim.Client.ViewModels.Organization;
using Mladim.Domain.Dtos.Organization;

namespace Mladim.Client.Services.SubjectServices.Implementations;

public class OrganizationService : IOrganizationService
{
	private IMapper Mapper { get; }
	private MladimApiUrls MladimApiUrls { get; }
	private IGenericHttpService HttpClient { get; }
	private ILocalStorageService Storage { get; }
    private StorageKeys StorageKeys { get; }

    public OrganizationService(IGenericHttpService httpClient, IOptions<MladimApiUrls> MladimApiUrls,
        ILocalStorageService storage, IOptions<StorageKeys>storageKeys,IMapper mapper)
	{
		this.Mapper = mapper;
        this.Storage = storage;        
        this.HttpClient = httpClient;
		this.StorageKeys = storageKeys.Value;
        this.MladimApiUrls = MladimApiUrls.Value;
    }


	public Task SetDefaultOrganizationAsync(DefaultOrganization defaultOrg) =>
		Task.FromResult(this.Storage.SetItemAsync(this.StorageKeys.SelectedOrganization, defaultOrg));

	private Task RemoveDefaultOrganizationAsync() =>
		Task.FromResult(this.Storage.RemoveItemAsync(this.StorageKeys.SelectedOrganization));	


	public async Task<DefaultOrganization?> DefaultOrganizationAsync() =>
		await this.Storage.ContainKeyAsync(this.StorageKeys.SelectedOrganization) ?
			await this.Storage.GetItemAsync<DefaultOrganization>(this.StorageKeys.SelectedOrganization) : null;


    public async Task<IEnumerable<OrganizationVM>> GetByUserIdAsync(string userId)
    {
        string url = string.Format(MladimApiUrls.GetOrganizationsByUserId, userId);
        var organizations = await HttpClient.GetAllAsync<OrganizationQueryDto>(url);
        return this.Mapper.Map<IEnumerable<OrganizationVM>>(organizations);        
       
    }

   
    public async Task<OrganizationVM?> GetByIdAsync(int organizationId)
    {
        string url = string.Format(MladimApiUrls.GetOrganizationById, organizationId);
        var organizations = await HttpClient.GetAsync<OrganizationQueryDto>(url);
        return this.Mapper.Map<OrganizationVM>(organizations);
    }

    public async Task<OrganizationStatisticVM?> GetStatisticsByYearAsync(int organizationId, int year)
    {
        string url = string.Format(MladimApiUrls.GetOrganizationStatistics, organizationId, year);
        var organizations = await HttpClient.GetAsync<OrganizationStatisticQueryDto>(url);
        return this.Mapper.Map<OrganizationStatisticVM>(organizations);
    }



    public async Task<OrganizationVM?> AddAsync(OrganizationVM organization, string userId)
    {
        var command = this.Mapper.Map<AddOrganizationCommandDto>(organization);
        command.AppUserId = userId;

        var organizationDto = await this.HttpClient
            .PostAsync<AddOrganizationCommandDto, OrganizationQueryDto>(MladimApiUrls.OrganizationCommand, command);
        
        return organizationDto != null ? this.Mapper.Map<OrganizationVM>(organizationDto) : null;
    }

    public async Task<bool> UpdateAsync(OrganizationVM organization)
    {
        var command = this.Mapper.Map<UpdateOrganizationCommandDto>(organization);

        var succeedResponse = await this.HttpClient
            .PutAsync(MladimApiUrls.OrganizationCommand, command);

        return succeedResponse;
    }

    public async Task<bool> RemoveAsync(int organiozationId)
    {
        string url = string.Format(MladimApiUrls.RemoveOrganization, organiozationId);

        if (await HttpClient.DeleteAsync(url))
        {
            await RemoveDefaultOrganizationAsync();
            return true;
        }
        return false;
    }



}
