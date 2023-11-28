using Mladim.Client.Validators.SurveyResponseValidators;
using Mladim.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Mladim.Domain.Extensions;

namespace Mladim.Client.ViewModels.Survey;

public abstract class ParticipantQuestionResponseVM
{
    public AnonymousParticipantVM AnonymousParticipant { get; set; }
    public int UniqueQuestionId { get; protected set; }

    public ParticipantQuestionResponseVM(AnonymousParticipantVM anonymousParticipant, int uniqueQuestionId)
    {
        this.AnonymousParticipant = anonymousParticipant;
        this.UniqueQuestionId = uniqueQuestionId;
    }

    public static ParticipantQuestionResponseVM Create(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM questionResponse) => 
        questionResponse switch
        {
            QuestionResponseVM<SurveyRatingResponseType> response => new ParticipantQuestionRatingResponseVM(anonymousParticipant, response),
            QuestionResponseVM<SurveyBooleanResponseType> response => new ParticipantQuestionBooleanResponseVM(anonymousParticipant, response),
            QuestionResponseVM<SurveryButtonResponseVM> response => new ParticipantQuestionButtonResponseVM(anonymousParticipant, response),    
            QuestionResponseVM<string> response => new ParticipantQuestionTextResponseVM(anonymousParticipant, response),

        };
   
}


public class ParticipantQuestionResponseVM<T> : ParticipantQuestionResponseVM
{  
    public virtual T Response { get; protected set; }
    public ParticipantQuestionResponseVM(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM<T> response) 
        : base(anonymousParticipant, response.UniqueQuestionId)
    {
        this.Response = response.Response;
    }
}


public class ParticipantQuestionRatingResponseVM : ParticipantQuestionResponseVM<SurveyRatingResponseType>
{
    [RatingResponseValidator]
    public override SurveyRatingResponseType Response { get; protected set; }

    public ParticipantQuestionRatingResponseVM(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM<SurveyRatingResponseType> response) 
        : base(anonymousParticipant, response)
    {

    }

    public override string ToString() =>
        this.Response.GetDisplayAttribute();

}

public class ParticipantQuestionTextResponseVM : ParticipantQuestionResponseVM<string>
{
    public ParticipantQuestionTextResponseVM(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM<string> response) 
        : base(anonymousParticipant, response)
    {

    }
    public override string ToString() =>
        this.Response;

}


public class ParticipantQuestionBooleanResponseVM : ParticipantQuestionResponseVM<SurveyBooleanResponseType>
{
    [BooleanResponseValidator]
    public override SurveyBooleanResponseType Response { get; protected set; }
    public ParticipantQuestionBooleanResponseVM(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM<SurveyBooleanResponseType> response) 
        : base(anonymousParticipant, response)
    {

    }

    public override string ToString() =>
        this.Response.GetDisplayAttribute();

}


public class ParticipantQuestionButtonResponseVM : ParticipantQuestionResponseVM<SurveryButtonResponseVM>
{
    [ValidateComplexType]
    public override SurveryButtonResponseVM Response { get; protected set; }
    public ParticipantQuestionButtonResponseVM(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM<SurveryButtonResponseVM> response)
        : base(anonymousParticipant, response)
    {
    }

    public override string ToString() =>
        this.Response.ToString();

}


public class ParticipantQuestionMultiButtonResponseVM : ParticipantQuestionResponseVM<List<SurveryButtonResponseVM>>
{
    [ValidateComplexType]
    public override List<SurveryButtonResponseVM> Response { get; protected set; } = new();
    public ParticipantQuestionMultiButtonResponseVM(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM<List<SurveryButtonResponseVM>> response)
        : base(anonymousParticipant, response)
    {

    }

    public override string ToString() =>
         string.Join(',', this.Response);

}



public record SurveryButtonResponseVM
{
    [ButtonResponseValidator]
    public SurveyButtonResponseType ButtonType { get; set; }

    public override string ToString() =>
        this.ButtonType.GetDisplayAttribute();

}
