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
        surveyResponses.SelectMany(sr => sr.Responses, (asr, response) => ParticipantQuestionResponseVM.Create(asr.AnonymousParticipant, response))
            .GroupBy(pqr => pqr.UniqueQuestionId)
            .Select(g => SurveyResponsesGroupedByQuestionVM.Create(surveyQuestions.FirstOrDefault(sq => sq.UniqueQuestionId == g.Key), g))
            .ToList();



    private async Task OnClickCsvExportFile()
    {
        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream);

        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", LeaveOpen = true, };

        using (var csv = new CsvWriter(streamWriter, csvConfiguration))
        {
            //csv.Context.Configuration.Delimiter = ";";

            //csv.Context.Configuration.Mode = CsvMode.Escape;
            //csv.Context.Configuration.Escape = '\\';

            csv.WriteField("Spol");
            csv.WriteField("Starostna skupina");


            var delimeter = csv.Configuration.Delimiter;

            foreach (var question in surveyQuestions)
            {
                foreach (var questionText in question.QuestionTexts)
                    csv.WriteField(questionText);
            }

            csv.NextRecord();

            foreach (var surveyResponse in surveyResponses)
            {
                csv.WriteField(surveyResponse.AnonymousParticipant.Gender.GetDisplayAttribute());
                csv.WriteField(surveyResponse.AnonymousParticipant.AgeGroup.GetDisplayAttribute());

                foreach (var response in surveyResponse.Responses)
                {
                    foreach (var responseText in response.QuestionResponses)
                        csv.WriteField(responseText);
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