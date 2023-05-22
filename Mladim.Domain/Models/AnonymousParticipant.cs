using Mladim.Domain.Enums;

namespace Mladim.Domain.Models;

public class AnonymousParticipant
{
    public int Id { get; set; }
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
    public List<AnonymousParticipantActivity> AnonymousParticipants { get; set; } = new();

}
