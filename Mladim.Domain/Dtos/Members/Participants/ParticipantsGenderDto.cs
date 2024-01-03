using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos.Members.Participants;

public class ParticipantsGenderDto
{
    public Gender Gender { get; set; }
    public int Number { get; set; }


    public ParticipantsGenderDto()
    {

    }

    public override bool Equals(object? obj) =>
        obj is ParticipantsGenderDto participantGenderDto && this.Equals(participantGenderDto);

    private bool Equals(ParticipantsGenderDto participantGenderDto) =>
        participantGenderDto.Gender == this.Gender;

    public override int GetHashCode() =>
        HashCode.Combine(this.Gender);

    public static ParticipantsGenderDto Create(Gender gender, int number = 1) =>
        new ParticipantsGenderDto
        {
            Gender = gender,
            Number = number,
        };

}
