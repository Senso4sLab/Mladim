using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;


namespace Mladim.Domain.Models.Survey.Responses;


public class SurveyQuestionnairyResponse
{
    public int Id { get; set; }
    public List<SurveyResponse> Responses { get; set; } = new();
    public int AnonymousParticipantId { get; set; }
    public AnonymousParticipant AnonymousParticipant { get; set; }

    public int ActivityId { get; set; }
}

public abstract class SurveyResponse
{
    public int Id { get; set; }
    public int UniqueQuestionId { get; set; }   
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
