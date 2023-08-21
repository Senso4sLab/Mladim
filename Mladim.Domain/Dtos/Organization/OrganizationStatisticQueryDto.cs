using Mladim.Domain.Dtos.Members;
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
    
    public int IndividualParticipants { get; set; }
    public int AnonymousParticipants { get; set; }

    public OrganizationStatisticQueryDto() { }


    public static OrganizationStatisticQueryDto Empty =>
        new OrganizationStatisticQueryDto();


    public static OrganizationStatisticQueryDto Create(List<NamedEntityDto> activeProjects, List<NamedEntityDto> pastProjects,
        List<NamedEntityDto> activeActivities, List<NamedEntityDto> pastActivities, int individualParticipants, int anonymousParticipants) =>
        new OrganizationStatisticQueryDto()
        {            
            ActiveProjects = activeProjects,
            PastProjects = pastProjects,
            ActiveActivities = activeActivities, 
            PastActivities = pastActivities,
            IndividualParticipants = individualParticipants,
            AnonymousParticipants = anonymousParticipants,
        };


}
