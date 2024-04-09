using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Mladim.Client.ViewModels.Survey;

public class SurveyQuestionResponseVM
{
    public SurveyQuestionVM Questions { get; set; }    

    [ValidateComplexType]
    public QuestionResponseVM Response { get; }
    private SurveyQuestionResponseVM(SurveyQuestionVM questions)
    {
        this.Questions = questions;
        this.Response = CreateDefaultResponse(questions.Type, questions.UniqueQuestionId);
    }
    private QuestionResponseVM CreateDefaultResponse(SurveyQuestionType type,  int questionId) => type switch
    {
        SurveyQuestionType.Boolean => new QuestionBooleanResponseVM(questionId),
        SurveyQuestionType.Text => new QuestionTextResponseVM(questionId),
        SurveyQuestionType.Rating => new QuestionRatingResponseVM(questionId),
        SurveyQuestionType.Multiple => new QuestionMultiButtonResponseVM(questionId),
        SurveyQuestionType.MultipleRepetitive => new QuestionMultiRepetitiveButtonResponseVM(questionId),
        _ => throw new NotImplementedException(),
    };

    public void Deconstruct(out SurveyQuestionVM questions, out QuestionResponseVM response)
    {
        questions = Questions;
        response = Response;
    }

    public static SurveyQuestionResponseVM Create(SurveyQuestionVM questions) =>
        new SurveyQuestionResponseVM(questions);
}
