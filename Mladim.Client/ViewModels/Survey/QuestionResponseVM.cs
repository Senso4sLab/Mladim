using Mladim.Client.Validators.SurveyResponseValidators;
using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;
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

//public class QuestionRatingResponseVM : QuestionResponseVM<SurveyRatingResponseType>
//{
//    [RatingResponseValidator]
//    public override SurveyRatingResponseType Response { get; set; }   

//    public QuestionRatingResponseVM(int uniqueQuestionId) :base(uniqueQuestionId)
//    {
        
//    }

//    public override string ToString() =>    
//        this.Response.GetDisplayAttribute();
    
//}

//public class QuestionTextResponseVM : QuestionResponseVM<string>
//{   
//    public QuestionTextResponseVM(int uniqueQuestionId):base(uniqueQuestionId)
//    {
        
//    }

//    public override string ToString() => 
//        this.Response;
    
//}


//public class QuestionBooleanResponseVM : QuestionResponseVM<SurveyBooleanResponseType>
//{
//    [BooleanResponseValidator]
//    public override SurveyBooleanResponseType Response { get; set; }
//    public QuestionBooleanResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
//    {

//    }

//    public override string ToString() =>
//        this.Response.GetDisplayAttribute();
    
//}


//public class QuestionButtonResponseVM : QuestionResponseVM<SurveryButtonResponseVM>
//{
//    [ValidateComplexType]
//    public override SurveryButtonResponseVM Response { get; set; }
//    public QuestionButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
//    {
//    }

//    public override string ToString() => 
//        this.Response.ToString();
    
//}


//public class QuestionMultiButtonResponseVM : QuestionResponseVM<List<SurveryButtonResponseVM>>
//{
//    [ValidateComplexType]
//    public override List<SurveryButtonResponseVM> Response { get; set; } = new();
//    public QuestionMultiButtonResponseVM(int uniqueQuestionId) : base(uniqueQuestionId)
//    {       

//    }

//    public override string ToString() =>   
//         string.Join(',', this.Response);
    
//}



//public record SurveryButtonResponseVM
//{
//    [ButtonResponseValidator]
//    public SurveyButtonResponseType ButtonType { get; set; }

//    public override string ToString() => 
//        this.ButtonType.GetDisplayAttribute();
    
//}

//public abstract class ParticipantQuestionResponseVM
//{
//    public AnonymousParticipantVM AnonymousParticipant { get; set; }
//    public int UniqueQuestionId { get; protected set; }
    
//    public ParticipantQuestionResponseVM(AnonymousParticipantVM anonymousParticipant)
//    {
//        this.AnonymousParticipant = anonymousParticipant;       
//    }

//    public static ParticipantQuestionResponseVM Create(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM questionResponse) =>
//       questionResponse switch
//       {
//           QuestionRatingResponseVM questionRatingResponse => new ParticipantQuestionResponseVM<SurveyRatingResponseType>(anonymousParticipant, questionRatingResponse),
//           QuestionBooleanResponseVM questionBooleanResponse => new ParticipantQuestionResponseVM<SurveyBooleanResponseType>(anonymousParticipant, questionBooleanResponse),
//           QuestionTextResponseVM questionTextResponse => new ParticipantQuestionResponseVM<string>(anonymousParticipant, questionTextResponse),
//           QuestionMultiButtonResponseVM questionButtonResponse => new ParticipantQuestionResponseVM<List<SurveryButtonResponseVM>>(anonymousParticipant, questionButtonResponse),
//           _ => throw new NotImplementedException(),
//       };
//}

//public class ParticipantQuestionResponseVM<T> : ParticipantQuestionResponseVM
//{
//    public QuestionResponseVM<T> QuestionResponse { get; set; }

//    public ParticipantQuestionResponseVM(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM<T> questionResponse) : base(anonymousParticipant)
//    {
//        this.AnonymousParticipant = anonymousParticipant;
//        this.QuestionResponse = questionResponse;
//        this.UniqueQuestionId = questionResponse.UniqueQuestionId;
//    }
//}

