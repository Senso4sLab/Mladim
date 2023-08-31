using Mladim.Domain.Dtos.DateTimeRange;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Project;

public class ProjectStatisticsQueryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int TotalActivities { get; set; }
    public DateTimeRangeQueryDto TimeRange { get; set; } = default!;

    public List<ParticipantsGenderDto> ParticipantsByGenders { get; set; } = new List<ParticipantsGenderDto>();

    public List<ParticipantsAgeGroupDto> ParticipantsByAgeGroups { get; set; } = new List<ParticipantsAgeGroupDto>();
    
    public ProjectStatisticsQueryDto()
    {
        
    }

    public void AddRange(IEnumerable<ParticipantsGenderDto> participantGenders)
    {
        foreach (var participantGender in participantGenders)
        {
            var pg = ParticipantsByGenders.FirstOrDefault(p => p.Equals(participantGender));

            if (pg != null)
                pg.Number += participantGender.Number;
            else
                ParticipantsByGenders.Add(participantGender);
        }       
    }

    public void AddRange(IEnumerable<ParticipantsAgeGroupDto> participantAgeGroups)
    {

        foreach (var participantAgeGroup in participantAgeGroups)
        {
            var pag = ParticipantsByAgeGroups.FirstOrDefault(p => p.Equals(participantAgeGroup));

            if (pag != null)
                pag.Number += participantAgeGroup.Number;
            else
                ParticipantsByAgeGroups.Add(participantAgeGroup);
        }
    }


    public static ProjectStatisticsQueryDto Create(int id, string name, DateTimeRangeQueryDto timeRange, IEnumerable<ParticipantsGenderDto> participantsByGenders, IEnumerable<ParticipantsAgeGroupDto> participantsByAgeGroups, int totalActivity)
    {
        var ps = new ProjectStatisticsQueryDto() { Id = id, Name = name, TimeRange = timeRange, TotalActivities = totalActivity, };        
        ps.AddRange(participantsByGenders);
        ps.AddRange(participantsByAgeGroups);
        return ps;
    }
       

}

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



