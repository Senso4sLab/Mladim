using AutoMapper;
using Mladim.Application.Features.Activities.Commands.AddActivity;
using Mladim.Application.Features.Activities.Commands.UpdateActivity;
using Mladim.Application.Features.Organizations;
using Mladim.Application.Features.Organizations.Commands.AddOrganization;
using Mladim.Application.Features.Organizations.Commands.UpdateOrganization;
using Mladim.Application.Features.Projects.Commands.AddProject;
using Mladim.Application.Features.Projects.Commands.UpdateProject;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Applications.MappingProfiles;

public class ApplicationProfiles : Profile
{
	public ApplicationProfiles()
	{

        CreateMap<AddOrganizationCommand, Organization>();
        CreateMap<UpdateOrganizationCommand, Organization>();
        CreateMap<Organization,OrganizationDto>();	

		CreateMap<AddProjectCommand, Project>();
        CreateMap<UpdateProjectCommand, Project>();
		CreateMap<Project, ProjectDto>();


        CreateMap<AddActivityCommand, Activity>();
        CreateMap<UpdateActivityCommand, Activity>();
        CreateMap<Activity, ActivityDto>();


        CreateMap<MemberProjectDto, MemberProject>().ReverseMap();
		CreateMap<MemberDto, Member>()
			.Include<StaffMemberDto, StaffMember>()
			.Include<ParticipantDto, Participant>();

        CreateMap<Member, MemberDto>()
            .Include<StaffMember, StaffMemberDto>()
            .Include<Participant, ParticipantDto>();

        CreateMap<PartnerDto, Partner>().ReverseMap();
	}
}
