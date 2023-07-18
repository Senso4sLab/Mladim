using AutoMapper;
using Blazored.LocalStorage;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;

namespace Mladim.Client.Services.SubjectServices.Implementations;

public class ProjectService : IProjectService
{
    private IMapper Mapper { get; }
    private MladimApiUrls MladimApiUrls { get; }
    private IGenericHttpService HttpClient { get; }   
    private StorageKeys StorageKeys { get; }

    public ProjectService(IGenericHttpService httpClient, IOptions<MladimApiUrls> MladimApiUrls,
         IOptions<StorageKeys> storageKeys, IMapper mapper)
    {
        this.Mapper = mapper;      
        this.HttpClient = httpClient;
        this.StorageKeys = storageKeys.Value;
        this.MladimApiUrls = MladimApiUrls.Value;
    }

    public async Task<IEnumerable<ProjectVM>> GetByOrganizationIdAsync(int organizationId)
    {
        string url = string.Format(MladimApiUrls.GetProjectsByOrganizationId, organizationId);
        var projects = await HttpClient.GetAllAsync<ProjectQueryDto>(url);
        return this.Mapper.Map<IEnumerable<ProjectVM>>(projects);
    }


    public async Task<ProjectVM?> GetByProjectIdAsync(int projectId)
    {
        string url = string.Format(MladimApiUrls.GetProjectById, projectId);
        var project = await HttpClient.GetAsync<ProjectQueryDetailsDto>(url);
        return this.Mapper.Map<ProjectVM>(project);
    }

    public async Task<bool> AddAsync(ProjectVM project, int organizationId)
    {
        var command = this.Mapper.Map<AddProjectCommandDto>(project);
        command.OrganizationId = organizationId;

        var projectDto = await this.HttpClient.PostAsync<AddProjectCommandDto, bool>(MladimApiUrls.ProjectCommand, command);
        return projectDto; 
    }

    public async Task<bool> UpdateAsync(ProjectVM project)
    {
        var command = this.Mapper.Map<UpdateProjectCommandDto>(project);

        var succeedResponse = await this.HttpClient
            .PutAsync(MladimApiUrls.ProjectCommand, command);

        return succeedResponse;
    }

    public async Task<bool> RemoveAsync(int projectId)
    {
        string url = string.Format(MladimApiUrls.RemoveProject, projectId);

        if (await HttpClient.DeleteAsync(url))                  
            return true;
        
        return false;
    }


}
