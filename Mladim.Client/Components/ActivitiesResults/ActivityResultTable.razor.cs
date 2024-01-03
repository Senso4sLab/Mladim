using global::Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Mladim.Client.ViewModels.Survey;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.Utilities.Converters;
using CsvHelper;
using Mladim.Client.Utilities.CsvMapping;
using System.Globalization;
using System.Reflection;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Client.Components.ActivitiesResults;

public partial class ActivityResultTable
{
    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    [Parameter]
    public IEnumerable<SurveyResponsesGroupedByQuestionVM> SurveyResponsesGroupByQuestions { get; set; } = new List<SurveyResponsesGroupedByQuestionVM>();

    [Parameter]
    public EventCallback CsvExportCallback { get; set; }

    private UnitSelectorStringConverter unitConverter = new UnitSelectorStringConverter();
    private CriterionSelectorStringConverter criterionConverter = new CriterionSelectorStringConverter();
    private CogntigencyTableContext cogntigencyTableContext { get; set; } = new CogntigencyTableContext();

    private IEnumerable<SurveyCriterionSelector> CriterionSelectors = new List<SurveyCriterionSelector>() { new GenderSelector(), new AgeGroupSelector() };
    private IEnumerable<UnitSelector> UnitSelectors = new List<UnitSelector>() { new PercantagesUnit(), new NumOfParticipantsUnit() };   

   
}