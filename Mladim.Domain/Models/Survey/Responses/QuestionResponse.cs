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

public class SelectableQuestionResponse : QuestionResponse, ISelectableResponse
{
    [NotMapped]
    public virtual IEnumerable<Enum> ResponseEnum => new Enum[] {};
}


public interface ISelectableResponse
{  
    IEnumerable<Enum> ResponseEnum { get; }
}




public class QuestionResponse<T> : SelectableQuestionResponse
{
    public T Response { get; set; } = default!;
}



public class QuestionTextResponse : QuestionResponse
{
    public string Response { get; set; } = default!;

}

public class QuestionRatingResponse : QuestionResponse<SurveyRatingResponseType>, ISelectableResponse
{    
    public override IEnumerable<Enum> ResponseEnum => new Enum[] { this.Response };
}
public class QuestionBooleanResponse : QuestionResponse<SurveyBooleanResponseType>, ISelectableResponse
{

   
    public override IEnumerable<Enum> ResponseEnum => new Enum[] { this.Response };
}

public class QuestionMultiButtonResponse : QuestionResponse<List<SurveyButtonResponseType>>, ISelectableResponse
{

   
    public override IEnumerable<Enum> ResponseEnum => this.Response.OfType<Enum>() ;
}
public class QuestionMultiRepetitiveButtonResponse : QuestionResponse<List<SurveyRepetitiveButtonResponseType>>, ISelectableResponse
{
  
    public override IEnumerable<Enum> ResponseEnum => this.Response.OfType<Enum>();
}







