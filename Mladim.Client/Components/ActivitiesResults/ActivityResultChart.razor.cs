using global::Microsoft.AspNetCore.Components;
using Mladim.Client.Models;
using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;
using static MudBlazor.Colors;

namespace Mladim.Client.Components.ActivitiesResults;

public partial class ActivityResultChart
{  
    [Parameter]
    public IEnumerable<SurveyResponsesGroupedByQuestionVM> SurveyResponsesGroupByQuestions { get; set; } = new List<SurveyResponsesGroupedByQuestionVM>();

    private IEnumerable<DoughnutPiece> ageDoughnut = new List<DoughnutPiece>();

    private IEnumerable<DoughnutPiece> genderDoughnut = new List<DoughnutPiece>();
            
    protected override void OnInitialized()
    {
        var participants = GetParticipantsFromSurveyResponse();
        ageDoughnut    = AgeGroupDoughnutPercantages(participants, participants.Count).ToList();
        genderDoughnut = GenderDoughnutPercantages(participants, participants.Count).ToList();        
    }

    private List<AnonymousParticipantVM> GetParticipantsFromSurveyResponse() =>    
        this.SurveyResponsesGroupByQuestions.FirstOrDefault()?
           .Participants.ToList() ?? new List<AnonymousParticipantVM>();    

    private IEnumerable<DoughnutPiece> GenderDoughnutPercantages(List<AnonymousParticipantVM> participants, int total)
    {        
       return participants.GroupBy(ap => ap.Gender)
            .Select(g => (name: g.Key, value: Math.Round(g.Count() * 100.0 / total)))
            .Select(tuple => DoughnutPiece.Create(tuple.name.GetDisplayAttribute(), (int)tuple.value,$"{tuple.value}%", GenderColor(tuple.name)))
            .ToList();        
    }

    private IEnumerable<DoughnutPiece> AgeGroupDoughnutPercantages(List<AnonymousParticipantVM> participants, int total)
    {
        return participants.GroupBy(ap => ap.AgeGroup)
             .Select(g => (name: g.Key, value: Math.Round(g.Count() * 100.0 / participants.Count)))
             .Select(tuple => DoughnutPiece.Create(tuple.name.GetDisplayAttribute(), (int)tuple.value, $"{tuple.value}%", AgeGroupColor(tuple.name)))
             .ToList();
    }

    //public List<SurveyBoleanResponsesGroupedByQuestion> BoleanResponses { get; set; } = new List<SurveyBoleanResponsesGroupedByQuestion>(); 
    //private void Test()
    //{
    //    foreach(var responsesGroup in this.SurveyResponsesGroupByQuestions)
    //    {
    //        if(responsesGroup is SurveyBoleanResponsesGroupedByQuestion boleanResponse)
    //        {
    //            var selectable = boleanResponse as ISelectableQuestionReponseCalculator;
    //            var response = selectable.ParticipantsByEnumTypes(ParticipantPredicate.None.Predicate).Select(s => s.InPercent(total));
    //            this.BoleanResponses.Add(boleanResponse);

    //        }
    //    }    
    //}

    private string GenderColor(Gender gender) => 
        gender switch
        {
            Gender.Male => "#4da456",
            Gender.Female => "#ffc700",
            Gender.Undefined => "8ed974",
            Gender.Other => "#394241",
            _ => throw new NotImplementedException(),
        };
    private string AgeGroupColor(AgeGroups age) =>
        age switch
        {
            AgeGroups.Age12_14 => "#4da456",
            AgeGroups.Age15_19 => "#ffc700",
            AgeGroups.Age20_24 => "#8ed974",
            AgeGroups.Age25_29 => "#394241",
            _ => throw new NotImplementedException(),
        };



   

}