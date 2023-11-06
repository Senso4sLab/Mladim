using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;
using System.Text.Json.Serialization;

namespace Mladim.Domain.Models.Survey.Responses;


public class AnonymousSurveyResponse
{
    public int Id { get; set; }
    public Activity Activity { get; set; } = default!;
    public int ActivityId { get; set; }
    public List<QuestionResponse> Responses { get; set; } = new();
    public AnonymousParticipant AnonymousParticipant { get; set; } = default!;    
}


[JsonDerivedType(typeof(QuestionResponse), typeDiscriminator: "baseQuestion")]
[JsonDerivedType(typeof(QuestionRatingResponse), typeDiscriminator: "ratingQuestion")]
[JsonDerivedType(typeof(QuestionTextResponse), typeDiscriminator: "textQuestion")]
[JsonDerivedType(typeof(QuestionBooleanResponse), typeDiscriminator: "booleanQuestion")]
[JsonDerivedType(typeof(QuestionMultiButtonResponse), typeDiscriminator: "multipleButtonQuestion")]
public class QuestionResponse
{   
    public int UniqueQuestionId { get; set; }   
}

public abstract class QuestionResponse<T> : QuestionResponse
{
    public T Response { get; set; } = default!;
}

public class QuestionRatingResponse : QuestionResponse<SurveyRatingResponseType>
{
    
}

public class QuestionTextResponse : QuestionResponse<string>
{
    
}


public class QuestionBooleanResponse : QuestionResponse<SurveyBooleanResponseType>
{
   
}

public class QuestionMultiButtonResponse : QuestionResponse<List<SurveyButtonResponseType>>
{
   
}
