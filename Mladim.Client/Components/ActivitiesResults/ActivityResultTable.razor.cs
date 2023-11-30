using global::Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Mladim.Client.ViewModels.Survey;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.Utilities.Converters;
using CsvHelper;
using Mladim.Client.Utilities.CsvMapping;
using System.Globalization;

namespace Mladim.Client.Components.ActivitiesResults;

public partial class ActivityResultTable
{
    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    [Parameter]
    public IEnumerable<SurveyResponsesGroupedByQuestionVM> SurveyResponsesGroupByQuestions { get; set; } = new List<SurveyResponsesGroupedByQuestionVM>();

    [Inject]
    public IJSRuntime JS { get; set; }

    public SurveyResponseSelector responseSelector = SurveyResponseSelector.CreateGenderSelector();
    private SurveyCriterionSelectorToBoolConverter surveyCriterionToBoolConverter = default!; // = new SurveyCriterionSelectorToBoolConverter();


    private ContingencyCalculator contingencyCalculatorSelector = default!;
    private ContingencyCalculatorSelectorToBoolConverter contingencyCalculatorSelectorToBoolConverter = default!;
    protected override void OnInitialized()
    {
        //contingencyCalculatorSelector = new ContingencyTableParticipants(responseSelector.Name, responseSelector.ParticipantPredicatesByType);


        surveyCriterionToBoolConverter = new SurveyCriterionSelectorToBoolConverter();
        contingencyCalculatorSelectorToBoolConverter = new ContingencyCalculatorSelectorToBoolConverter(responseSelector);
      
    }
    private IEnumerable<ParticipantsByResponseType> GenerateRows(ISelectableReponseType ResponseType)
    {
        return this.responseSelector.ParticipantPredicatesByType.SelectMany(pp => ResponseType.ParticipantsByResponseTypes(pp.Predicate));
    }

    private async Task OnClickCsvExportFile()
    {
        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream);

        using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture, true))
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

                if (surveyResponses is ISelectableReponseType selectableReponseType)
                {
                    //foreach (var group in GenerateRows(selectableReponseType).GroupBy(row => row.Question))
                    //{
                    //    csv.WriteField(group.Key);
                    //    csv.NextRecord();
                    //    csv.NextRecord();

                    //    foreach (var row in group)
                    //    {
                    //        csv.WriteField(row.Criterion);
                    //        csv.NextRecord();
                    //        csv.WriteHeader<ParticipantsByResponseType>();
                    //        csv.NextRecord();
                    //        csv.NextRecord();
                    //        csv.WriteRecords(row.ParticipantsPerType);
                    //        csv.NextRecord();
                    //    }
                    //}
                }               
            }
        }

        memoryStream.Position = 0;

        using var streamRef = new DotNetStreamReference(stream: memoryStream);
        await JS.InvokeVoidAsync("downloadFileFromStream", $"{responseSelector.Name}.csv", streamRef);

    }
}