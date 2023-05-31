using AutoMapper;
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

        CreateMap<UpdateAppUserCommand, AppUser>();

        CreateMap<AppUser, AppUserQueryDto>();

        //socialurls

        CreateMap<SocialMediaUrlsCommandDto, SocialMediaUrls>();
        CreateMap<SocialMediaUrls, SocialMediaUrlsQueryDto>();


        // Organizations
        CreateMap<AddOrganizationCommand, Organization>();
        CreateMap<UpdateOrganizationCommand, Organization>();
        CreateMap<Organization, OrganizationQueryDto>().ReverseMap();
                     

        // Projects

        CreateMap<AddProjectCommand, Project>();

        CreateMap<UpdateProjectCommand, Project>()
            .ForMember(p => p.Groups, m => m.Ignore())
            .ForMember(p => p.Partners, m => m.Ignore())
            .ForMember(p => p.Staff, m => m.Ignore());



        CreateMap<Project, ProjectQueryDto>().ReverseMap();
       
      


        //CreateMap<StaffMemberProjectDto, StaffMemberProject>().ReverseMap();
        CreateMap<StaffMemberSubjectCommandDto, StaffMemberProject>();
        CreateMap<StaffMemberProject, StaffMemberSubjectQueryDto>()
            .ForMember(dto => dto.Name, m => m.MapFrom(sm => sm.StaffMember.Name));
       

        //CreateMap<ProjectGroupDto, ProjectGroup>().ReverseMap();

        CreateMap<GroupCommandDto, ProjectGroup>();
       

        // Groups
        CreateMap<GroupDetailsQueryDto, Group>().ReverseMap();
        CreateMap<GroupCommandDto, Group>();
        CreateMap<ProjectGroup, GroupQueryDto>();



        //Activity

        CreateMap<Activity, ActivityQueryDto>();




        CreateMap<AddActivityCommand, Activity>()
            .ForMember(a => a.AnonymousParticipantActivities, m => m.MapFrom(ad => ad.AnonymousParticipantActivities));
           
            


        CreateMap<UpdateActivityCommand, Activity>()
             .ForMember(p => p.Groups, m => m.Ignore())
             .ForMember(p => p.Partners, m => m.Ignore())
             .ForMember(p => p.Staff, m => m.Ignore())
             .ForMember(p => p.AnonymousParticipantActivities, m => m.Ignore())
             .ForMember(p => p.Participants, m => m.Ignore());


        CreateMap<AnonymousParticipantActivity, AnonymousParticipantDetailsQueryDto>()
            .ConvertUsing<AnonymousParticipantActivityToCompactDto>();


        CreateMap<ActivityWithProjectName, ActivityWithProjectNameQueryDto>();




        //CreateMap<StaffMemberActivityDto, StaffMemberActivity>().ReverseMap();

        CreateMap<StaffMemberSubjectCommandDto, StaffMemberActivity>();
        CreateMap<StaffMemberActivity, StaffMemberSubjectQueryDto>()
            .ForMember(dto => dto.Name, m => m.MapFrom(sm => sm.StaffMember.Name));

        //CreateMap<ActivityGroupDto, ActivityGroup>().ReverseMap();

        CreateMap<GroupCommandDto, ActivityGroup>();

        //CreateMap<AnonymousParticipantActivityDto, AnonymousParticipantActivity>().ReverseMap();
        CreateMap<AnonymousParticipant, AnonymousParticipantQueryDto>();

        //StaffMember

        CreateMap<AddStaffMemberCommand, StaffMember>();

        CreateMap<AddStaffMemberCommand, OrganizationMember>()
          .ConvertUsing<AddCommandToOrganizationStaffMemberConverter>();

        CreateMap<UpdateStaffMemberCommand, StaffMember>();

        CreateMap<StaffMember, StaffMemberDetailsQueryDto>().ReverseMap();

        // Partners
        CreateMap<AddPartnerCommand, Partner>();
        CreateMap<PartnerCommandDto, Partner>();

        CreateMap<AddPartnerCommand, OrganizationPartner>()
            .ConvertUsing<AddCommandToOrganizationPartnerConverter>();       

        CreateMap<UpdatePartnerCommand, Partner>();

        CreateMap<PartnerQueryDetailsDto, Partner>().ReverseMap();

        CreateMap<Partner, PartnerQueryDto>();

        // Participant

        CreateMap<AddParticipantCommand, Participant>();
        CreateMap<AddParticipantCommand, OrganizationMember>()
           .ConvertUsing<AddCommandToOrganizationParticipantConverter>();

        CreateMap<UpdateParticipantCommand, Participant>();

        CreateMap<Participant, ParticipantDetailsQueryDto>().ReverseMap();
        CreateMap<ParticipantCommandDto, Participant>();

        CreateMap<Participant, ParticipantQueryDto>();
     

        // AnonymousParticipant

        CreateMap<AnonymousParticipantQueryDto, AnonymousParticipant>().ReverseMap();

        CreateMap<AnonymousParticipantCommandDto, AnonymousParticipantActivity>()
            .ConvertUsing<AnonymousParticipantCommandDtoToActivity>();


        CreateMap<MemberDetailsDto, Member>()
            .Include<StaffMemberDetailsQueryDto, StaffMember>()
            .Include<ParticipantDetailsQueryDto, Participant>();

        CreateMap<Member, MemberDetailsDto>()
            .Include<StaffMember, StaffMemberDetailsQueryDto>()
            .Include<Participant, ParticipantDetailsQueryDto>();


    }
}
