using AutoMapper;
using Mladim.Application.MappingProfiles.Converters;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.AnonymousParticipants;

public class AnonymousParticipantProfile : Profile
{
	public AnonymousParticipantProfile()
	{
        CreateMap<AnonymousParticipantGroup, AnonymousParticipantQueryDto>()
            .ForMember(dest => dest.Number, m => m.MapFrom(src => src.Number))
            .ForAllMembers(dest => dest.MapFrom(src => src.AnonymousParticipant));
           

        CreateMap<AnonymousParticipantCommandDto, AnonymousParticipant>();
        CreateMap<AnonymousParticipantCommandDto, AnonymousParticipantGroup>()
            .ForMember(dest => dest.Number, m => m.MapFrom(src => src.Number))
            .ForMember(dest => dest.AnonymousParticipant, m => m.MapFrom(src => src));
       
    }
}
