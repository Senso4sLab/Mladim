using AutoMapper;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;

namespace Mladim.Application.MappingProfiles.Converters;

public class AnonymousParticipantActivityToCompactDto : ITypeConverter<AnonymousParticipantActivity, AnonymousParticipantDetailsQueryDto>
{
    public AnonymousParticipantDetailsQueryDto Convert(AnonymousParticipantActivity source, AnonymousParticipantDetailsQueryDto destination, ResolutionContext context) =>
        new AnonymousParticipantDetailsQueryDto
        {
            AgeGroup = source.AnonymousParticipant.AgeGroup,
            Gender = source.AnonymousParticipant.Gender,
            Number = source.Number,
        };

}




