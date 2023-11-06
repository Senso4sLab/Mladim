using AutoMapper;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Pages;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;

namespace Mladim.Client.Services.SubjectServices.Implementations;

public class ActivityService : IActivityService
{
    private IMapper Mapper { get; }
    private MladimApiUrls MladimApiUrls { get; }
    private IGenericHttpService HttpClient { get; }
    private StorageKeys StorageKeys { get; }

    public ActivityService(IGenericHttpService httpClient, IOptions<MladimApiUrls> MladimApiUrls,
       IOptions<StorageKeys> storageKeys, IMapper mapper)
    {
        this.Mapper = mapper;
        this.HttpClient = httpClient;
        this.StorageKeys = storageKeys.Value;
        this.MladimApiUrls = MladimApiUrls.Value;
    }

    public async Task<bool> AddAsync(ActivityVM activity, int projectId)
    {
        var command = this.Mapper.Map<AddActivityCommandDto>(activity);
        command.ProjectId = projectId;

        return await this.HttpClient
            .PostAsync<AddActivityCommandDto, bool>(MladimApiUrls.ActivityCommand, command);       
    }

    public async Task<ActivityVM?> GetByActivityIdAsync(int activityId)
    {
        string url = string.Format(MladimApiUrls.GetActivityById, activityId);
        var activity = await HttpClient.GetAsync<ActivityQueryDetailsDto>(url);
        return this.Mapper.Map<ActivityVM>(activity);
    }

    public async Task<string?> GetActivityNameAsync(int activityId)
    {
        string url = string.Format(MladimApiUrls.GetActivityNameById, activityId);
        return await HttpClient.GetStringAsync(url);        
    }

    public async Task<IEnumerable<ActivityWithProjectNameVM>> GetByProjectIdAsync(int projectId, int? upcommingActivities = null)
    {
        string url = string.Format(MladimApiUrls.GetActivitiesByProjectId, projectId, upcommingActivities);
        var activities = await HttpClient.GetAllAsync<ActivityWithProjectNameQueryDto>(url);
        return this.Mapper.Map<IEnumerable<ActivityWithProjectNameVM>>(activities);
    }

    public async Task<IEnumerable<ActivityWithProjectNameVM>> GetByOrganizationIdAsync(int organizationId, int? upcommingActivities = null)
    {
        string url = string.Format(MladimApiUrls.GetActivitiesByOrganizationId, organizationId, upcommingActivities);
        var activities = await HttpClient.GetAllAsync<ActivityWithProjectNameQueryDto>(url);
        return this.Mapper.Map<IEnumerable<ActivityWithProjectNameVM>>(activities);
    }

    public async Task<bool> RemoveAsync(int activityId)
    {
        string url = string.Format(MladimApiUrls.RemoveActivity, activityId);

        if (await HttpClient.DeleteAsync(url))
            return true;

        return false;
    }

    public async Task<bool> UpdateAsync(ActivityVM activity)
    {
        var command = this.Mapper.Map<UpdateActivityCommandDto>(activity);

        var succeedResponse = await this.HttpClient
            .PutAsync(MladimApiUrls.ActivityCommand, command);

        return succeedResponse;
    }
}
