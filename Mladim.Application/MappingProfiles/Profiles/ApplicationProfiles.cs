﻿using AutoMapper;
using Mladim.Application.Features.Accounts.Commands.UpdateAppUser;
using Mladim.Application.Features.Activities.Commands.AddActivity;
using Mladim.Application.Features.Activities.Commands.UpdateActivity;
using Mladim.Application.Features.Members.Participants.Commands.AddParticipant;
using Mladim.Application.Features.Members.Participants.Commands.UpdateParticipant;
using Mladim.Application.Features.Members.Partners.Commands.AddPartner;
using Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
using Mladim.Application.Features.Members.StaffMembers.Commands.UpdatePartner;
using Mladim.Application.Features.Members.StaffMembers.Commands.UpdateStaffMember;
using Mladim.Application.Features.Organizations.Commands.AddOrganization;
using Mladim.Application.Features.Organizations.Commands.UpdateOrganization;
using Mladim.Application.Features.Projects.Commands.AddProject;
using Mladim.Application.Features.Projects.Commands.UpdateProject;
using Mladim.Application.MappingProfiles.Converters;
using Mladim.Application.MappingProfiles.Resolvers;
using Mladim.Domain.Dtos;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;

namespace Mladim.Application.MappingProfiles.Profiles;

public class ApplicationProfiles : Profile
{
    
    public ApplicationProfiles()
    {

        //app user profile

        //CreateMap<UpdateAppUserCommand, AppUser>();

        //CreateMap<AppUser, AppUserQueryDto>();

        //socialurls

        //CreateMap<SocialMediaUrlsCommandDto, SocialMediaUrls>();
        //CreateMap<SocialMediaUrls, SocialMediaUrlsQueryDto>();


        //// Organizations
        //CreateMap<AddOrganizationCommand, Organization>();
        //CreateMap<UpdateOrganizationCommand, Organization>();
        //CreateMap<Organization, OrganizationQueryDto>().ReverseMap();
                     

        // Projects

        //CreateMap<AddProjectCommand, Project>();

        //CreateMap<UpdateProjectCommand, Project>()
        //    .ForMember(p => p.Groups, m => m.Ignore())
        //    .ForMember(p => p.Partners, m => m.Ignore())
        //    .ForMember(p => p.Staff, m => m.Ignore());



        //CreateMap<Project, ProjectQueryDetailsDto>().ReverseMap();
       
      


        //CreateMap<StaffMemberProjectDto, StaffMemberProject>().ReverseMap();
        //CreateMap<StaffMemberCommandDto, StaffMemberProject>();
        //CreateMap<StaffMemberProject, StaffMemberQueryDto>()
        //    .ForMember(dto => dto.Name, m => m.MapFrom(sm => sm.StaffMember.FullName))
        //    .ForMember(dto => dto.Surname, m => m.MapFrom(sm => sm.StaffMember.Surname));



        //CreateMap<ProjectGroupDto, ProjectGroup>().ReverseMap();

        //CreateMap<GroupCommandDto, ProjectGroup>();
       

        //// Groups
        //CreateMap<GroupDetailsQueryDto, Group>().ReverseMap();
        //CreateMap<GroupCommandDto, Group>();
        //CreateMap<ProjectGroup, GroupQueryDto>();



        //Activity

        //CreateMap<Activity, ActivityQueryDto>();




        //CreateMap<AddActivityCommand, Activity>()
        //    .ForMember(a => a.AnonymousParticipantActivities, m => m.MapFrom(ad => ad.AnonymousParticipantActivities));
           
            


        //CreateMap<UpdateActivityCommand, Activity>()
        //     .ForMember(p => p.Groups, m => m.Ignore())
        //     .ForMember(p => p.Partners, m => m.Ignore())
        //     .ForMember(p => p.Staff, m => m.Ignore())
        //     .ForMember(p => p.AnonymousParticipantActivities, m => m.Ignore())
        //     .ForMember(p => p.Participants, m => m.Ignore());


        //CreateMap<AnonymousParticipantActivity, AnonymousParticipantDetailsQueryDto>()
        //    .ConvertUsing<AnonymousParticipantActivityToCompactDto>();


        //CreateMap<ActivityWithProjectName, ActivityWithProjectNameQueryDto>();




        //CreateMap<StaffMemberActivityDto, StaffMemberActivity>().ReverseMap();

        //CreateMap<StaffMemberCommandDto, StaffMemberActivity>();
        //CreateMap<StaffMemberActivity, StaffMemberQueryDto>()
        //    .ForMember(dto => dto.Name, m => m.MapFrom(sm => sm.StaffMember.FullName))
        //    .ForMember(dto => dto.Surname, m => m.MapFrom(sm => sm.StaffMember.Surname));
        //CreateMap<ActivityGroupDto, ActivityGroup>().ReverseMap();

        //CreateMap<GroupCommandDto, ActivityGroup>();

        //CreateMap<AnonymousParticipantActivityDto, AnonymousParticipantActivity>().ReverseMap();
        //CreateMap<AnonymousParticipant, AnonymousParticipantQueryDto>();

        //StaffMember

        //CreateMap<AddStaffMemberCommand, StaffMember>();

        //CreateMap<AddStaffMemberCommand, OrganizationMember>()
        //  .ConvertUsing<AddCommandToOrganizationStaffMemberConverter>();

        //CreateMap<UpdateStaffMemberCommand, StaffMember>();

        //CreateMap<StaffMember, StaffMemberDetailsQueryDto>().ReverseMap();

        // Partners
        //CreateMap<AddPartnerCommand, Partner>();
        //CreateMap<PartnerCommandDto, Partner>();

        //CreateMap<AddPartnerCommand, OrganizationPartner>()
        //    .ConvertUsing<AddCommandToOrganizationPartnerConverter>();       

        //CreateMap<UpdatePartnerCommand, Partner>();

        //CreateMap<PartnerQueryDetailsDto, Partner>().ReverseMap();

        //CreateMap<Partner, PartnerQueryDto>();

        // Participant

        //CreateMap<AddParticipantCommand, Participant>();
        //CreateMap<AddParticipantCommand, OrganizationMember>()
        //   .ConvertUsing<AddCommandToOrganizationParticipantConverter>();

        //CreateMap<UpdateParticipantCommand, Participant>();

        //CreateMap<Participant, ParticipantDetailsQueryDto>().ReverseMap();
        //CreateMap<ParticipantCommandDto, Participant>();

        //CreateMap<Participant, ParticipantQueryDto>();
     

        // AnonymousParticipant

        //CreateMap<AnonymousParticipantQueryDto, AnonymousParticipant>().ReverseMap();

        //CreateMap<AnonymousParticipantCommandDto, AnonymousParticipantActivity>()
        //    .ConvertUsing<AnonymousParticipantCommandDtoToActivity>();


        //CreateMap<MemberDto, Member>()
        //    .Include<StaffMemberDetailsQueryDto, StaffMember>()
        //    .Include<ParticipantDetailsQueryDto, Participant>();

        //CreateMap<Member, MemberDto>()
        //    .Include<StaffMember, StaffMemberDetailsQueryDto>()
        //    .Include<Participant, ParticipantDetailsQueryDto>();


    }
}
