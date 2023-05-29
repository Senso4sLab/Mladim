﻿using AutoMapper;
using Mladim.Client.Extensions;
using Mladim.Client.ViewModels;

using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Client.MappingProfiles.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<OrganizationVM, AddOrganizationCommandDto>()
               .ForMember(db => db.AgeGroups, dto => dto.MapFrom(field => (AgeGroups)(field.AgeGroups.Sum(x => (int)x))))
               .ForMember(db => db.YouthSectors, dto => dto.MapFrom(field => (YouthSectors)(field.YouthSectors.Sum(x => (int)x))))
               .ForMember(db => db.Types, dto => dto.MapFrom(field => (OrganizationTypes)(field.Types.Sum(x => (int)x))))
               .ForMember(db => db.Status, dto => dto.MapFrom(field => (OrganizationStatus)(field.Status.Sum(x => (int)x))))
               .ForMember(db => db.Fields, dto => dto.MapFrom(field => (OrganizationFields)(field.Fields.Sum(x => (int)x))))
               .ForMember(db => db.Regions, dto => dto.MapFrom(field => (OrganizationRegions)(field.Regions.Sum(x => (int)x))));

            CreateMap<OrganizationVM, UpdateOrganizationCommandDto>()
               .ForMember(db => db.AgeGroups, dto => dto.MapFrom(field => (AgeGroups)(field.AgeGroups.Sum(x => (int)x))))
               .ForMember(db => db.YouthSectors, dto => dto.MapFrom(field => (YouthSectors)(field.YouthSectors.Sum(x => (int)x))))
               .ForMember(db => db.Types, dto => dto.MapFrom(field => (OrganizationTypes)(field.Types.Sum(x => (int)x))))
               .ForMember(db => db.Status, dto => dto.MapFrom(field => (OrganizationStatus)(field.Status.Sum(x => (int)x))))
               .ForMember(db => db.Fields, dto => dto.MapFrom(field => (OrganizationFields)(field.Fields.Sum(x => (int)x))))
               .ForMember(db => db.Regions, dto => dto.MapFrom(field => (OrganizationRegions)(field.Regions.Sum(x => (int)x))));
            
            CreateMap<OrganizationQueryDto, OrganizationVM>()
                    .ForMember(dto => dto.AgeGroups, dt => dt.MapFrom(field => field.AgeGroups.ToEnums()))
                    .ForMember(dto => dto.YouthSectors, dt => dt.MapFrom(field => field.YouthSectors.ToEnums()))
                    .ForMember(dto => dto.Types, dt => dt.MapFrom(field => field.Types.ToEnums()))
                    .ForMember(dto => dto.Status, dt => dt.MapFrom(field => field.Status.ToEnums()))
                    .ForMember(dto => dto.Fields, dt => dt.MapFrom(field => field.Fields.ToEnums()))
                    .ForMember(dto => dto.Regions, dt => dt.MapFrom(field => field.Regions.ToEnums()));
          

            CreateMap<StaffMemberSubjectVM, StaffMemberSubjectCommandDto>();
            CreateMap<GroupBaseVM, GroupCommandDto>();
            CreateMap<MemberBaseVM, PartnerCommandDto>();

            CreateMap<MemberBaseVM, ParticipantCommandDto>();

            CreateMap<AnonymousParticipantsVM, AnonymousParticipantCommandDto>();

            CreateMap<ProjectVM, UpdateProjectCommandDto>()
                .ForMember(dto => dto.Staff, dt => dt.MapFrom(field => field.LeadStaff.Select(mb => new StaffMemberSubjectCommandDto
                {
                    IsLead = true,
                    StaffMemberId= mb.Id,
                })
                .Union(field.Administrators.Select(mb => new StaffMemberSubjectCommandDto
                {
                    StaffMemberId = mb.Id,
                }))));

            CreateMap<ProjectVM, AddProjectCommandDto>()
                 .ForMember(dto => dto.Staff, dt => dt.MapFrom(field => field.LeadStaff.Select(mb => new StaffMemberSubjectCommandDto
                 {
                     IsLead = true,
                     StaffMemberId = mb.Id,
                 })
                .Union(field.Administrators.Select(mb => new StaffMemberSubjectCommandDto
                {
                    StaffMemberId = mb.Id,
                }))));


            CreateMap<ProjectQueryDto, ProjectVM>();


          

            CreateMap<PartnerQueryDto, MemberBaseVM>();
            CreateMap<GroupQueryDto, GroupBaseVM>();
            CreateMap<ParticipantQueryDto, MemberBaseVM>();
            CreateMap<StaffMemberSubjectQueryDto, StaffMemberSubjectVM>();
            CreateMap<AnonymousParticipantDetailsQueryDto, AnonymousParticipantsVM>();


            CreateMap<ActivityWithProjectNameQueryDto, ActivityWithProjectNameVM>();

            CreateMap<ActivityVM, AddActivityCommandDto>()
               .ForMember(db => db.ActivityTypes, dto => dto.MapFrom(field => (ActivityTypes)(field.ActivityTypes.Sum(x => (int)x))))
               .ForMember(dto => dto.Staff, dt => dt.MapFrom(field => field.LeadStaff.Select(mb => new StaffMemberSubjectCommandDto
               {
                   IsLead = true,
                   StaffMemberId = mb.Id,
               })
                .Union(field.Administrators.Select(mb => new StaffMemberSubjectCommandDto
                {
                    StaffMemberId = mb.Id,
                }))));


            CreateMap<ActivityVM, UpdateActivityCommandDto>()
               .ForMember(db => db.ActivityTypes, dto => dto.MapFrom(field => (ActivityTypes)(field.ActivityTypes.Sum(x => (int)x))))
               .ForMember(dto => dto.Staff, dt => dt.MapFrom(field => field.LeadStaff.Select(mb => new StaffMemberSubjectCommandDto
               {
                   IsLead = true,
                   StaffMemberId = mb.Id,
               })
                .Union(field.Administrators.Select(mb => new StaffMemberSubjectCommandDto
                {
                    StaffMemberId = mb.Id,
                }))));

         
            CreateMap<ActivityQueryDto, ActivityVM>();

         

            CreateMap<MemberBase, MemberBaseVM>()
                .Include<StaffMemberDetailsQueryDto, StaffMemberVM>()
                .Include<ParticipantDetailsQueryDto, ParticipantVM>();


            CreateMap<StaffMemberDetailsQueryDto, StaffMemberVM>();
            CreateMap<StaffMemberVM, AddStaffMemberCommandDto>();
            CreateMap<StaffMemberVM, UpdateStaffMemberCommandDto>();

            

            CreateMap<ParticipantDetailsQueryDto, ParticipantVM>();
            CreateMap<ParticipantVM, AddParticipantCommandDto>();
            CreateMap<ParticipantVM, UpdateParticipantCommandDto>();

            CreateMap<PartnerQueryDetailsDto, PartnerVM>();
            CreateMap<PartnerVM, AddPartnerCommandDto>();
            CreateMap<PartnerVM, UpdatePartnerCommandDto>();

        }
    }
}