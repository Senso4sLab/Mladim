using global::Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Enums;
using Mladim.Client.ViewModels.Survey;
using Mladim.Client.Validators;
using Mladim.Client.ViewModels.Activity;
using System.ComponentModel.DataAnnotations;

namespace Mladim.Client.Pages;

public partial class Survey
{
    [Parameter]
    public int ActivityId { get; set; }

    [Inject]
    public IActivityService ActivityService { get; set; } = default!;

    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;    


    private string? activityName;
    private bool showSurvey = false;

    private AnonymousParticipantVM AnonymousParticipant = new();   
    private List<SurveyQuestionResponseVM> SurveyResponse { get; set; } = new List<SurveyQuestionResponseVM>();
    
    protected override async Task OnInitializedAsync()
    {
        activityName = await ActivityService.GetActivityNameAsync(ActivityId);
    }

    private async Task OnClickAnonymousParticipant(AnonymousParticipantVM ap)
    {
        AnonymousParticipant = ap;
        showSurvey = true;
        SurveyResponse = await GetSurveyAsync(AnonymousParticipant.Gender);
    }
    private async Task<List<SurveyQuestionResponseVM>> GetSurveyAsync(Gender gender)
    {
        var surveyQuestions = await this.SurveyService.GetSurveyQuestionnairyAsync(this.ActivityId, gender);
        return surveyQuestions.Select(SurveyQuestionResponseVM.Create).ToList();
    }   

    private async Task SurveyValidSubmit()
    {
        var succeed = await SurveyService.PostAnonymousSurveyResponseAsync(this.ActivityId, AnonymousSurveyResponseVM.Create(AnonymousParticipant, SurveyResponse.Select(sr => sr.Response)));
        this.NavigationManager.NavigateTo("/survey/end");
    }   
}