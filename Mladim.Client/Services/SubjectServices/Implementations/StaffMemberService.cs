﻿

using AutoMapper;
using Microsoft.Extensions.Options;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Dtos;
using Mladim.Client.Models;
using Mladim.Domain.Models;
using Mladim.Domain.Dtos.Members;
using Mladim.Client.ViewModels.Members.StaffMembers;

namespace Mladim.Client.Services.SubjectServices.Implementations;

public class StaffMemberService : IStaffMemberService
{
	private IGenericHttpService HttpService { get;}
    private IMapper Mapper { get; }
    private MladimApiUrls ApiUrls { get; }

    public StaffMemberService(IGenericHttpService httpService, IMapper mapper, IOptions<MladimApiUrls> mladimApiUrls)
	{
		this.HttpService = httpService;
        this.Mapper = mapper;
        this.ApiUrls = mladimApiUrls.Value;
    }

	public async Task<IEnumerable<StaffMemberVM>> GetByOrganizationIdAsync(int organizationId, bool isActive)
	{
		string url = string.Format(this.ApiUrls.GetStafMembersByOrganizationId, organizationId, false, isActive);				
		var staffDto = await this.HttpService.GetAllAsync<StaffMemberDetailsQueryDto>(url);
		return this.Mapper.Map<IEnumerable<StaffMemberVM>>(staffDto);	
	}

    public async Task<IEnumerable<NamedEntityVM>> GetBaseByOrganizationIdAsync(int organizationId, bool isActive)
    {
        string url = string.Format(this.ApiUrls.GetStafMembersByOrganizationId, organizationId, true, isActive);
        var baseDto = await this.HttpService.GetAllAsync<NamedEntityDto>(url);
        return this.Mapper.Map<IEnumerable<NamedEntityVM>>(baseDto);
    }

    public async Task<IEnumerable<StaffMemberLeadVM>> GetLeadStaffMembersAsync(int organizationId)
    {
        string url = string.Format(this.ApiUrls.GetLeadStaffMembers, organizationId);
        var baseDto = await this.HttpService.GetAllAsync<StaffMemberLeadQueryDto>(url);
        return this.Mapper.Map<IEnumerable<StaffMemberLeadVM>>(baseDto);
    }


    public async Task<StaffMemberVM?> AddAsync(int organizationId, StaffMemberVM staffMember)
	{
		var command = this.Mapper.Map<AddStaffMemberCommandDto>(staffMember);
		command.OrganizationId = organizationId;

		var staffMemberDto = await this.HttpService
			.PostAsync<AddStaffMemberCommandDto, StaffMemberDetailsQueryDto>(ApiUrls.StaffMemberCommand, command);
		
		return staffMemberDto != null ? this.Mapper.Map<StaffMemberVM>(staffMemberDto) : null;		
	}

    public async Task<bool> UpdateAsync(StaffMemberVM staffMember)
    {
        var command = this.Mapper.Map<UpdateStaffMemberCommandDto>(staffMember);       

        var succeedResponse = await this.HttpService
            .PutAsync<UpdateStaffMemberCommandDto>(ApiUrls.StaffMemberCommand, command);

		return succeedResponse;
    }



}
