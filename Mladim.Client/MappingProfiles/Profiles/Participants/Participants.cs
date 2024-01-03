using AutoMapper;
using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Members.Participants;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Members.Participants;
using Mladim.Domain.Dtos.Project;

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

        CreateMap<ParticipantsGenderDto, ParticipantsGenderVM>();

        CreateMap<ParticipantsAgeGroupDto, ParticipantsAgeGroupVM>();


    }
}
