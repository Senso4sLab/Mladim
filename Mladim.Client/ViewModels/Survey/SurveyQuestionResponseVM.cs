using Mladim.Domain.Dtos.Survey.Questions;

namespace Mladim.Client.ViewModels.Survey;

public class SurveyQuestionResponseVM
{
    public SurveyQuestionVM Questions { get; set; }

    private SurveyResponseVM surveyResponseVM = null;
    public SurveyResponseVM SurveyResponseVM => surveyResponseVM ??= Questions.CreateSurveyResponse();


    private SurveyQuestionResponseVM(SurveyQuestionVM questions)
    {
        this.Questions = questions;
    }

    public static SurveyQuestionResponseVM Create(SurveyQuestionVM questions) =>
        new SurveyQuestionResponseVM(questions);
}
