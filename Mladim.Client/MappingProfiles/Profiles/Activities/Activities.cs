using AutoMapper;
using Mladim.Client.Extensions;
using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Activity;
using Mladim.Client.ViewModels.Project;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using MudBlazor;
using Mladim.Domain.Extensions;

namespace Mladim.Client.MappingProfiles.Profiles.Activities;

public class Activities : Profile
{
    public Activities()
    {
        CreateMap<ActivityVM, UpdateActivityCommandDto>()
             .ForMember(dest => dest.TimeRange, m => m.MapFrom(src => DateTimeRangeCommandDto.Create(src.DateRange.Start.Value, src.DateRange.End.Value, src.StartTime, src.EndTime)))
             .ForMember(dest => dest.Staff, m => m.MapFrom(src => src.Staff.Select(s => StaffMemberCommandDto.Create(s.Id, true)).Concat(src.Administration.Select(s => StaffMemberCommandDto.Create(s.Id, false))).ToList()));

        CreateMap<ActivityVM, AddActivityCommandDto>()
            .ForMember(dest => dest.TimeRange, m => m.MapFrom(src => DateTimeRangeCommandDto.Create(src.DateRange.Start.Value, src.DateRange.End.Value, src.StartTime, src.EndTime)))
            .ForMember(dest => dest.Staff, m => m.MapFrom(src => src.Staff.Select(s => StaffMemberCommandDto.Create(s.Id, true)).Concat(src.Administration.Select(s => StaffMemberCommandDto.Create(s.Id, false))).ToList()));

        
        CreateMap<ActivityQueryDetailsDto, ActivityVM>()
            .ForMember(dest => dest.DateRange, m => m.MapFrom(src => new DateRange(src.TimeRange.StartDate, src.TimeRange.EndDate)))
            .ForMember(dest => dest.StartTime, m => m.MapFrom(src => src.TimeRange.StartTime))
            .ForMember(dest => dest.EndTime, m => m.MapFrom(src => src.TimeRange.EndTime))
            .ForMember(dest => dest.Staff, m => m.MapFrom(src => src.Staff.Where(s => s.IsLead).ToList()))
            .ForMember(dest => dest.Administration, m => m.MapFrom(src => src.Staff.Where(s => !s.IsLead).ToList()));

        CreateMap<ActivityQueryDto, ActivityVM>()
            .ForMember(dest => dest.DateRange, m => m.MapFrom(src => new DateRange(src.TimeRange.StartDate, src.TimeRange.EndDate)))
            .ForMember(dest => dest.StartTime, m => m.MapFrom(src => src.TimeRange.StartTime))
            .ForMember(dest => dest.EndTime, m => m.MapFrom(src => src.TimeRange.EndTime));

        CreateMap<ActivityWithProjectNameQueryDto, ActivityWithProjectNameVM>();
             

        CreateMap<ActivityAttributesVM, ActivityAttributesCommandDto>()
             .ForMember(db => db.ActivityTypes, dto => dto.MapFrom(field => (ActivityTypes)(field.ActivityTypes.Sum(x => (int)x))));

        CreateMap<ActivityAttributesQueryDto, ActivityAttributesVM>()
             .ForMember(dto => dto.ActivityTypes, dt => dt.MapFrom(field => field.ActivityTypes.ToEnums()));
    }
}

