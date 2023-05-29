using AutoMapper;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;

namespace Mladim.Client.Services.SubjectServices.Implementations;

public class ParticipantService : IParticipantService
{
    private IGenericHttpService HttpService { get; }
    private IMapper Mapper { get; }
    private MladimApiUrls ApiUrls { get; }

    public ParticipantService(IGenericHttpService httpService, IMapper mapper, IOptions<MladimApiUrls> mladimApiUrls)
    {
        this.HttpService = httpService;
        this.Mapper = mapper;
        this.ApiUrls = mladimApiUrls.Value;
    }

    public async Task<IEnumerable<ParticipantVM>> GetByOrganizationIdAsync(int organizationId, bool isActive)
    {
        string url = string.Format(this.ApiUrls.GetParticipantsByOrganizationId, organizationId, false, isActive);
        var staffDto = await this.HttpService.GetAllAsync<ParticipantDetailsQueryDto>(url);
        return this.Mapper.Map<IEnumerable<ParticipantVM>>(staffDto);
    }

    public async Task<ParticipantVM?> AddAsync(int organizationId, ParticipantVM participant)
    {
        var command = this.Mapper.Map<AddParticipantCommandDto>(participant);
        command.OrganizationId = organizationId;

        var participanDto = await this.HttpService
            .PostAsync<AddParticipantCommandDto, ParticipantDetailsQueryDto>(ApiUrls.ParticipantCommand, command);

        return participanDto != null ? this.Mapper.Map<ParticipantVM>(participanDto) : null;
    }

    public async Task<bool> UpdateAsync(ParticipantVM participant)
    {
        var command = this.Mapper.Map<UpdateParticipantCommandDto>(participant);

        var succeedResponse = await this.HttpService
            .PutAsync<UpdateParticipantCommandDto>(ApiUrls.ParticipantCommand, command);

        return succeedResponse;
    }


    public async Task<IEnumerable<MemberBaseVM>> GetBaseByOrganizationIdAsync(int organizationId, bool isActive)
    {
        string url = string.Format(this.ApiUrls.GetParticipantsByOrganizationId, organizationId, true, isActive);
        var baseDto = await this.HttpService.GetAllAsync<MemberBase>(url);
        return this.Mapper.Map<IEnumerable<MemberBaseVM>>(baseDto);
    }
   
}
