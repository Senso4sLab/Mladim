using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class AnonymousParticipantQueryDto
{
    public int Number { get; set; }
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
}
