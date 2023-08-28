

using Mladim.Client.Extensions;
using Mladim.Client.Models;
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

    private IEnumerable<DoughnutPiece> genderDoughnut = null!;
    public IEnumerable<DoughnutPiece> GenderDoughnut => genderDoughnut ??=
        ParticipantsByGenders.Select(pg => DoughnutPiece.Create(pg.Gender.GetDisplayAttribute(), pg.Number))
            .ToList();


    private IEnumerable<DoughnutPiece> ageDoughnut = null!;
    public IEnumerable<DoughnutPiece> AgeDoughnut => ageDoughnut ??=
      ParticipantsByAgeGroups.Select(pa => DoughnutPiece.Create(pa.AgeGroup.GetDisplayAttribute(), pa.Number))
            .ToList();
    
}
