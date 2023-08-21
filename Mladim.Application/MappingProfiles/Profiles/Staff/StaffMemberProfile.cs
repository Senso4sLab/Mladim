using AutoMapper;
using Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
using Mladim.Application.Features.Members.StaffMembers.Commands.UpdateStaffMember;

using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.Staff;

public class StaffMemberProfile : Profile
{
    public StaffMemberProfile()
    {
        CreateMap<StaffMember, StaffMemberDetailsQueryDto>()
            .ReverseMap();

        CreateMap<AddStaffMemberCommand, StaffMember>();
        CreateMap<UpdateStaffMemberCommand, StaffMember>();     


    
        CreateMap<StaffMemberLeadQuery, StaffMemberLeadQueryDto>();

        CreateMap<StaffMemberCommandDto, StaffMemberProject>()
            .ForMember(dest => dest.StaffMemberId, m => m.MapFrom(src => src.Id));

        CreateMap<StaffMemberProject, StaffMemberQueryDto>()
            .ForMember(dest => dest.Id, m => m.MapFrom(src => src.StaffMemberId))
            .ForMember(dest => dest.FullName, m => m.MapFrom(src => src.StaffMember.FullName));

        CreateMap<StaffMemberCommandDto, StaffMemberActivity>()
            .ForMember(dest => dest.StaffMemberId, m => m.MapFrom(src => src.Id));

        CreateMap<StaffMemberActivity, StaffMemberQueryDto>()
            .ForMember(dest => dest.Id, m => m.MapFrom(src => src.StaffMemberId))
            .ForMember(dest => dest.FullName, m => m.MapFrom(src => src.StaffMember.FullName));

    }
}
