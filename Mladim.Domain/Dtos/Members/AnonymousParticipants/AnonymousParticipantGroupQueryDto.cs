using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class AnonymousParticipantGroupQueryDto
{
    public int Number { get; set; }
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }

    public AnonymousParticipantGroupQueryDto() { }


    private AnonymousParticipantGroupQueryDto(int number, Gender gender, AgeGroups ageGroup) => 
        (Number, Gender, AgeGroup) = (number, gender, ageGroup);


    public static AnonymousParticipantGroupQueryDto Create(int number, Gender gender, AgeGroups ageGroup) =>
        new AnonymousParticipantGroupQueryDto(number, gender, ageGroup);
}
