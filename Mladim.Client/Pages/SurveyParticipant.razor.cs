using Microsoft.AspNetCore.Components;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Survey;

namespace Mladim.Client.Pages
{
    public partial class SurveyParticipant
    {

        [Parameter]
        public int ActivityId { get; set; }

        [Inject]
        public IActivityService ActivityService { get; set; } = default!;

        [Inject]
        public ISurveyService SurveyService { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        private ActivitySummaryVM ActivitySummary { get; set; }
        private SurveyParticipantVM Participant { get; set; }

        private List<SurveyQuestionResponseVM> Responses { get; set; } = new List<SurveyQuestionResponseVM>();


        private bool showSurvey = false;

        protected async override Task OnInitializedAsync()
        {
            ActivitySummary = await ActivityService.GetActivitySummaryAsync(ActivityId);
        }

        private async Task OnClickAnonymousParticipant(SurveyParticipantVM participant)
        {
            Participant = participant;
            showSurvey = true;
            Responses = await GetSurveyAsync();
        }

        private async Task<List<SurveyQuestionResponseVM>> GetSurveyAsync()
        {
            var surveyQuestions = await SurveyService.GetQuestionnaireAsync(ActivityId);
            return surveyQuestions.Select(SurveyQuestionResponseVM.Create).ToList();
        }
        private async Task SurveyValidSubmit()
        {
            var succeed = await SurveyService.PostAnonymousSurveyResponseAsync(ActivityId, AnonymousSurveyResponseVM.Create(Participant, Responses.Select(sr => sr.Response)));
            NavigationManager.NavigateTo("/survey/end");
        }

    }
}