using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels;

public class AnonymousParticipantGroupVM
{
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
    public int Number { get; set; }

    public override bool Equals(object? obj) =>
        obj is AnonymousParticipantGroupVM mb &&
        mb.Gender == this.Gender &&
        mb.AgeGroup == this.AgeGroup;


    public override int GetHashCode() =>
        HashCode.Combine(this.AgeGroup, this.Gender);
}

