using global::Microsoft.AspNetCore.Components;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Enums;
using Mladim.Client.ViewModels.Survey;

namespace Mladim.Client.Pages;

public partial class ActivityResults
{

    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    [Parameter]
    public int? ActivityId { get; set; }

    private IEnumerable<SurveyQuestionVM> SurveyQuestions = new List<SurveyQuestionVM>();
    private IEnumerable<AnonymousSurveyResponseVM> SurveyResponses = new List<AnonymousSurveyResponseVM>();

    protected async override Task OnInitializedAsync()
    {
        if (ActivityId is null)
            return;

        SurveyQuestions = await SurveyService.GetSurveyQuestionnairyAsync(ActivityId.Value, Gender.Female);
        SurveyResponses = await SurveyService.GetAnonymousSurveyResponsesAsync(ActivityId.Value);


        var responseGroupByGender = SurveyResponses.SelectMany(sr => sr.Responses, (asr, response) => new
        {
            Gender = asr.AnonymousParticipant.Gender,
            QuestionResponse = response,
        }).GroupBy(u => new { u.Gender, u.QuestionResponse.UniqueQuestionId })
        .Select(g => new SurveyResponseGroupByGender(g.Key.Gender, g.Select(g => g.QuestionResponse)));

    }


    public class SurveyResponseGroupByGender
    {
        public Gender Gender { get; set; }
        public List<QuestionResponseVM> QuestionResponses { get; set; } = new();

        public SurveyResponseGroupByGender(Gender gender, IEnumerable<QuestionResponseVM> questionResponses)
        {
            this.Gender = gender;
            this.QuestionResponses = questionResponses.ToList();    
        }

    }


}