using global::Microsoft.AspNetCore.Components;
using Mladim.Client.Models;
using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Enums;

namespace Mladim.Client.Components.ActivitiesResults;

public partial class ActivityResultChart
{  
    [Parameter]
    public IEnumerable<SurveyResponsesGroupedByQuestionVM> SurveyResponsesGroupByQuestions { get; set; } = new List<SurveyResponsesGroupedByQuestionVM>();
    
    protected async override Task OnInitializedAsync()
    {
       
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
    //public IEnumerable<DoughnutPiece> AgeDoughnut => ageDoughnut ??= AgeGroupsDoughnutPercantages();

}