

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mladim.Client.Extensions;
using Mladim.Client.Models;
using Mladim.Client.ViewModels.Members.Participants;
using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;

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
             .Select(tuple => DoughnutPiece.Create(tuple.pg.Gender.GetDisplayAttribute(), (int)tuple.percentage, $"{tuple.percentage}%", GenderColor(tuple.pg.Gender)))  //    pg.Gender.GetDisplayAttribute(), pg.Number, $"{Math.Round(pg.Number * 100.0 / total)}%"))
             .ToList();
    }



    private string GenderColor(Gender gender) => gender switch
    {
        Gender.Male => "#4da456",
        Gender.Female => "#ffc700",
        Gender.Undefined => "8ed974",
        Gender.Other => "#394241",
    };



    private string AgeGroupColor(AgeGroups age) => age switch
    {
        AgeGroups.Age12_14 => "#4da456",
        AgeGroups.Age15_19 => "#ffc700",
        AgeGroups.Age20_24 => "#8ed974",
        AgeGroups.Age25_29 => "#394241",       
        _ => "#7cc769",
    };



    private IEnumerable<DoughnutPiece> ageDoughnut = null!;
    public IEnumerable<DoughnutPiece> AgeDoughnut => ageDoughnut ??= AgeGroupsDoughnutPercantages();




    private IEnumerable<DoughnutPiece> AgeGroupsDoughnutPercantages()
    {
        int total = this.TotalParticipants;

        return total == 0 ? Enumerable.Empty<DoughnutPiece>() :
              ParticipantsByAgeGroups.Select(pg => (percentage: Math.Round(pg.Number * 100.0 / total), pg: pg))
             .Select(tuple => DoughnutPiece.Create(tuple.pg.AgeGroup.GetDisplayAttribute(), (int)tuple.percentage, $"{tuple.percentage}%", AgeGroupColor(tuple.pg.AgeGroup)))  // pg.Gender.GetDisplayAttribute(), pg.Number, $"{Math.Round(pg.Number * 100.0 / total)}%"))
             .ToList();
    }    

}
