using AutoMapper;
using Mladim.Client.Extensions;
using Mladim.Client.Models;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;

namespace Mladim.Client.MappingProfiles.Profiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Organization, OrganizationDto>()
           .ForMember(db => db.AgeGroups, dto => dto.MapFrom(field => (AgeGroups)(field.AgeGroups.Sum(x => (int)x))))
           .ForMember(db => db.YouthSectors, dto => dto.MapFrom(field => (YouthSectors)(field.YouthSectors.Sum(x => (int)x))))
           .ForMember(db => db.Types, dto => dto.MapFrom(field => (OrganizationTypes)(field.Types.Sum(x => (int)x))))
           .ForMember(db => db.Status, dto => dto.MapFrom(field => (OrganizationStatus)(field.Status.Sum(x => (int)x))))
           .ForMember(db => db.Fields, dto => dto.MapFrom(field => (OrganizationFields)(field.Fields.Sum(x => (int)x))))
           .ForMember(db => db.Regions, dto => dto.MapFrom(field => (OrganizationRegions)(field.Regions.Sum(x => (int)x))));

        CreateMap<OrganizationDto, Organization>()
           .ForMember(dto => dto.AgeGroups, dt => dt.MapFrom(field => field.AgeGroups.ToEnums()))
           .ForMember(dto => dto.YouthSectors, dt => dt.MapFrom(field => field.YouthSectors.ToEnums()))
           .ForMember(dto => dto.Types, dt => dt.MapFrom(field => field.Types.ToEnums()))
           .ForMember(dto => dto.Status, dt => dt.MapFrom(field => field.Status.ToEnums()))
           .ForMember(dto => dto.Fields, dt => dt.MapFrom(field => field.Fields.ToEnums()))
           .ForMember(dto => dto.Regions, dt => dt.MapFrom(field => field.Regions.ToEnums()));
               








        CreateMap<StaffMemberDto, StaffMember>().ReverseMap();
        CreateMap<StaffMember, AddStaffMemberCommand>();
        CreateMap<StaffMember, UpdateStaffMemberCommand>();

        
    }
}
