using AutoMapper;
using Mladim.Application.Features.Groups.Commands.AddGroup;
using Mladim.Application.Features.Groups.Commands.UpdateGroup;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
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
        CreateMap<Group, GroupDetailsQueryDto>();
            

        CreateMap<GroupCommandDto, Group>()
            .ForAllMembers(dest => dest.MapFrom(src => Group.Create(src.GroupType, src.Id)));      

        CreateMap<ProjectGroup, GroupQueryDto>();
        CreateMap<ActivityGroup, GroupQueryDto>();
       
        CreateMap<UpdateGroupCommand, Group>()
            .ForMember(dest => dest.Members, m => m.MapFrom(src => src.Members.ConvertAll(id => Member.Create(src.MemberType, id))));
       

    }
}
