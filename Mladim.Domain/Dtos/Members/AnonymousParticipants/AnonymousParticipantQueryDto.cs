using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class AnonymousParticipantQueryDto
{
    public int Number { get; set; }
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }

    public AnonymousParticipantQueryDto() { }


    private AnonymousParticipantQueryDto(int number, Gender gender, AgeGroups ageGroup) => 
        (Number, Gender, AgeGroup) = (number, gender, ageGroup);


    public static AnonymousParticipantQueryDto Create(int number, Gender gender, AgeGroups ageGroup) =>
        new AnonymousParticipantQueryDto(number, gender, ageGroup);
}
