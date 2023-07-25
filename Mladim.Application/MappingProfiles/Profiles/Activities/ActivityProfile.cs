using AutoMapper;
using Mladim.Application.Features.Activities.Commands.AddActivity;
using Mladim.Application.Features.Activities.Commands.UpdateActivity;
using Mladim.Application.Features.Projects.Commands.AddProject;
using Mladim.Application.Features.Projects.Commands.UpdateProject;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.Activities;

public class ActivityProfile : Profile
{
	public ActivityProfile()
	{
        CreateMap<Activity, ActivityQueryDto>();

        CreateMap<Activity, ActivityQueryDetailsDto>()
            .ForMember(dest => dest.AnonymousParticipantActivities, m => m.MapFrom(src => src.AnonymousParticipantGroups));

        CreateMap<AddActivityCommand, Activity>()
            .ForMember(dest => dest.AnonymousParticipantGroups, m => m.MapFrom(src => src.AnonymousParticipantActivities))
             .ForMember(dest => dest.Files, m => m.Ignore());

        CreateMap<UpdateActivityCommand, Activity>()
            .ForMember(dest => dest.Partners, m => m.Ignore())
            .ForMember(dest => dest.Groups, m => m.Ignore())
            .ForMember(dest => dest.Participants, m => m.Ignore())
            .ForMember(dest => dest.AnonymousParticipantGroups, m => m.Ignore())
             .ForMember(dest => dest.Files, m => m.Ignore());

        CreateMap<ActivityWithProjectName, ActivityWithProjectNameQueryDto>();
    }
}
