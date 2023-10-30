﻿using global::Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Enums;
using Mladim.Client.ViewModels.Survey;
using Mladim.Client.Validators;

namespace Mladim.Client.Pages;

public partial class Ankete
{
    [Parameter]
    public int ActivityId { get; set; }

    [Inject]
    public IActivityService ActivityService { get; set; } = default!;

    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; }    


    private ActivityVM? Activity;
    private AnonymousParticipantVM AnonymousParticipant = new();
    

    private List<SurveyQuestionResponseVM> Survey = new List<SurveyQuestionResponseVM>();  
   
    private bool isSurveyActive = false;
    //protected override async Task OnInitializedAsync()
    //{
    //    this.Activity = await ActivityService.GetActivityNameAsync(ActivityId);       
    //} 

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


    private void SurveyEndClick()
    {
        this.NavigationManager.NavigateTo("/survey/end");
    }
}