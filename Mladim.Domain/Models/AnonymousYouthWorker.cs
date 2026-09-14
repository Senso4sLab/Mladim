using Mladim.Domain.Enums;

namespace Mladim.Domain.Models;

public class AnonymousYouthWorker
{
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
    public YouthWorkRole Role { get; set; } 
    public YearsOfExperienceInYouthWork YearsOfExperience { get; set; }

    private AnonymousYouthWorker() { }

    public AnonymousYouthWorker(Gender gender, AgeGroups ageGroup, YouthWorkRole role, YearsOfExperienceInYouthWork yearsOfExperience) =>
        (Gender, AgeGroup, Role, YearsOfExperience) = (gender, ageGroup, role, yearsOfExperience);

    public static AnonymousYouthWorker Create(Gender gender, AgeGroups ageGroup, YouthWorkRole role, YearsOfExperienceInYouthWork yearsOfExperience) =>
        new(gender, ageGroup, role, yearsOfExperience);
      
}
