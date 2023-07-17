using AutoMapper;
using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Organization;
using Mladim.Client.ViewModels.Project;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Attributes;

namespace Mladim.Client.MappingProfiles.Profiles.Projects;

public class Projects : Profile
{
    public Projects()
    {
        CreateMap<ProjectVM, UpdateProjectCommandDto>()
            .ForMember(dest => dest.Staff, m => m.MapFrom(src => src.Staff.Select(s => StaffMemberCommandDto.Create(s.Id, true)).Concat(src.Staff.Select(s => StaffMemberCommandDto.Create(s.Id, true))).ToList()));

        CreateMap<ProjectVM, AddProjectCommandDto>()
            .ForMember(dest => dest.Staff, m => m.MapFrom(src => src.Staff.Select(s => StaffMemberCommandDto.Create(s.Id, true)).Concat(src.Staff.Select(s => StaffMemberCommandDto.Create(s.Id, true))).ToList()));
        

        CreateMap<ProjectQueryDetailsDto, ProjectVM>()
            .ForMember(dest => dest.Staff, m => m.MapFrom(src => src.Staff.Where(s => s.IsLead).ToList()))
            .ForMember(dest => dest.Administration, m => m.MapFrom(src => src.Staff.Where(s => !s.IsLead).ToList()));

        CreateMap<ProjectQueryDto, ProjectVM>();


        CreateMap<ProjectAttributesVM, ProjectAttributesCommandDto>();

        CreateMap<ProjectAttributesQueryDto, ProjectAttributesVM>();
    }
}
