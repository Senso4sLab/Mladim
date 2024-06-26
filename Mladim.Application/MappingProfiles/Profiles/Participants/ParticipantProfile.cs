﻿using AutoMapper;
using Mladim.Application.Features.Members.Participants.Commands.AddParticipant;
using Mladim.Application.Features.Members.Participants.Commands.UpdateParticipant;

using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.Participants;

public class ParticipantProfile : Profile
{
	public ParticipantProfile()
	{
        CreateMap<AddParticipantCommand, Participant>();       

        CreateMap<UpdateParticipantCommand, Participant>();

        CreateMap<Participant, ParticipantDetailsQueryDto>()
            .ReverseMap();

        CreateMap<ParticipantCommandDto, Participant>();

        CreateMap<Participant, ParticipantQueryDto>();
    }
}
