

using AutoMapper;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Dtos;


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

	public async Task<IEnumerable<StaffMember>> GetByOrganizationIdAsync(int organizationId, bool isAcitve)
	{
		string url = string.Format(this.ApiUrls.GetStafMembersByOrganizationId, organizationId, isAcitve);
		var staffDto = await this.HttpService.GetAllAsync<StaffMemberDto>(url);
		return this.Mapper.Map<IEnumerable<StaffMember>>(staffDto);	
	}

	public async Task<StaffMember?> AddAsync(int organizationId, StaffMember staffMember)
	{
		var command = this.Mapper.Map<AddStaffMemberCommand>(staffMember);
		command.OrganizationId = organizationId;

		var staffMemberDto = await this.HttpService
			.PostAsync<AddStaffMemberCommand, StaffMemberDto>(ApiUrls.StaffMemberCommand, command);
		
		return staffMemberDto != null ? this.Mapper.Map<StaffMember>(staffMemberDto) : null;		
	}

    public async Task<bool> UpdateAsync(StaffMember staffMember)
    {
        var command = this.Mapper.Map<UpdateStaffMemberCommand>(staffMember);       

        var succeedResponse = await this.HttpService
            .PutAsync<UpdateStaffMemberCommand>(ApiUrls.StaffMemberCommand, command);

		return succeedResponse;
    }



}
