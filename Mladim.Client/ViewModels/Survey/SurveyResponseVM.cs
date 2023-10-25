using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels.Survey;








public abstract class SurveyResponseVM
{  
 
    public int UniqueQuestionId { get; set; }

    public SurveyResponseVM(int uniqueQuestionId)
    {
        this.UniqueQuestionId = uniqueQuestionId;   
    }

}

public abstract class SurveyResponseVM<T> : SurveyResponseVM
{
    public T Response { get; set; } = default!;

    public SurveyResponseVM(int uniqueQuestionId):base(uniqueQuestionId) { }

}

public class SurveryRatingResponseVM : SurveyResponseVM<SurveyRatingResponseType>
{
    public SurveryRatingResponseVM(int uniqueQuestionId) :base(uniqueQuestionId)
    {
        
    }
}

public class SurveryTextResponseVM : SurveyResponseVM<string>
{
    public SurveryTextResponseVM(int uniqueQuestionId):base(uniqueQuestionId)
    {
        
    }
}


public class SurveryBooleanResponseVM : SurveyResponseVM<SurveyBooleanResponseType>
{
    public SurveryBooleanResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }
}

public class SurveryMultipleResponseVM : SurveyResponseVM<List<SurveyMultipleResponseType>>
{
    public SurveryMultipleResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }
}
