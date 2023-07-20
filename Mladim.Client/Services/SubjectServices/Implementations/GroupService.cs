using AutoMapper;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.Dtos.Group;

namespace Mladim.Client.Services.SubjectServices.Implementations;

public class GroupService : IGroupService
{

    private IMapper Mapper { get; }
    private MladimApiUrls MladimApiUrls { get; }
    private IGenericHttpService HttpClient { get; }

    public GroupService(IGenericHttpService httpClient, IOptions<MladimApiUrls> mladimApiUrls,
            IOptions<StorageKeys> storageKeys, IMapper mapper) 
        => (HttpClient, MladimApiUrls, Mapper) = (httpClient, mladimApiUrls.Value, mapper);


    public async Task<GroupVM?> GetByGroupIdAsync(int groupId)
    {
        string url = string.Format(MladimApiUrls.GetActivityById, groupId);
        var group = await HttpClient.GetAsync<GroupDetailsQueryDto>(url);
        return this.Mapper.Map<GroupVM>(group);
    }

    public async Task<IEnumerable<GroupVM>> GetByOrganizationIdAsync(int organizationId, GroupType groupType,  bool isActive)
    {
        string url = string.Format(this.MladimApiUrls.GetGroupsByOrganizationId, organizationId, groupType, isActive);
        var groupDto = await this.HttpClient.GetAllAsync<GroupQueryDto>(url);
        return this.Mapper.Map<IEnumerable<GroupVM>>(groupDto);
    }

    public async Task<GroupVM?> AddAsync(int organizationId, GroupVM group)
    {
        var command = this.Mapper.Map<AddGroupCommandDto>(group);
        command.OrganizationId = organizationId;

        var groupDto = await this.HttpClient
            .PostAsync<AddGroupCommandDto, GroupQueryDto>(MladimApiUrls.GroupCommand, command);

        return groupDto != null ? this.Mapper.Map<GroupVM>(groupDto) : null;
    }

    public async Task<bool> UpdateAsync(GroupVM group)
    {
        var command = this.Mapper.Map<UpdateGroupCommandDto>(group);

        var succeedResponse = await this.HttpClient
            .PutAsync<UpdateGroupCommandDto>(MladimApiUrls.GroupCommand, command);

        return succeedResponse;
    }
}
