using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class AnonymousParticipantDto
{
    public int Id { get; set; }
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
}
