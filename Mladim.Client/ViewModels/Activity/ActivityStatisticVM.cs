using Mladim.Client.ViewModels.Activity;
using Mladim.Client.ViewModels.Members.Participants;

namespace Mladim.Client.ViewModels;

public class ActivityStatisticVM
{
    public ActivityAttributesVM Attributes { get; set; } = default!;
    public string ProjectName { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }   
    public List<ParticipantsGenderVM> ParticipantsByGenders { get; set; } = new List<ParticipantsGenderVM>();
    public List<ParticipantsAgeGroupVM> ParticipantsByAgeGroups { get; set; } = new List<ParticipantsAgeGroupVM>();  

}
