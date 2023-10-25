using global::Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Enums;
using Mladim.Client.ViewModels.Survey;


namespace Mladim.Client.Pages;

public partial class Ankete
{
    [Parameter]
    public int ActivityId { get; set; }

    [Inject]
    public IActivityService ActivityService { get; set; } = default!;

    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    private ActivityVM? Activity;
    private AnonymousParticipantVM AnonymousParticipant = new();

    private IEnumerable<SurveyQuestionResponseVM> Survey = new List<SurveyQuestionResponseVM>();  
   
    private bool isSurveyActive = false;
    protected override async Task OnInitializedAsync()
    {
        this.Activity = await ActivityService.GetByActivityIdAsync(ActivityId);       
    } 

    private async Task OnClickAnonymousParticipant(AnonymousParticipantVM ap)
    {
        AnonymousParticipant = ap;
        isSurveyActive = true;
        Survey = await GetSurveyAsync(AnonymousParticipant.Gender);
    }


    private async Task<List<SurveyQuestionResponseVM>> GetSurveyAsync(Gender gender)
    {
        var surveyQuestions = await this.SurveyService.GetSurveyQuestionnairyAsync(this.ActivityId, gender);
        return surveyQuestions.Select(SurveyQuestionResponseVM.Create).ToList();
    }   
}