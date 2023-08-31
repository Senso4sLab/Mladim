

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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


  
    public int TotalParticipants
    {
        get
        {
            var totalByGender = ParticipantsByGenders.Sum(p => p.Number);
            var totalByAgeGroups = ParticipantsByAgeGroups.Sum(p => p.Number);
            return totalByGender == totalByAgeGroups ? totalByGender : 0;               
        }        
    }
   

    private IEnumerable<DoughnutPiece> genderDoughnut = null!;
    public IEnumerable<DoughnutPiece> GenderDoughnut => genderDoughnut ??= GenderDoughnutPercantages();



    private IEnumerable<DoughnutPiece> GenderDoughnutPercantages()
    {
        int total = this.TotalParticipants;

        return total == 0 ? Enumerable.Empty<DoughnutPiece>() :
             ParticipantsByGenders.Select(pg => (percentage: Math.Round(pg.Number * 100.0 / total), pg: pg))
             .Select(tuple => DoughnutPiece.Create(tuple.pg.Gender.GetDisplayAttribute(), (int)tuple.percentage, $"{tuple.percentage}%"))  //    pg.Gender.GetDisplayAttribute(), pg.Number, $"{Math.Round(pg.Number * 100.0 / total)}%"))
             .ToList();
    }


    private IEnumerable<DoughnutPiece> ageDoughnut = null!;
    public IEnumerable<DoughnutPiece> AgeDoughnut => ageDoughnut ??= AgeGroupsDoughnutPercantages();




    private IEnumerable<DoughnutPiece> AgeGroupsDoughnutPercantages()
    {
        int total = this.TotalParticipants;

        return total == 0 ? Enumerable.Empty<DoughnutPiece>() :
              ParticipantsByAgeGroups.Select(pg => (percentage: Math.Round(pg.Number * 100.0 / total), pg: pg))
             .Select(tuple => DoughnutPiece.Create(tuple.pg.AgeGroup.GetDisplayAttribute(), (int)tuple.percentage, $"{tuple.percentage}%"))  // pg.Gender.GetDisplayAttribute(), pg.Number, $"{Math.Round(pg.Number * 100.0 / total)}%"))
             .ToList();
    }    

}
