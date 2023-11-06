using Mladim.Domain.Dtos.Survey.Questions;
using System.ComponentModel.DataAnnotations;

namespace Mladim.Client.ViewModels.Survey;

public class SurveyQuestionResponseVM
{
    public SurveyQuestionVM Questions { get; set; }

    private QuestionResponseVM surveyResponseVM = null!;

    [ValidateComplexType]
    public QuestionResponseVM Response => 
        surveyResponseVM ??= Questions.CreateSurveyResponse();


    private SurveyQuestionResponseVM(SurveyQuestionVM questions)
    {
        this.Questions = questions;
    }

    public void Deconstruct(out SurveyQuestionVM questions, out QuestionResponseVM response)
    {
        questions = Questions;
        response = Response;
    }

    public static SurveyQuestionResponseVM Create(SurveyQuestionVM questions) =>
        new SurveyQuestionResponseVM(questions);
}
