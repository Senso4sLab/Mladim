using Mladim.Domain.Enums;

namespace Mladim.Domain.Models;

public class AnonymousParticipantGroup
{
    public int Number { get; set; }
    public AnonymousParticipant AnonymousParticipant { get; set; }

    public AnonymousParticipantGroup(int number, AnonymousParticipant anonymousParticipant)
    {
        this.Number = number;
        this.AnonymousParticipant = anonymousParticipant;   
    }
    public override bool Equals(object? obj) =>
        obj is AnonymousParticipantGroup anonymousParticipantGroup ?
            this.Equals(anonymousParticipantGroup) : false;

    private bool Equals(AnonymousParticipantGroup anonymousParticipantGroup) =>
       anonymousParticipantGroup.Number == this.Number &&
       anonymousParticipantGroup.AnonymousParticipant.Equals(this.AnonymousParticipant);

    public override int GetHashCode() =>
        HashCode.Combine(this.Number, this.AnonymousParticipant);

    public static AnonymousParticipantGroup Create(int number, Gender gender, AgeGroups ageGroups) =>
        new AnonymousParticipantGroup(number, AnonymousParticipant.Create(gender, ageGroups));
}
