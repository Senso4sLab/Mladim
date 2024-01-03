using Mladim.Client.Validators.SurveyResponseValidators;
using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;
using Mladim.Domain.Models.Survey.Responses;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Mladim.Client.ViewModels.Survey;


public abstract class QuestionResponseVM
{   
    public int UniqueQuestionId { get; set; }
    public QuestionResponseVM(int uniqueQuestionId)
    {
        this.UniqueQuestionId = uniqueQuestionId;   
    }    

    public static QuestionResponseVM<T> Create<T>(int uniqueQuestionId, T response) =>
       new QuestionResponseVM<T>(uniqueQuestionId, response);

    public abstract IEnumerable<string> QuestionResponses { get; }
}

public class QuestionResponseVM<T> : QuestionResponseVM
{
    public virtual T Response { get; set; }

    public override IEnumerable<string> QuestionResponses => new[] { Response.ToString() };

    public QuestionResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {
    }

    public QuestionResponseVM(int uniqueQuestionId, T response) : this(uniqueQuestionId)
    {
        Response = response;
    }

    
}



public class QuestionRatingResponseVM : QuestionResponseVM<SurveyRatingResponseType> // QuestionRatingResponse
{
    [RatingResponseValidator]
    public override SurveyRatingResponseType Response { get; set; }

    public override IEnumerable<string> QuestionResponses => new[] { Response.GetDisplayAttribute() };

    public QuestionRatingResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }

    public override string ToString() =>
        this.Response.GetDisplayAttribute();

}

public class QuestionTextResponseVM : QuestionResponseVM<string> // QuestionTextResponse
{
    public QuestionTextResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }

    public override string ToString() =>
        this.Response;

}


public class QuestionBooleanResponseVM : QuestionResponseVM<SurveyBooleanResponseType> // QuestionBooleanReponse
{ 
    [BooleanResponseValidator]
    public override SurveyBooleanResponseType Response { get; set; }
    public override IEnumerable<string> QuestionResponses => new[] { Response.GetDisplayAttribute() };
    public QuestionBooleanResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }

    public override string ToString() =>
        this.Response.GetDisplayAttribute();

}


public class QuestionButtonResponseVM : QuestionResponseVM<SurveryButtonResponseVM>  // QuestionButtonReponse
{
    [ValidateComplexType]
    public override SurveryButtonResponseVM Response { get; set; }
    public QuestionButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {
    }

    public override string ToString() =>
        this.Response.ToString();

   

}


public class QuestionMultiButtonResponseVM : QuestionResponseVM<List<SurveryButtonResponseVM>> // QuestionMultipleButtonReponse
{
    [ValidateComplexType]
    public override List<SurveryButtonResponseVM> Response { get; set; } = new();
    public QuestionMultiButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }

    public override string ToString() =>
         string.Join(',', this.Response);

    public override IEnumerable<string> QuestionResponses => this.Response.Select(br => br.ToString()).ToList();

}



public class QuestionMultiRepetitiveButtonResponseVM : QuestionResponseVM<List<SurveyRepetitiveButtonResponseVM>> // QuestionMultipleButtonReponse
{
    [ValidateComplexType]
    public override List<SurveyRepetitiveButtonResponseVM> Response { get; set; } = new();
    public QuestionMultiRepetitiveButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }

    public override string ToString() =>
         string.Join(',', this.Response);

    public override IEnumerable<string> QuestionResponses => this.Response.Select(br => br.ToString()).ToList();

}



