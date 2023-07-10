using AutoMapper;
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
        CreateMap<StaffMemberCommandDto, StaffMemberProject>();
        CreateMap<StaffMemberProject, StaffMemberQueryDto>()
            .ForMember(dto => dto.Name, m => m.MapFrom(sm => sm.StaffMember.FullName))
            .ForMember(dto => dto.Surname, m => m.MapFrom(sm => sm.StaffMember.Surname));
    }
}
