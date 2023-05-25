using AutoMapper;
using Blazored.LocalStorage;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Dtos;
using System.Net.Http;
using System.Runtime.CompilerServices;

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

	public async Task<IEnumerable<Organization>> GetByUserIdAsync(string userId)
	{
		string url = string.Format(MladimApiUrls.GetOrganizationsByUserId, userId);		
		
		var organizations = await HttpClient.GetAllAsync<OrganizationDto>(url);	

		return this.Mapper.Map<IEnumerable<Organization>>(organizations);
		      
    }

	


    public async Task<bool> RemoveAsync(int organiozationId)
	{
        string url = string.Format(MladimApiUrls.GetOrganizationsByUserId, organiozationId);

		if (await HttpClient.DeleteAsync(url))
		{
			await RemoveDefaultOrganizationAsync();
			return true;
		}
		return false;
    }


	public Task SetDefaultOrganizationAsync(int orgId) =>
		Task.FromResult(this.Storage.SetItemAsync<int>(this.StorageKeys.SelectedOrganizationId, orgId));

	private Task RemoveDefaultOrganizationAsync() =>
		Task.FromResult(this.Storage.RemoveItemAsync(this.StorageKeys.SelectedOrganizationId));	



	public async Task<int?> DefaultOrganizationIdAsync() =>
		await this.Storage.ContainKeyAsync(this.StorageKeys.SelectedOrganizationId) ?
			await this.Storage.GetItemAsync<int>(this.StorageKeys.SelectedOrganizationId) : null;


    }
