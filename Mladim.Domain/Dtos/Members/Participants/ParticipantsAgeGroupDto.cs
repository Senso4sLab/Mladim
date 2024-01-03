using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Members.Participants;

public class ParticipantsAgeGroupDto
{
    public AgeGroups AgeGroup { get; set; }
    public int Number { get; set; }

    public ParticipantsAgeGroupDto()
    {

    }

    public override bool Equals(object? obj) =>
       obj is ParticipantsAgeGroupDto participantsAgeGroup && this.Equals(participantsAgeGroup);

    private bool Equals(ParticipantsAgeGroupDto participantsAgeGroup) =>
        participantsAgeGroup.AgeGroup == this.AgeGroup;

    public override int GetHashCode() =>
        HashCode.Combine(this.AgeGroup);


    public static ParticipantsAgeGroupDto Create(AgeGroups ageGroup, int number = 1) =>
       new ParticipantsAgeGroupDto
       {
           AgeGroup = ageGroup,
           Number = number,
       };

}
