using AutoMapper;
using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Organization;
using Mladim.Client.ViewModels.Project;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;
using MudBlazor;

namespace Mladim.Client.MappingProfiles.Profiles.Projects;

public class Projects : Profile
{
    public Projects()
    {
        CreateMap<ProjectVM, UpdateProjectCommandDto>()
            .ForMember(dest => dest.Staff, m => m.MapFrom(src => src.Staff.Select(s => StaffMemberCommandDto.Create(s.Id, true))
                .Concat(src.Administration.Select(s => StaffMemberCommandDto.Create(s.Id, false))).ToList()))
                .ForMember(dest => dest.TimeRange, m => m.MapFrom(src => DateTimeRangeCommandDto.Create(src.DateRange.Start.Value, src.DateRange.End.Value, TimeSpan.Zero, TimeSpan.Zero)));


        CreateMap<ProjectVM, AddProjectCommandDto>()
            .ForMember(dest => dest.Staff, m => m.MapFrom(src => src.Staff.Select(s => StaffMemberCommandDto.Create(s.Id, true))
                .Concat(src.Administration.Select(s => StaffMemberCommandDto.Create(s.Id, false))).ToList()))
                .ForMember(dest => dest.TimeRange, m => m.MapFrom(src => DateTimeRangeCommandDto.Create(src.DateRange.Start.Value, src.DateRange.End.Value, TimeSpan.Zero, TimeSpan.Zero)));
        

        CreateMap<ProjectQueryDetailsDto, ProjectVM>()
            .ForMember(dest => dest.Staff, m => m.MapFrom(src => src.Staff.Where(s => s.IsLead).ToList()))
            .ForMember(dest => dest.Administration, m => m.MapFrom(src => src.Staff.Where(s => !s.IsLead).ToList()))
            .ForMember(dest => dest.DateRange, m => m.MapFrom(src => new DateRange(src.TimeRange.StartDate, src.TimeRange.EndDate)));


        CreateMap<ProjectQueryDto, ProjectVM>();




        CreateMap<ProjectAttributesVM, ProjectAttributesCommandDto>();

        CreateMap<ProjectAttributesQueryDto, ProjectAttributesVM>();
    }
}
