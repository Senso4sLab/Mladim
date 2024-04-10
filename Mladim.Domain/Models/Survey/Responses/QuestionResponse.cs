using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;
using System.ComponentModel.DataAnnotations.Schema;
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
[JsonDerivedType(typeof(QuestionMultiRepetitiveButtonResponse), typeDiscriminator: "multipleRepetitiveButtonQuestion")]
public class QuestionResponse
{
    public int UniqueQuestionId { get; set; }

}

public interface ISelectableResponse
{
    IEnumerable<Enum> ResponseEnum { get; }
}

public class SelectableQuestionResponse<T> : QuestionResponse, ISelectableResponse where T : Enum
{
    public T Response { get; set; } = default!;

    [NotMapped]
    public virtual IEnumerable<Enum> ResponseEnum => new Enum[] { Response };
}


public interface ITextResponse
{
    string Response { get; set; }
}

public class QuestionTextResponse : QuestionResponse, ITextResponse
{
    public string Response { get; set; } = default!;
}

public class QuestionRatingResponse : SelectableQuestionResponse<SurveyRatingResponseType>
{   
}
public class QuestionBooleanResponse : SelectableQuestionResponse<SurveyRatingResponseType>
{   
}

public abstract class MultiSelectableQuestionResponse<T> : QuestionResponse, ISelectableResponse where T : Enum
{
    public IEnumerable<T> Response { get; set; } = default!;

    [NotMapped]
    public virtual IEnumerable<Enum> ResponseEnum => Response.OfType<Enum>().ToList();
}

public class QuestionMultiButtonResponse : MultiSelectableQuestionResponse<SurveyButtonResponseType>{}
public class QuestionMultiRepetitiveButtonResponse : MultiSelectableQuestionResponse<SurveyRepetitiveButtonResponseType> { }    








