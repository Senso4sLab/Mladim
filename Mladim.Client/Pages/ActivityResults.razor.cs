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
using Mladim.Client.Utilities.CsvMapping;
using System.IO;

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
    public SurveyResponseSelector selector { get; set; } = SurveyResponseSelector.CreateGenderSelector();  
    private CustomSelectorToBoolConverter customSelectorToBoolConverter = new CustomSelectorToBoolConverter();
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


    private IEnumerable<SurveyParticipantRow> GenerateRows(SurveyResponsesGroupedByQuestionVM surveyResponse)
    {
        return this.selector.ParticipantPredicatesByType.SelectMany(pp => surveyResponse.NumberOfParticipantsByCriterion(pp.Predicate, pp.Name));
    }

    private async Task OnClickCsvExportFile()
    {
        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream);

        using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture,true))
        {
            csv.Context.RegisterClassMap<ParticipantsPerTypeCsvMap>();

            foreach (var surveyResponses in SurveyResponsesGroupByQuestions)
            {
                if (surveyResponses.QuestionDescription != null)
                {
                    csv.WriteField(surveyResponses.QuestionDescription);
                    csv.NextRecord();
                    csv.NextRecord();
                }

                foreach(var group in GenerateRows(surveyResponses).GroupBy(row => row.Question))
                {
                    csv.WriteField(group.Key);
                    csv.NextRecord();
                    csv.NextRecord();

                    foreach (var row in group)
                    {
                        csv.WriteField(row.Criterion);
                        csv.NextRecord();
                        csv.WriteHeader<ParticipantsPerType>();
                        csv.NextRecord();
                        csv.NextRecord();
                        csv.WriteRecords(row.ParticipantsPerType);
                        csv.NextRecord();
                    }
                }               
            }           
        }

        memoryStream.Position = 0;
        
        using var streamRef = new DotNetStreamReference(stream: memoryStream);
        await JS.InvokeVoidAsync("downloadFileFromStream", $"{selector.Name}_{ActivityId}.csv", streamRef);
        
    }
}