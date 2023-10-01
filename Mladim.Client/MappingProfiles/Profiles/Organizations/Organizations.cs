using AutoMapper;
using Mladim.Client.Extensions;
using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Organization;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.Organization;
using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;

namespace Mladim.Client.MappingProfiles.Profiles.Organizations;

public class Organizations : Profile
{
    public Organizations()
    {

        CreateMap<OrganizationVM, AddOrganizationCommandDto>();
        CreateMap<OrganizationVM, UpdateOrganizationCommandDto>();

        CreateMap<OrganizationAttributesVM, OrganizationAttributesCommandDto>()
           .ForMember(db => db.AgeGroups, dto => dto.MapFrom(field => (AgeGroups)(field.AgeGroups.Sum(x => (int)x))))
           .ForMember(db => db.YouthSectors, dto => dto.MapFrom(field => (YouthSectors)(field.YouthSectors.Sum(x => (int)x))))
           .ForMember(db => db.Types, dto => dto.MapFrom(field => (OrganizationTypes)(field.Types.Sum(x => (int)x))))
           .ForMember(db => db.Status, dto => dto.MapFrom(field => (OrganizationStatus)(field.Status.Sum(x => (int)x))))
           .ForMember(db => db.Fields, dto => dto.MapFrom(field => (OrganizationFields)(field.Fields.Sum(x => (int)x))))
           .ForMember(db => db.Regions, dto => dto.MapFrom(field => (OrganizationRegions)(field.Regions.Sum(x => (int)x))))
           .ForMember(db => db.NPMAims, dto => dto.MapFrom(field => (OrganizationNPMAims)(field.NPMAims.Sum(x => (int)x))));

        CreateMap<OrganizationQueryDto, OrganizationVM>();
        CreateMap<OrganizationStatisticQueryDto, OrganizationStatisticVM>();
       

        CreateMap<OrganizationAttributesQueryDto, OrganizationAttributesVM>()
             .ForMember(dto => dto.AgeGroups, dt => dt.MapFrom(field => field.AgeGroups.ToEnums()))
             .ForMember(dto => dto.YouthSectors, dt => dt.MapFrom(field => field.YouthSectors.ToEnums()))
             .ForMember(dto => dto.Types, dt => dt.MapFrom(field => field.Types.ToEnums()))
             .ForMember(dto => dto.Status, dt => dt.MapFrom(field => field.Status.ToEnums()))
             .ForMember(dto => dto.Fields, dt => dt.MapFrom(field => field.Fields.ToEnums()))
             .ForMember(dto => dto.Regions, dt => dt.MapFrom(field => field.Regions.ToEnums()))
             .ForMember(dto => dto.NPMAims, dt => dt.MapFrom(field => field.NPMAims.ToEnums()));
    }
}
