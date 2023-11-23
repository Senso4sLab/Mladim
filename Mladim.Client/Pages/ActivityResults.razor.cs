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
using Microsoft.JSInterop;
using CsvHelper;
using System.Globalization;

namespace Mladim.Client.Pages;

public partial class ActivityResults
{

    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    [Parameter]
    public int? ActivityId { get; set; }

    [Inject]
    public IJSRuntime JS { get; set; }

    private IEnumerable<SurveyResponsesGroupedByQuestionVM> SurveyResponsesGroupByQuestions = new List<SurveyResponsesGroupedByQuestionVM>();
    public SurveyResponseSelector selector { get; set; } = new GenderSurveyResponseSelector();   
    private CustomSelectorToBoolConverter customSelectorToBoolConverter = new CustomSelectorToBoolConverter();
    protected async override Task OnInitializedAsync()
    {
        if (ActivityId is null)
            return;

        SurveyResponsesGroupByQuestions = await GetSurveyResponsesGroupByQuestionAsync();
    }
    private async Task<IEnumerable<SurveyResponsesGroupedByQuestionVM>> GetSurveyResponsesGroupByQuestionAsync()
    {
        IEnumerable<SurveyQuestionVM> surveyQuestions = await SurveyService.GetSurveyQuestionnairyAsync(ActivityId!.Value, Gender.Female);
        IEnumerable<AnonymousSurveyResponseVM> surveyResponses = await SurveyService.GetAnonymousSurveyResponsesAsync(ActivityId.Value);

        return surveyResponses.SelectMany(sr => sr.Responses, (asr, response) => new
            {
                Participant = asr.AnonymousParticipant,
                QuestionResponse = response,
            })
            .GroupBy(u => u.QuestionResponse.UniqueQuestionId)
            .Select(g => SurveyResponsesGroupedByQuestionVM.Create(surveyQuestions.FirstOrDefault(sq => sq.UniqueQuestionId == g.Key), g.Select(a => ParticipantQuestionResponseVM.Create(a.Participant, a.QuestionResponse))))
            .ToList();
    }    


    private async Task OnClickCsvExportFile()
    {
        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream);    
        

        

        using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
        {
            foreach (var surveyResponses in SurveyResponsesGroupByQuestions) 
            {
                csv.WriteComment(surveyResponses.Question);
                csv.NextRecord();
                
                //surveyResponses.
                //csv.NextRecord();
            }
            //foreach (var row in rows)
            //{
            //    csv.WriteComment(RatingResponses.Question);
            //    csv.NextRecord();
            //    csv.WriteRecords(row.ParticipantsPerType);
            //    csv.NextRecord();
            //}
        }       

        string fileName = $"{selector.Name}_{ActivityId}";
        if (streamWriter != null)
        {
            using var streamRef = new DotNetStreamReference(stream: streamWriter.BaseStream);
            await JS.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }
    }



}





