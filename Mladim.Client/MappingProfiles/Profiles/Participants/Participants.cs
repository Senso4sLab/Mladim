using AutoMapper;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;

namespace Mladim.Client.MappingProfiles.Profiles.Participants;

public class Participants : Profile
{
    public Participants()
    {
        CreateMap<NamedEntityVM, ParticipantCommandDto>();
        CreateMap<ParticipantQueryDto, NamedEntityVM>();

        CreateMap<ParticipantDetailsQueryDto, ParticipantVM>();
        CreateMap<ParticipantVM, AddParticipantCommandDto>();
        CreateMap<ParticipantVM, UpdateParticipantCommandDto>();
    }
}
