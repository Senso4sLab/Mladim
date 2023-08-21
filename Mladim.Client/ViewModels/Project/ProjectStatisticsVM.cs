

using Mladim.Client.ViewModels.Members.Participants;

namespace Mladim.Client.ViewModels.Project;

public class ProjectStatisticsVM
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int TotalActivities { get; set; }
    public DateTimeRangeVM TimeRange { get; private set; } = default!;

    public List<ParticipantsGenderVM> ParticipantsByGenders = new List<ParticipantsGenderVM>();

    public List<ParticipantsAgeGroupVM> ParticipantsByAgeGroups = new List<ParticipantsAgeGroupVM>();
    public int TotalParticipants { get; set; }
}
