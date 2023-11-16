using global::Microsoft.AspNetCore.Components;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Enums;
using Mladim.Client.ViewModels.Survey;
using Mladim.Client.ViewModels;
using System.Linq;
using System.Collections.Generic;
using Syncfusion.Blazor.PivotView;
using MudBlazor;
using Mladim.Client.Components.Dialogs;
using Mladim.Client.Utilities.Converters;

namespace Mladim.Client.Pages;

public partial class ActivityResults
{

    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    [Parameter]
    public int? ActivityId { get; set; }

    private IEnumerable<SurveyResponsesGroupedByQuestion> SurveyResponsesGroupByQuestions = new List<SurveyResponsesGroupedByQuestion>();
    public SurveyResponseSelector selector { get; set; } = new GenderSurveyResponseSelector();   
    private CustomSelectorToBoolConverter customSelectorToBoolConverter = new CustomSelectorToBoolConverter();
    protected async override Task OnInitializedAsync()
    {
        if (ActivityId is null)
            return;

        SurveyResponsesGroupByQuestions = await GetSurveyResponsesGroupByQuestionAsync();
    }
    private async Task<IEnumerable<SurveyResponsesGroupedByQuestion>> GetSurveyResponsesGroupByQuestionAsync()
    {
        IEnumerable<SurveyQuestionVM> surveyQuestions = await SurveyService.GetSurveyQuestionnairyAsync(ActivityId!.Value, Gender.Female);
        IEnumerable<AnonymousSurveyResponseVM> surveyResponses = await SurveyService.GetAnonymousSurveyResponsesAsync(ActivityId.Value);

        return surveyResponses.SelectMany(sr => sr.Responses, (asr, response) => new
            {
                Participant = asr.AnonymousParticipant,
                QuestionResponse = response,
            })
            .GroupBy(u => u.QuestionResponse.UniqueQuestionId)
            .Select(g => SurveyResponsesGroupedByQuestion.Create(surveyQuestions.FirstOrDefault(sq => sq.UniqueQuestionId == g.Key), g.Select(a => ParticipantQuestionResponse.Create(a.Participant, a.QuestionResponse))))
            .ToList();
    }    


    private void OnClickCsvExportFile()
    {

    }
}





