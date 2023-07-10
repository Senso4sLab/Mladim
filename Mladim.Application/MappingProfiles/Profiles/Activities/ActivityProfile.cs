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
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.Activities;

public class ActivityProfile : Profile
{
	public ActivityProfile()
	{
        CreateMap<Activity, ActivityQueryDto>();
        CreateMap<Activity, ActivityQueryDetailsDto>();
        CreateMap<AddActivityCommand, Activity>();
        CreateMap<UpdateActivityCommand, Activity>();

        CreateMap<ActivityWithProjectName, ActivityWithProjectNameQueryDto>();

        

    }
}
