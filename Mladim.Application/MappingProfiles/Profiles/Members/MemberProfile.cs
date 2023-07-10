﻿using AutoMapper;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.Members;

public class MemberProfile : Profile
{
	public MemberProfile()
	{
        CreateMap<MemberDto, Member>()
            .Include<StaffMemberDetailsQueryDto, StaffMember>()
            .Include<ParticipantDetailsQueryDto, Participant>();

        CreateMap<Member, MemberDto>()
            .Include<StaffMember, StaffMemberDetailsQueryDto>()
            .Include<Participant, ParticipantDetailsQueryDto>();
    }
}
