using Mladim.Domain.Dtos.Members;

namespace Mladim.Client.ViewModels.Organization;

public class OrganizationStatisticVM
{
    public List<NamedEntityVM> ActiveProjects { get; set; } = new List<NamedEntityVM>();
    public List<NamedEntityVM> PastProjects { get; set; } = new List<NamedEntityVM>();
    public List<NamedEntityVM> ActiveActivities { get; set; } = new List<NamedEntityVM>();
    public List<NamedEntityVM> PastActivities { get; set; } = new List<NamedEntityVM>();
    public int IndividualParticipants { get; set; }
    public int AnonymousParticipants { get; set; }
}
