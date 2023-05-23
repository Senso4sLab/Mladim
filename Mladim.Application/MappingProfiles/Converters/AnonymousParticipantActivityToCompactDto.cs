using AutoMapper;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;

namespace Mladim.Application.MappingProfiles.Converters;

public class AnonymousParticipantActivityToCompactDto : ITypeConverter<AnonymousParticipantActivity, AnonymousParticipantCompactDto>
{
    public AnonymousParticipantCompactDto Convert(AnonymousParticipantActivity source, AnonymousParticipantCompactDto destination, ResolutionContext context) =>
        new AnonymousParticipantCompactDto
        {
            AgeGroup = source.AnonymousParticipant.AgeGroup,
            Gender = source.AnonymousParticipant.Gender,
            Number = source.Number,
            AnonymousParticipantId = source.AnonymousParticipantId
        };
   
}




