using Mladim.Client.Validators.SurveyResponseValidators;
using Mladim.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Mladim.Client.ViewModels.Survey;


public abstract class QuestionResponseVM
{  
 
    public int UniqueQuestionId { get; set; }

    public QuestionResponseVM(int uniqueQuestionId)
    {
        this.UniqueQuestionId = uniqueQuestionId;   
    }

}

public class QuestionRatingResponseVM : QuestionResponseVM
{
    [RatingResponseValidator]
    public SurveyRatingResponseType Response { get; set; }
    public QuestionRatingResponseVM(int uniqueQuestionId) :base(uniqueQuestionId)
    {
        
    }
}

public class QuestionTextResponseVM : QuestionResponseVM
{
    public string Response { get; set; } = string.Empty;
    public QuestionTextResponseVM(int uniqueQuestionId):base(uniqueQuestionId)
    {
        
    }
}


public class QuestionBooleanResponseVM : QuestionResponseVM
{
    [BooleanResponseValidator]
    public SurveyBooleanResponseType Response { get; set; }
    public QuestionBooleanResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }
}


public class QuestionMultiButtonResponseVM : QuestionResponseVM
{
    [ValidateComplexType]
    public List<SurveryButtonResponseVM> Response { get; set; } = new();
    public QuestionMultiButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {       

    }
}


public class SurveryButtonResponseVM
{
    [ButtonResponseValidator]
    public SurveyButtonResponseType ButtonType { get; set; } = SurveyButtonResponseType.None;
}
