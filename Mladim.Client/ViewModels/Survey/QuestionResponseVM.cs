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

public class QuestionResponseVM<T> : QuestionResponseVM
{
    public virtual T Response { get; set; }
    
    public QuestionResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {
    }

    public QuestionResponseVM(int uniqueQuestionId, T response) : this(uniqueQuestionId)
    {
        Response = response;
    }
}

public class QuestionRatingResponseVM : QuestionResponseVM<SurveyRatingResponseType>
{
    [RatingResponseValidator]
    public override SurveyRatingResponseType Response { get; set; }   

    public QuestionRatingResponseVM(int uniqueQuestionId) :base(uniqueQuestionId)
    {
        
    }
}

public class QuestionTextResponseVM : QuestionResponseVM<string>
{   
    public QuestionTextResponseVM(int uniqueQuestionId):base(uniqueQuestionId)
    {
        
    }
}


public class QuestionBooleanResponseVM : QuestionResponseVM<SurveyBooleanResponseType>
{
    [BooleanResponseValidator]
    public override SurveyBooleanResponseType Response { get; set; }
    public QuestionBooleanResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {

    }
}


public class QuestionButtonResponseVM : QuestionResponseVM<SurveryButtonResponseVM>
{
    [ValidateComplexType]
    public override SurveryButtonResponseVM Response { get; set; }
    public QuestionButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {
    }
}


public class QuestionMultiButtonResponseVM : QuestionResponseVM<List<QuestionButtonResponseVM>>
{
    [ValidateComplexType]
    public override List<QuestionButtonResponseVM> Response { get; set; } = new();
    public QuestionMultiButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
    {       

    }
}



public record SurveryButtonResponseVM
{
    [ButtonResponseValidator]
    public SurveyButtonResponseType ButtonType { get; set; }
}





public class ParticipantQuestionResponseVM
{
    public AnonymousParticipantVM AnonymousParticipant { get; set; }

    public ParticipantQuestionResponseVM(AnonymousParticipantVM anonymousParticipant)
    {
        this.AnonymousParticipant = anonymousParticipant;
    }
}

public class ParticipantQuestionResponseVM<T> : ParticipantQuestionResponseVM
{
    public QuestionResponseVM<T> QuestionResponse { get; set; }

    public ParticipantQuestionResponseVM(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM<T> questionResponse) : base(anonymousParticipant)
    {
        this.AnonymousParticipant = anonymousParticipant;
        this.QuestionResponse = questionResponse;
    }

   
}
