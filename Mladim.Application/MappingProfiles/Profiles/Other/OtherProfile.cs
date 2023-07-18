using AutoMapper;
using Mladim.Application.Features.Accounts.Commands.UpdateAppUser;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.Other;

public class OtherProfile : Profile
{
	public OtherProfile()
	{

        CreateMap<UpdateAppUserCommand, AppUser>();
        CreateMap<AppUser, AppUserQueryDto>();

        CreateMap<SocialMediaUrlsCommandDto, SocialMediaUrls>();
        CreateMap<SocialMediaUrls, SocialMediaUrlsQueryDto>();

        CreateMap<DateTimeRangeCommandDto, DateTimeRange>();

        CreateMap<DateTimeRangeQueryDto, DateTimeRange>()
            .ReverseMap();

        CreateMap<ProjectAttributesCommandDto, ProjectAttibutes>();
        CreateMap<ProjectAttibutes, ProjectAttributesQueryDto>();

        CreateMap<ActivityAttributesCommandDto, ActivityAttributes>();
        CreateMap<ActivityAttributes, ActivityAttributesQueryDto>();

        CreateMap<OrganizationAttributesCommandDto, OrganizationAttributes>();
        CreateMap<OrganizationAttributes, OrganizationAttributesQueryDto>();

    }
}
