using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;
using Mladim.Domain.Models.Survey.Responses;

namespace Mladim.Domain.Models;

public class AnonymousParticipant
{
    public int Id { get; set; } 
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
    //public List<SurveyQuestionnairyResponse> SurveyQuestionnairyResponse { get; set; } = new();

    private AnonymousParticipant()
    {
        
    }
    private AnonymousParticipant(Gender gender, AgeGroups ageGroups) =>
        (Gender, AgeGroup) = (gender, ageGroups);    

    public static AnonymousParticipant Create(Gender gender, AgeGroups ageGroups) =>
        new AnonymousParticipant(gender, ageGroups);

    public override bool Equals(object? obj) =>
        obj is AnonymousParticipant anonymousParticipant ?
            this.Equals(anonymousParticipant) : false;
       
    private bool Equals(AnonymousParticipant anonymousParticipant) =>
        anonymousParticipant.AgeGroup == this.AgeGroup &&
        anonymousParticipant.Gender == this.Gender;

    public override int GetHashCode() =>
        HashCode.Combine(this.Gender, this.AgeGroup);


    public override string ToString() =>
        $"{this.Gender.GetDisplayAttribute()}-{this.AgeGroup.GetDisplayAttribute()}";
   

}
