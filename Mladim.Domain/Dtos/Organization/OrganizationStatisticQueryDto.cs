using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Dtos.Members.Participants;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Organization;

public class OrganizationStatisticQueryDto
{
    public List<NamedEntityDto> ActiveProjects { get; set; } = new List<NamedEntityDto>();
    public List<NamedEntityDto> PastProjects { get; set; } = new List<NamedEntityDto>();
    public List<NamedEntityDto> ActiveActivities { get; set; } = new List<NamedEntityDto>();
    public List<NamedEntityDto> PastActivities { get; set; } = new List<NamedEntityDto>();
    public List<ParticipantsGenderDto> ParticipantsByGenders { get; set; } = new List<ParticipantsGenderDto>();
    public List<ParticipantsAgeGroupDto> ParticipantsByAgeGroups { get; set; } = new List<ParticipantsAgeGroupDto>();

    public int IndividualParticipants { get; set; }
    public int AnonymousParticipants { get; set; }

    public OrganizationStatisticQueryDto()
    {
        
    }
    public OrganizationStatisticQueryDto(IEnumerable<ParticipantsGenderDto> participantsGender, IEnumerable<ParticipantsAgeGroupDto> participantAgeGroup)
    {
        AddRange(participantsGender);
        AddRange(participantAgeGroup);
    }


    public static OrganizationStatisticQueryDto Empty =>
        new OrganizationStatisticQueryDto(new List<ParticipantsGenderDto>(), new List<ParticipantsAgeGroupDto>());


    public static OrganizationStatisticQueryDto Create(List<NamedEntityDto> activeProjects, List<NamedEntityDto> pastProjects,
        List<NamedEntityDto> activeActivities, List<NamedEntityDto> pastActivities, int individualParticipants, int anonymousParticipants, 
        IEnumerable<ParticipantsGenderDto> participantsGender, IEnumerable<ParticipantsAgeGroupDto> participantAgeGroup) =>
        new OrganizationStatisticQueryDto(participantsGender, participantAgeGroup)
        {            
            ActiveProjects = activeProjects,
            PastProjects = pastProjects,
            ActiveActivities = activeActivities, 
            PastActivities = pastActivities,
            IndividualParticipants = individualParticipants,
            AnonymousParticipants = anonymousParticipants,           
        };


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
}
