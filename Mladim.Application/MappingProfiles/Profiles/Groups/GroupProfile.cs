using AutoMapper;
using Mladim.Application.Features.Groups.Commands.AddGroup;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.Groups;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<GroupDetailsQueryDto, Group>()
            .ReverseMap();

        CreateMap<GroupCommandDto, ProjectGroup>();
        CreateMap<GroupCommandDto, ActivityGroup>();

        CreateMap<ProjectGroup, GroupQueryDto>();
        CreateMap<ActivityGroup, GroupQueryDto>();

        //CreateMap<AddOrganizationCommandDto, OrganizationGroup>()

    }
}
