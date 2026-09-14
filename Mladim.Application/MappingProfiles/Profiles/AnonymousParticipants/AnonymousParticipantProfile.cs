using AutoMapper;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Members.AnonymousParticipants;
using Mladim.Domain.Models;

namespace Mladim.Application.MappingProfiles.Profiles.AnonymousParticipants;

public class AnonymousParticipantProfile : Profile
{
	public AnonymousParticipantProfile()
	{
        CreateMap<AnonymousParticipantGroup, AnonymousParticipantGroupQueryDto>()
             .ForMember(dest => dest.Number, m => m.MapFrom(src => src.Number))
             .ForMember(dest => dest.AgeGroup, m => m.MapFrom(src => src.AnonymousParticipant.AgeGroup))
             .ForMember(dest => dest.Gender, m => m.MapFrom(src => src.AnonymousParticipant.Gender));

        CreateMap<AnonymousParticipantGroupCommandDto, AnonymousParticipant>();

        CreateMap<AnonymousParticipantGroupCommandDto, AnonymousParticipantGroup>()
            .ForMember(dest => dest.Number, m => m.MapFrom(src => src.Number))
            .ForMember(dest => dest.AnonymousParticipant, m => m.MapFrom(src => src));




        CreateMap<AnonymousParticipantCommandDto, AnonymousParticipant>().ReverseMap();
        CreateMap<AnonymousYouthWorkerCommandDto, AnonymousYouthWorker>().ReverseMap();



        //CreateMap<AddAnonymousParticipantCommand, AnonymousParticipant>();        
        //CreateMap<AddAnonymousParticipantCommand, AnonymousParticipantGroup>()
        //    .ForMember(dest => dest.Number, m => m.MapFrom(src => src.Number))
        //    .ForMember(dest => dest.AnonymousParticipant, m => m.MapFrom(src => src));

    }
}
