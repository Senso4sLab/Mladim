using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class AnonymousParticipantCompactDto
{
    public int AnonymousParticipantId { get; set; }
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
    public int Number { get; set; }
}
