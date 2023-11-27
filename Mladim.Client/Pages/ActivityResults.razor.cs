using global::Microsoft.AspNetCore.Components;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Enums;

namespace Mladim.Client.Pages;

public partial class ActivityResults
{

    [Parameter]
    public int? ActivityId { get; set; }       

    private bool isTableMode = true;

    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    private IEnumerable<SurveyResponsesGroupedByQuestionVM> SurveyResponsesGroupByQuestions = new List<SurveyResponsesGroupedByQuestionVM>();
    protected async override Task OnInitializedAsync()
    {
        if (ActivityId is int activityId)
            SurveyResponsesGroupByQuestions = await GetSurveyResponsesGroupByQuestionAsync(activityId);
    }

    private async Task<IEnumerable<SurveyResponsesGroupedByQuestionVM>> GetSurveyResponsesGroupByQuestionAsync(int activityId)
    {
        IEnumerable<SurveyQuestionVM> surveyQuestions = await SurveyService.GetSurveyQuestionnairyAsync(activityId, Gender.Female);
        IEnumerable<AnonymousSurveyResponseVM> surveyResponses = await SurveyService.GetAnonymousSurveyResponsesAsync(activityId);

        return surveyResponses.SelectMany(sr => sr.Responses, (asr, response) => ParticipantQuestionResponseVM.Create(asr.AnonymousParticipant, response))
            .GroupBy(pqr => pqr.UniqueQuestionId)
            .Select(g => SurveyResponsesGroupedByQuestionVM.Create(surveyQuestions.FirstOrDefault(sq => sq.UniqueQuestionId == g.Key), g))
            .ToList();
    }

    private async Task OnTablePresentation()
    {
        isTableMode = true;
        StateHasChanged();
    }

    private async Task OnChartPresentation()
    {
        isTableMode= false;
        StateHasChanged();
    }
}