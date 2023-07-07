using Mladim.Domain.Enums;

namespace Mladim.Domain.Models;

public class AnonymousParticipant
{
    //public int Id { get; set; }
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
    //public List<AnonymousParticipantActivity> AnonymousParticipantActivities { get; set; } = new();

  
    public AnonymousParticipant(Gender gender, AgeGroups ageGroups)
    {
        this.Gender = gender;
        this.AgeGroup = ageGroups;
    }

    public override bool Equals(object? obj) =>
        obj is AnonymousParticipant anonymousParticipant ?
            this.Equals(anonymousParticipant) : false;

       
    private bool Equals(AnonymousParticipant anonymousParticipant) =>
        anonymousParticipant.AgeGroup == this.AgeGroup &&
        anonymousParticipant.Gender == this.Gender;

    public override int GetHashCode() =>
        HashCode.Combine(this.Gender, this.AgeGroup);    

}


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

    public AnonymousParticipantGroup CreateWithNumber(int number) =>
        new AnonymousParticipantGroup(number, AnonymousParticipant);
}
