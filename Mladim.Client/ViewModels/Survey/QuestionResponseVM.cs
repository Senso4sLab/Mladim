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

public abstract class SelectableQuestionResponseVM<T> : QuestionResponseVM, ISelectableResponse where T: Enum
{
    public virtual T Response { get; set; } = default!;
    public virtual Enum ResponseEnum => this.Response;
    public SelectableQuestionResponseVM(int uniqueQuestionId) :base(uniqueQuestionId) { }
}

public class QuestionRatingResponseVM : SelectableQuestionResponseVM<SurveyRatingResponseType> 
{
    [RatingResponseValidator]
    public override SurveyRatingResponseType Response { get; set; }
    public QuestionRatingResponseVM(int uniqueQuestionId) : base(uniqueQuestionId) {}
}

public class QuestionBooleanResponseVM : SelectableQuestionResponseVM<SurveyBooleanResponseType> 
{
    [BooleanResponseValidator]
    public override SurveyBooleanResponseType Response { get; set; }   
    public QuestionBooleanResponseVM(int uniqueQuestionId) : base(uniqueQuestionId) {}
}

public class QuestionButtonResponseVM : SelectableQuestionResponseVM<SurveyButtonResponseType>
{
    [ButtonResponseValidator]
    public override SurveyButtonResponseType Response { get; set; }   
    public QuestionButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId) {}    
}


public abstract class MultiSelectableQuestionResponseVM<T> : QuestionResponseVM, IMultiSelectableResponse where T : ISelectableResponse
{
    public virtual List<T> Response { get; set; } = new List<T>();
    public virtual List<ISelectableResponse> ResponseEnum => 
        Response.OfType<ISelectableResponse>().ToList();
    public MultiSelectableQuestionResponseVM(int uniqueQuestionId) : base(uniqueQuestionId) { }

    public void AddQuestionResponse(T response)
    {      
        this.Response.Add(response);
    }
}

public class QuestionMultiButtonResponseVM : MultiSelectableQuestionResponseVM<QuestionButtonResponseVM>
{
    [ValidateComplexType]
    public override List<QuestionButtonResponseVM> Response { get; set; } = new List<QuestionButtonResponseVM>();
    public QuestionMultiButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId) {}
}



public class QuestionRepetitiveButtonResponseVM : SelectableQuestionResponseVM<SurveyRepetitiveButtonResponseType>
{
    [RepetitiveButtonResponseValidator]
    public override SurveyRepetitiveButtonResponseType Response { get; set; } 
    public QuestionRepetitiveButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId) { }
   
}

public class QuestionMultiRepetitiveButtonResponseVM : MultiSelectableQuestionResponseVM<QuestionRepetitiveButtonResponseVM>
{
    [ValidateComplexType]
    public override List<QuestionRepetitiveButtonResponseVM> Response { get; set; } = new List<QuestionRepetitiveButtonResponseVM>();   
    public QuestionMultiRepetitiveButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId) { }  
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


