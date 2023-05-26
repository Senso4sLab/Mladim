using AutoMapper;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;

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

    public async Task<IEnumerable<ParticipantVM>> GetByOrganizationIdAsync(int organizationId, bool isAcitve)
    {
        string url = string.Format(this.ApiUrls.GetParticipantsByOrganizationId, organizationId, isAcitve);
        var staffDto = await this.HttpService.GetAllAsync<IEnumerable<ParticipantDto>>(url);
        return this.Mapper.Map<IEnumerable<ParticipantVM>>(staffDto);
    }

    public async Task<ParticipantVM?> AddAsync(int organizationId, ParticipantVM participant)
    {
        var command = this.Mapper.Map<AddParticipantVM>(participant);
        command.OrganizationId = organizationId;

        var participanDto = await this.HttpService
            .PostAsync<AddParticipantVM, ParticipantDto>(ApiUrls.ParticipantCommand, command);

        return participanDto != null ? this.Mapper.Map<ParticipantVM>(participanDto) : null;
    }

    public async Task<bool> UpdateAsync(ParticipantVM participant)
    {
        var command = this.Mapper.Map<UpdateParticipantVM>(participant);

        var succeedResponse = await this.HttpService
            .PutAsync<UpdateParticipantVM>(ApiUrls.ParticipantCommand, command);

        return succeedResponse;
    }




}
