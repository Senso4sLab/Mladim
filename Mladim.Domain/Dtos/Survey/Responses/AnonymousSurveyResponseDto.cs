
using Mladim.Domain.Dtos.Members.AnonymousParticipants;
using Mladim.Domain.Enums;
using System.Text.Json.Serialization;

namespace Mladim.Domain.Dtos.Survey.Responses;




public class AnonymousSurveyResponseDto
{
    public List<QuestionResponseDto> Responses { get; set; } = new();
    public AnonymousParticipantCommandDto AnonymousParticipant { get; set; } = default!;
    public static AnonymousSurveyResponseDto Create(AnonymousParticipantCommandDto anonymousParticipant, IEnumerable<QuestionResponseDto> responses) =>
        new AnonymousSurveyResponseDto
        {
            AnonymousParticipant = anonymousParticipant,
            Responses = responses.ToList(),
        };

}


[JsonDerivedType(typeof(QuestionResponseDto), typeDiscriminator: "baseQuestion")]
[JsonDerivedType(typeof(QuestionRatingResponseDto), typeDiscriminator: "ratingQuestion")]
[JsonDerivedType(typeof(QuestionTextResponseDto), typeDiscriminator: "textQuestion")]
[JsonDerivedType(typeof(QuestionBooleanResponseDto), typeDiscriminator: "booleanQuestion")]
[JsonDerivedType(typeof(QuestionMultiButtonResponseDto), typeDiscriminator: "multipleButtonQuestion")]
[JsonDerivedType(typeof(QuestionMultiRepetitiveButtonResponseDto), typeDiscriminator: "multipleRepetitiveButtonQuestion")]
public class QuestionResponseDto
{   

    public int UniqueQuestionId { get; set; }
    
    public QuestionResponseDto()
    {

    }
}

public abstract class QuestionResponseDto<T> : QuestionResponseDto
{
    public T Response { get; set; } = default!;

    public QuestionResponseDto() : base() { }
}
    

public class QuestionRatingResponseDto : QuestionResponseDto<SurveyRatingResponseType>
{
    public QuestionRatingResponseDto():base()
    {
        
    }
}

public class QuestionTextResponseDto : QuestionResponseDto<string>
{
    public QuestionTextResponseDto():base()
    {
        
    }
}


public class QuestionBooleanResponseDto : QuestionResponseDto<SurveyBooleanResponseType>
{
    public QuestionBooleanResponseDto():base()
    {
        
    }
}

public class QuestionMultiButtonResponseDto : QuestionResponseDto<List<SurveyButtonResponseType>>
{
    public QuestionMultiButtonResponseDto():base()
    {
        
    }
}

public class QuestionMultiRepetitiveButtonResponseDto : QuestionResponseDto<List<SurveyRepetitiveButtonResponseType>>
{
    public QuestionMultiRepetitiveButtonResponseDto() : base()
    {

    }
}
