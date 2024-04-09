using CsvHelper;
using CsvHelper.Configuration;
using global::Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream);

        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", LeaveOpen = true, };

        using (var csv = new CsvWriter(streamWriter, csvConfiguration))
        {   

            csv.WriteField("Spol");
            csv.WriteField("Starostna skupina");


            var delimeter = csv.Configuration.Delimiter;

            foreach (var question in surveyQuestions)
            {
                foreach (var questionText in question.Texts) // TODO preveri
                    csv.WriteField(questionText);
            }

            csv.NextRecord();

            foreach (var surveyResponse in surveyResponses)
            {
                csv.WriteField(surveyResponse.AnonymousParticipant.Gender.GetDisplayAttribute());
                csv.WriteField(surveyResponse.AnonymousParticipant.AgeGroup.GetDisplayAttribute());

                foreach (var qResponse in surveyResponse.Responses)
                {
                    if(qResponse is ISelectableResponse selectable)
                    {                       
                       csv.WriteField(selectable.ResponseEnum.GetDisplayAttribute());
                    }
                    else if (qResponse is IMultiSelectableResponse multiSelectable)
                    {                        
                        foreach (var mSelectable in multiSelectable.ResponseEnum)
                            csv.WriteField(mSelectable.ResponseEnum.GetDisplayAttribute());
                    }
                    else if(qResponse is ITextResponse textable)                    
                        csv.WriteField(textable.Response);                    
                    
                }
                csv.NextRecord();
            }
        }
        memoryStream.Position = 0;

        using var streamRef = new DotNetStreamReference(stream: memoryStream);
        await JS.InvokeVoidAsync("downloadFileFromStream", $"file_{ActivityId}.csv", streamRef);        
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