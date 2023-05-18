using AutoMapper;
using Mladim.Application.Features.Activities.Commands.AddActivity;
using Mladim.Application.Features.Activities.Commands.UpdateActivity;
using Mladim.Application.Features.Members.Participants.Commands.AddParticipant;
using Mladim.Application.Features.Members.Participants.Commands.UpdateParticipant;
using Mladim.Application.Features.Members.Partners.Commands.AddPartner;
using Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
using Mladim.Application.Features.Members.StaffMembers.Commands.UpdatePartner;
using Mladim.Application.Features.Members.StaffMembers.Commands.UpdateStaffMember;
using Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMember;
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
        CreateMap<Organization, OrganizationDto>().ReverseMap();	

		CreateMap<AddProjectCommand, Project>();
        CreateMap<UpdateProjectCommand, Project>();
		CreateMap<Project, ProjectDto>().ReverseMap();


        CreateMap<AddActivityCommand, Activity>();
        CreateMap<UpdateActivityCommand, Activity>();
        CreateMap<Activity, ActivityDto>().ReverseMap();


        
       
        CreateMap<AddStaffMemberCommand, StaffMember>();
        CreateMap<UpdateStaffMemberCommand, StaffMember>();

        CreateMap<AddPartnerCommand, Partner>();
        CreateMap<UpdatePartnerCommand, Partner>();

        CreateMap<AddParticipantCommand, Participant>();
        CreateMap<UpdateParticipantCommand, Participant>();


        CreateMap<PartnerDto, Partner>().ReverseMap();
        CreateMap<StaffMember, StaffMemberDto>().ReverseMap();
        CreateMap<Participant, ParticipantDto>().ReverseMap();
        CreateMap<AnonymousParticipantsDto, AnonymousParticipants>().ReverseMap();

        

        CreateMap<MemberProjectDto, MemberProject>().ReverseMap();
		CreateMap<MemberDto, Member>()
			.Include<StaffMemberDto, StaffMember>()
			.Include<ParticipantDto, Participant>();

        CreateMap<Member, MemberDto>()
            .Include<StaffMember, StaffMemberDto>()
            .Include<Participant, ParticipantDto>();

      
    }
}
