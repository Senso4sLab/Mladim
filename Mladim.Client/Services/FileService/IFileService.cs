using AutoMapper;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.ViewModels.Organization;
using Mladim.Domain.Dtos.Organization;

namespace Mladim.Client.Services.FileService;

public interface IFileService
{
    Task<Stream?> GetFileStreamByProjectIdAsync(string fileName, int projectId);
    Task<Stream?> GetFileStreamByActivityIdAsync(string fileName, int activityId);
    Task<string?> AddOrganizationImageAsync(int organizationId, List<byte> data, string fileName, OrganizationImageType type);
    Task<string?> AddUserProfileImageAsync(string userId, List<byte> data, string fileName);
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

    public async Task<string?> AddOrganizationImageAsync(int organizationId, List<byte> data, string fileName, OrganizationImageType type)
    {
        var organizationUrl = OrganizationImageUrl(type);
        var urlProfile = await this.HttpClient.PostAsync<AddOrganizationProfileImageDto>(organizationUrl, AddOrganizationProfileImageDto.Create(organizationId,data,fileName));
        return urlProfile;
    }

    private string OrganizationImageUrl(OrganizationImageType type) => type switch
    {
        OrganizationImageType.Banner => this.MladimApiUrls.AddOrganizationBannerImage,
        OrganizationImageType.Profile => this.MladimApiUrls.AddOrganizationProfileImage,
        _ => throw new NotImplementedException(),
    };

    public async Task<string?> AddUserProfileImageAsync(string userId, List<byte> data, string fileName)
    {
        var userProfileImageUrl = this.MladimApiUrls.AddUserProfileImage;
        var urlProfile = await this.HttpClient.PostAsync<AddUserProfileImageDto>(userProfileImageUrl, AddUserProfileImageDto.Create(userId, data, fileName));
        return urlProfile;
    }
}



