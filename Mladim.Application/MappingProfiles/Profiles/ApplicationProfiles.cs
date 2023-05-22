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
using Mladim.Application.MappingProfiles.Converters;
using Mladim.Application.MappingProfiles.Resolvers;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles;

public class ApplicationProfiles : Profile
{
    
    public ApplicationProfiles()
    {
        // Organizations
        CreateMap<AddOrganizationCommand, Organization>();
        CreateMap<UpdateOrganizationCommand, Organization>();
        CreateMap<Organization, OrganizationDto>().ReverseMap();

        
           
              

        // Projects

        CreateMap<AddProjectCommand, Project>();
        CreateMap<UpdateProjectCommand, Project>();
        CreateMap<Project, ProjectDto>().ReverseMap();


        CreateMap<StaffMemberProjectDto, StaffMemberProject>().ReverseMap();
        CreateMap<ProjectGroupDto, ProjectGroup>().ReverseMap();
        CreateMap<GroupDto, Group>().ReverseMap();

        //Activity

        CreateMap<AddActivityCommand, Activity>();
        CreateMap<UpdateActivityCommand, Activity>();
        CreateMap<Activity, ActivityDto>().ReverseMap();


        CreateMap<StaffMemberActivityDto, StaffMemberActivity>().ReverseMap();
        CreateMap<ActivityGroupDto, ActivityGroup>().ReverseMap();

        CreateMap<AnonymousParticipantActivityDto, AnonymousParticipantActivity>().ReverseMap();


        //StaffMember

        CreateMap<AddStaffMemberCommand, StaffMember>();

        CreateMap<AddStaffMemberCommand, OrganizationMember>()
          .ConvertUsing<AddCommandToOrganizationStaffMemberConverter>();

        CreateMap<UpdateStaffMemberCommand, StaffMember>();

        CreateMap<StaffMember, StaffMemberDto>().ReverseMap();

        // Partners
        CreateMap<AddPartnerCommand, Partner>();

        CreateMap<AddPartnerCommand, OrganizationPartner>()
            .ConvertUsing<AddCommandToOrganizationPartnerConverter>();       

        CreateMap<UpdatePartnerCommand, Partner>();

        CreateMap<PartnerDto, Partner>().ReverseMap();

        // Participant

        CreateMap<AddParticipantCommand, Participant>();
        CreateMap<AddParticipantCommand, OrganizationMember>()
           .ConvertUsing<AddCommandToOrganizationParticipantConverter>();

        CreateMap<UpdateParticipantCommand, Participant>();

        CreateMap<Participant, ParticipantDto>().ReverseMap();

        // AnonymousParticipant

        CreateMap<AnonymousParticipantDto, AnonymousParticipant>().ReverseMap();

        CreateMap<MemberDto, Member>()
            .Include<StaffMemberDto, StaffMember>()
            .Include<ParticipantDto, Participant>();

        CreateMap<Member, MemberDto>()
            .Include<StaffMember, StaffMemberDto>()
            .Include<Participant, ParticipantDto>();


    }
}
