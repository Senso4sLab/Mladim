using AutoMapper;
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
        CreateMap<GroupCommandDto, ProjectGroup>();
       
        CreateMap<GroupDetailsQueryDto, Group>().ReverseMap();
       // CreateMap<GroupCommandDto, Group>();
        CreateMap<ProjectGroup, GroupQueryDto>();
    }
}
