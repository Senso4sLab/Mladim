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


public interface ISelectableResponse
{
    int UniqueQuestionId { get; set; }
    Enum ResponseEnum { get; }   
}

public interface IMultiSelectableResponse
{
    List<ISelectableResponse> ResponseEnum { get; }
}

public abstract class SelectableQuestionResponseVM<T> : QuestionResponseVM, ISelectableResponse
{
    public virtual T Response { get; set; } = default!;
    public abstract Enum ResponseEnum { get; }
   

    public SelectableQuestionResponseVM(int uniqueQuestionId) :base(uniqueQuestionId)
    {  
        
    }
}

public class QuestionRatingResponseVM : SelectableQuestionResponseVM<SurveyRatingResponseType> // QuestionRatingResponse
{
    [RatingResponseValidator]
    public override SurveyRatingResponseType Response { get; set; }
    public override Enum ResponseEnum => this.Response ;

    public QuestionRatingResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }
}

public class QuestionBooleanResponseVM : SelectableQuestionResponseVM<SurveyBooleanResponseType> 
{
    [BooleanResponseValidator]
    public override SurveyBooleanResponseType Response { get; set; }
    public override Enum ResponseEnum => this.Response;
    public QuestionBooleanResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }
}


public record SurveryButtonResponseVM
{
    [ButtonResponseValidator]
    public SurveyButtonResponseType ButtonType { get; set; }   

}

public class QuestionButtonResponseVM : SelectableQuestionResponseVM<SurveryButtonResponseVM>
{
    [ValidateComplexType]
    public override SurveryButtonResponseVM Response { get; set; } = new();
    public override Enum ResponseEnum => this.Response.ButtonType;
    public QuestionButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }
    public QuestionButtonResponseVM(int uniqueQuestionId, SurveryButtonResponseVM response) : base(uniqueQuestionId)
    {
        this.Response = response;   
    }
}


public abstract class MultiSelectableQuestionResponseVM : QuestionResponseVM, IMultiSelectableResponse
{   
    public abstract List<ISelectableResponse> ResponseEnum { get; }

    public MultiSelectableQuestionResponseVM(int uniqueQuestionId) :base(uniqueQuestionId)
    {

    }
}

public class QuestionMultiButtonResponseVM : MultiSelectableQuestionResponseVM
{
    [ValidateComplexType]
    public List<QuestionButtonResponseVM> Response { get; set; } = new();

    public override List<ISelectableResponse> ResponseEnum => 
        this.Response.OfType<ISelectableResponse>().ToList();
    public QuestionMultiButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {       
        
    }


    public QuestionButtonResponseVM AddButtonResponse()
    {
        QuestionButtonResponseVM questionButtonResponseVM = new QuestionButtonResponseVM(this.UniqueQuestionId);
        this.Response.Add(questionButtonResponseVM);
        return questionButtonResponseVM;
    }


}



public record SurveyRepetitiveButtonResponseVM
{
    [RepetitiveButtonResponseValidator]
    public SurveyRepetitiveButtonResponseType ButtonType { get; set; }    
}

public class QuestionRepetitiveButtonResponseVM : SelectableQuestionResponseVM<SurveyRepetitiveButtonResponseVM>
{
    [ValidateComplexType]
    public override SurveyRepetitiveButtonResponseVM Response { get; set; } = new();
    public override Enum ResponseEnum =>
        this.Response.ButtonType;
    public QuestionRepetitiveButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }

    public QuestionRepetitiveButtonResponseVM(int uniqueQuestionId, SurveyRepetitiveButtonResponseVM response) : base(uniqueQuestionId)
    {
        this.Response = response;
    }
}


public class QuestionMultiRepetitiveButtonResponseVM : MultiSelectableQuestionResponseVM
{
    [ValidateComplexType]
    public List<QuestionRepetitiveButtonResponseVM> Response { get; set; } = new();

    public override List<ISelectableResponse> ResponseEnum =>
       this.Response.OfType<ISelectableResponse>().ToList();
    public QuestionMultiRepetitiveButtonResponseVM(int uniqueQuestionId) :base(uniqueQuestionId)
    {

    }

    public QuestionRepetitiveButtonResponseVM AddButtonResponse()
    {
        QuestionRepetitiveButtonResponseVM questionRepetitiveButtonResponseVM = new QuestionRepetitiveButtonResponseVM(this.UniqueQuestionId);       
        this.Response.Add(questionRepetitiveButtonResponseVM);
        return questionRepetitiveButtonResponseVM;
    }
}

public interface ITextResponse
{
    int UniqueQuestionId { get; set; }
    string Response { get; }
}

public class QuestionTextResponseVM : QuestionResponseVM, ITextResponse
{
    public string Response { get; set; } = string.Empty;   

    public QuestionTextResponseVM(int uniqueQuestionId) :base(uniqueQuestionId)
    {
        this.UniqueQuestionId = uniqueQuestionId;
    }    
}


//public class QuestionTextResponseVM : QuestionResponseVM<string> // QuestionTextResponse
//{
//    public QuestionTextResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
//    {

//    }

//    public override string ToString() =>
//        this.Response;

//}





//public class QuestionButtonResponseVM : QuestionResponseVM<SurveryButtonResponseVM>  // QuestionButtonReponse
//{
//    [ValidateComplexType]
//    public override SurveryButtonResponseVM Response { get; set; }
//    public QuestionButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
//    {
//    }

//    public override string ToString() =>
//        this.Response.ToString();
//}


//public class QuestionMultiButtonResponseVM : QuestionResponseVM<List<SurveryButtonResponseVM>> // QuestionMultipleButtonReponse
//{
//    [ValidateComplexType]
//    public override List<SurveryButtonResponseVM> Response { get; set; } = new();
//    public QuestionMultiButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
//    {

//    }

//    public override string ToString() =>
//         string.Join(',', this.Response);

//    public override IEnumerable<string> QuestionResponses => this.Response.Select(br => br.ToString()).ToList();

//}



//public class QuestionMultiRepetitiveButtonResponseVM : QuestionResponseVM<List<SurveyRepetitiveButtonResponseVM>> // QuestionMultipleButtonReponse
//{
//    [ValidateComplexType]
//    public override List<SurveyRepetitiveButtonResponseVM> Response { get; set; } = new();
//    public QuestionMultiRepetitiveButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
//    {

//    }

//    public override string ToString() =>
//         string.Join(',', this.Response);

//    public override IEnumerable<string> QuestionResponses => this.Response.Select(br => br.ToString()).ToList();

//}



