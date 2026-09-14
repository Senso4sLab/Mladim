using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Mladim.Client.ViewModels.Survey;

public class SurveyQuestionResponseVM
{
   
    public SurveyQuestionVM Question { get; set; }    

    [ValidateComplexType]
    public QuestionResponseVM Response { get; }
    private SurveyQuestionResponseVM(SurveyQuestionVM question)
    {
        this.Question = question;       
        this.Response = CreateDefaultResponse(question.Type, question.Id);
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
   

    public static SurveyQuestionResponseVM Create(SurveyQuestionVM question) =>
        new SurveyQuestionResponseVM(question);
}
