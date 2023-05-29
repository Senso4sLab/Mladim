using AutoMapper;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;

namespace Mladim.Client.Services.SubjectServices.Implementations;

public class PartnerService : IPartnerService
{
    private IGenericHttpService HttpService { get; }
    private IMapper Mapper { get; }
    private MladimApiUrls ApiUrls { get; }

    public PartnerService(IGenericHttpService httpService, IMapper mapper, IOptions<MladimApiUrls> mladimApiUrls)
    {
        this.HttpService = httpService;
        this.Mapper = mapper;
        this.ApiUrls = mladimApiUrls.Value;
    }

    public async Task<PartnerVM?> AddAsync(int organizationId, PartnerVM partner)
    {
        var command = this.Mapper.Map<AddPartnerCommandDto>(partner);
        command.OrganizationId = organizationId;

        var partnerDto = await this.HttpService
            .PostAsync<AddPartnerCommandDto, PartnerQueryDetailsDto>(ApiUrls.PartnerCommand, command);

        return partnerDto != null ? this.Mapper.Map<PartnerVM>(partnerDto) : null;
    }

    public async Task<IEnumerable<PartnerVM>> GetByOrganizationIdAsync(int organizationId, bool isAcitve)
    {
        string url = string.Format(this.ApiUrls.GetPartnersByOrganizationId, organizationId, isAcitve);
        var partnerDto = await this.HttpService.GetAllAsync<PartnerQueryDetailsDto>(url);
        return this.Mapper.Map<IEnumerable<PartnerVM>>(partnerDto);
    }

    public async Task<bool> UpdateAsync(PartnerVM partner)
    {
        var command = this.Mapper.Map<UpdatePartnerCommandDto>(partner);

        var succeedResponse = await this.HttpService
            .PutAsync<UpdatePartnerCommandDto>(ApiUrls.PartnerCommand, command);

        return succeedResponse;
    }
   
    public async Task<IEnumerable<MemberBaseVM>> GetBaseByOrganizationIdAsync(int organizationId, bool isActive)
    {
        string url = string.Format(this.ApiUrls.GetPartnersByOrganizationId, organizationId, true, isActive);
        var baseDto = await this.HttpService.GetAllAsync<MemberBase>(url);
        return this.Mapper.Map<IEnumerable<MemberBaseVM>>(baseDto);
    }
}
