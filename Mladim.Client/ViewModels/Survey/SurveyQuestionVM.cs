using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels.Survey;

public class SurveyQuestionVM
{    
    public int UniqueQuestionId { get; set; }
    public List<string> Texts { get; set; } = new();    
    public SurveyQuestionType Type { get; set; }    

    public SurveyResponseVM CreateSurveyResponse() => Type switch
    {
        SurveyQuestionType.Boolean => new SurveryBooleanResponseVM(UniqueQuestionId),
        SurveyQuestionType.Text => new SurveryTextResponseVM(UniqueQuestionId),
        SurveyQuestionType.Rating => new SurveryRatingResponseVM(UniqueQuestionId),
        SurveyQuestionType.Multiple => new SurveryMultipleResponseVM(UniqueQuestionId),
        _ => throw new NotImplementedException(),
    };
    
}




