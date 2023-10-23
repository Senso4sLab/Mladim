using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;


namespace Mladim.Domain.Models.Survey.Responses;




public abstract class SurveyResponse
{
    public int Id { get; set; }
    public int QuestionId { get; set; }    
    public SurveyQuestion Question {get;set;} = default!;
    public int ActivityId { get; set; }    
}

public abstract class SurveyResponse<T> : SurveyResponse
{
    public T Response { get; set; } = default!;
}

public class SurveryRatingResponse : SurveyResponse<SurveyRatingResponseType>
{
    
}

public class SurveryTextResponse : SurveyResponse<string>
{
    
}


public class SurveryBooleanResponse : SurveyResponse<SurveyBooleanResponseType>
{
   
}

public class SurveryMultipleResponse : SurveyResponse<List<SurveyMultipleResponseType>>
{
   
}
