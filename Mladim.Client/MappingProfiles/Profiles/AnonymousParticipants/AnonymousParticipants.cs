using AutoMapper;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;

namespace Mladim.Client.MappingProfiles.Profiles.AnonymousParticipants;

public class AnonymousParticipants : Profile
{
    public AnonymousParticipants()
    {
        CreateMap<AnonymousParticipantGroupQueryDto, AnonymousParticipantGroupVM>();

        CreateMap<AnonymousParticipantGroupVM, AnonymousParticipantGroupCommandDto>();

    }
}
