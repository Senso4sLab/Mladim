using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels;

public class AnonymousParticipantVM
{
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }   

    public override bool Equals(object? obj) =>
        obj is AnonymousParticipantVM mb &&
        mb.Gender == this.Gender &&
        mb.AgeGroup == this.AgeGroup;
    public override int GetHashCode() =>
        HashCode.Combine(this.AgeGroup, this.Gender);
}


