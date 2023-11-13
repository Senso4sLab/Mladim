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

    string resultsRespectTo = "Spol";

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


    private async Task OnClickTextsResponseCommands(SurveyTextResponsesGroupedByQuestion textGroupResponses)
    {
        var options = new DialogOptions { CloseOnEscapeKey = true, CloseButton = true };
        DialogParameters surveyTextResponsesParameters = new DialogParameters();
        surveyTextResponsesParameters.Add("Question", textGroupResponses.SurveyQuestion?.Texts.FirstOrDefault());
        surveyTextResponsesParameters.Add("AnonymousCommands", textGroupResponses.GetAnonymousComments().ToList());
        this.DialogService.Show<ShowTextQuestionCommands>("", surveyTextResponsesParameters, options);
    }


    




    private string RowStyleFunc(SurveyResponsesGroupedByQuestion question, int index)
    {
        string rowCss = "font-size: 0.8rem; font-family:poppins; font-weight:400; line-height:1.0; letter-spacing:-0.024rem; color:#6e7191;";

        return index % 2 == 0 ? rowCss + "background-color:white;" : rowCss + "background-color:#EFEFEF;";
    }



    public class CustomStringToBoolConverter : BoolConverter<string>
    {

        public CustomStringToBoolConverter()
        {
            SetFunc = OnSet;
            GetFunc = OnGet;
        }

        private string TrueString = "spola";
        private string FalseString = "starostne skupine";       

        private string OnGet(bool? value)
        {
            try
            {
                return (value == true) ? TrueString : FalseString;
            }
            catch (Exception e)
            {
                UpdateGetError("Conversion error: " + e.Message);
                return TrueString;
            }
        }

        private bool? OnSet(string arg)
        {
            if (arg == null)
                return null;
            try
            {
                if (arg == TrueString)
                    return true;
                if (arg == FalseString)
                    return false;
                else
                    return null;
            }
            catch (FormatException e)
            {
                UpdateSetError("Conversion error: " + e.Message);
                return null;
            }
        }

    }
}





