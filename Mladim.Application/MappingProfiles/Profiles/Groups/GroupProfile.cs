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

        CreateMap<GroupCommandDto, ProjectGroup>();
        //.ForAllMembers(dest => dest.MapFrom(src => Group.Create(GroupType.Project, src.Id)));

        CreateMap<GroupCommandDto, ActivityGroup>();
            //.ForAllMembers(dest => dest.MapFrom(src => Group.Create(GroupType.Activity, src.Id)));
      
        CreateMap<ProjectGroup, GroupQueryDto>();
        CreateMap<ActivityGroup, GroupQueryDto>();

        CreateMap<UpdateGroupCommand, ProjectGroup>()
            .ForMember(dest => dest.Members, m => m.Ignore());
            //.ForMember(dest => dest.Members, m => m.MapFrom(src => Member.Create(MemberType.StaffMember, src.Id)));

        CreateMap<UpdateGroupCommand, ActivityGroup>()
              .ForMember(dest => dest.Members, m => m.Ignore());
        //.ForMember(dest => dest.Members, m => m.MapFrom(src => Member.Create(MemberType.Participant, src.Id)));


        //CreateMap<UpdateGroupCommand, ProjectGroup>(); ProjectGroup.Create()
        //CreateMap<UpdateGroupCommand, ActivityGroup>(); ActivityGroup.Create


    }
}
