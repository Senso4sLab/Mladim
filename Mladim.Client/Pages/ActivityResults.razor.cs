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

    protected async override Task OnInitializedAsync()
    {
        if (ActivityId is null)
            return;

        SurveyResponsesGroupByQuestions = await GetSurveyResponsesGroupByQuestionAsync();
    }


    private void OnSwitcerStateChanged(bool? switchState)
    {
        selector = switchState is true ? new AgeGroupSurveyResponseSelector() : new GenderSurveyResponseSelector();       
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

    private string RowStyleFunc(SurveyResponsesGroupedByQuestion question, int index)
    {
        string rowCss = "font-size: 0.8rem; font-family:poppins; font-weight:400; line-height:1.0; letter-spacing:-0.024rem; color:#6e7191;";

        return index % 2 == 0 ? rowCss + "background-color:white;" : rowCss + "background-color:#EFEFEF;";
    }

    public class CustomSelectorToBoolConverter : BoolConverter<SurveyResponseSelector>
    {

        public CustomSelectorToBoolConverter()
        {
            SetFunc = OnSet;
            GetFunc = OnGet;
        }

        private SurveyResponseSelector Gender = new GenderSurveyResponseSelector();
        private SurveyResponseSelector AgeGroup = new AgeGroupSurveyResponseSelector();

        private SurveyResponseSelector OnGet(bool? value)
        {
            try
            {
                return (value == true) ? Gender : AgeGroup;
            }
            catch (Exception e)
            {
                UpdateGetError("Conversion error: " + e.Message);
                return new GenderSurveyResponseSelector();
            }
        }

        private bool? OnSet(SurveyResponseSelector? arg)
        {           
            try
            {
                if (arg == null)
                    return true;
                if (arg.GetType() == typeof(GenderSurveyResponseSelector))
                    return true;
                if (arg.GetType() == typeof(AgeGroupSurveyResponseSelector))
                    return false;
                else
                    return true;
            }
            catch (FormatException e)
            {
                UpdateSetError("Conversion error: " + e.Message);
                return null;
            }
        }

    }   

    
}





