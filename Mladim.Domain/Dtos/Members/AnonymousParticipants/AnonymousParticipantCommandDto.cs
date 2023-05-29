using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class AnonymousParticipantCommandDto
{
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
    public int Number { get; set; }
}

