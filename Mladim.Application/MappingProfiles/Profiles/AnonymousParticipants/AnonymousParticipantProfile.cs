using AutoMapper;
using Mladim.Application.Features.Members.AnonymousParticipants.Commands.AddAnonymousParticipant;
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
             .ForMember(dest => dest.AgeGroup, m => m.MapFrom(src => src.AnonymousParticipant.AgeGroup))
             .ForMember(dest => dest.Gender, m => m.MapFrom(src => src.AnonymousParticipant.Gender));

        CreateMap<AnonymousParticipantCommandDto, AnonymousParticipant>();

        CreateMap<AnonymousParticipantCommandDto, AnonymousParticipantGroup>()
            .ForMember(dest => dest.Number, m => m.MapFrom(src => src.Number))
            .ForMember(dest => dest.AnonymousParticipant, m => m.MapFrom(src => src));

        
        CreateMap<AddAnonymousParticipantCommand, AnonymousParticipant>();        
        CreateMap<AddAnonymousParticipantCommand, AnonymousParticipantGroup>()
            .ForMember(dest => dest.Number, m => m.MapFrom(src => src.Number))
            .ForMember(dest => dest.AnonymousParticipant, m => m.MapFrom(src => src));

    }
}
