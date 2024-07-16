using CsvHelper;
using CsvHelper.Configuration;
using global::Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Mladim.Client.Services.Csv;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.Utilities.CsvMapping;
using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;
using System.Globalization;

namespace Mladim.Client.Pages;

public partial class ActivityResults
{

    [Parameter]
    public int? ActivityId { get; set; }       

    private bool isTableMode = true;

    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    [Inject]
    public IJSRuntime JS { get; set; }

    [Inject]
    public ICsvService CsvService { get; set; }



    private IEnumerable<SurveyQuestionVM> surveyQuestions = new List<SurveyQuestionVM>();
    // AnonymousSurveyResponseVM one anonymous with multiple question responses
    private IEnumerable<AnonymousSurveyResponseVM> surveyResponses = new List<AnonymousSurveyResponseVM>(); 
    private IEnumerable<SurveyResponsesGroupedByQuestionVM> SurveyResponsesGroupByQuestions = new List<SurveyResponsesGroupedByQuestionVM>();
    protected async override Task OnInitializedAsync()
    {
        if (ActivityId is int activityId)
        {
            surveyQuestions = await SurveyService.GetSurveyQuestionnairyAsync(activityId, Gender.Female);
            surveyResponses = await SurveyService.GetAnonymousSurveyResponsesAsync(activityId);
            SurveyResponsesGroupByQuestions = GetSurveyResponsesGroupByQuestion(activityId);
        }
    }

    private IEnumerable<SurveyResponsesGroupedByQuestionVM> GetSurveyResponsesGroupByQuestion(int activityId) =>
        surveyResponses.SelectMany(sr => sr.Responses, (asr, response) => (response.UniqueQuestionId, ParticipantResponse: ParticipantQuestionResponseVM.Create(asr.AnonymousParticipant, response)))
            .GroupBy(pqr => pqr.UniqueQuestionId, pqr => pqr.ParticipantResponse)
            .Select(g => SurveyResponsesGroupedByQuestionVM.Create(GetSurveyQuestionById(g.Key), g))
            .ToList();

    private SurveyQuestionVM? GetSurveyQuestionById(int id) =>
        surveyQuestions.FirstOrDefault(sq => sq.UniqueQuestionId == id);

    private async Task OnClickCsvExportFile()
    {

        CsvService.Open();

        CsvService.Write("Spol", "Starostna skupina");
        CsvService.Write(surveyQuestions.SelectMany(q => q.Texts));
        CsvService.NextRow();

        foreach (var surveyResponse in surveyResponses)
        {           

            CsvService.Write(surveyResponse.AnonymousParticipant.Gender.GetDisplayAttribute(), surveyResponse.AnonymousParticipant.AgeGroup.GetDisplayAttribute());

            foreach (var qResponse in surveyResponse.Responses)
            {
                if (qResponse is ISelectableResponse selectable)
                {
                    CsvService.Write(selectable.ResponseEnum.GetDisplayAttribute());
                }
                else if (qResponse is IMultiSelectableResponse multiSelectable)
                {
                    foreach (var mSelectable in multiSelectable.ResponseEnum)
                        CsvService.Write(mSelectable.ResponseEnum.GetDisplayAttribute());
                }
                else if (qResponse is ITextResponse textable)
                    CsvService.Write(textable.Response);

            }
            CsvService.NextRow();
        }

        CsvService.Close();

        using var streamRef = new DotNetStreamReference(CsvService.Stream);

        await JS.InvokeVoidAsync("downloadFileFromStream", $"Statistika_anket_{ActivityId}.csv", streamRef);     
    }


    private void OnTablePresentation()
    {
        isTableMode = true;
        StateHasChanged();
    }

    private void OnChartPresentation()
    {
        isTableMode= false;
        StateHasChanged();
    }
}