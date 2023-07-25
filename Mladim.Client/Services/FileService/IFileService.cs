using AutoMapper;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Domain.Dtos.Organization;

namespace Mladim.Client.Services.FileService;

public interface IFileService
{
    Task<Stream?> GetFileStreamByProjectIdAsync(string fileName, int projectId);
    Task<Stream?> GetFileStreamByActivityIdAsync(string fileName, int activityId);

    Task<string?> AddOrganizationProfileImageAsync(int organizationId, List<byte> data, string fileName);
}


public class FileService : IFileService
{
    private IMapper Mapper { get; }
    private MladimApiUrls MladimApiUrls { get; }
    private IGenericHttpService HttpClient { get; }
    
    public FileService(IGenericHttpService httpClient, IOptions<MladimApiUrls> MladimApiUrls,
       IMapper mapper)
    {
        this.Mapper = mapper;
        this.HttpClient = httpClient;       
        this.MladimApiUrls = MladimApiUrls.Value;
    }

    public async Task<Stream?> GetFileStreamByProjectIdAsync(string fileName, int projectId)
    {
        var url = string.Format(this.MladimApiUrls.GetFileByProjectId, fileName, projectId);       
        return await this.HttpClient.GetStreamAsync(url);
    }

    public async Task<Stream?> GetFileStreamByActivityIdAsync(string fileName, int activityId)
    {
        var url = string.Format(this.MladimApiUrls.GetFileByActivityId, fileName, activityId);
        return await this.HttpClient.GetAsync<Stream>(url);
    }

    public async Task<string?> AddOrganizationProfileImageAsync(int organizationId, List<byte> data, string fileName)
    {
        var url = this.MladimApiUrls.AddOrganizationProfileImage;
        var urlProfile = await this.HttpClient.PostAsync<AddOrganizationProfileImageDto>(url, AddOrganizationProfileImageDto.Create(organizationId,data,fileName));
        return urlProfile;
    }
}



