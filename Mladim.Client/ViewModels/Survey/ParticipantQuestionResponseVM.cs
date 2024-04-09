using Mladim.Client.Validators.SurveyResponseValidators;
using Mladim.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Mladim.Domain.Extensions;
using Mladim.Domain.Models.Survey.Responses;
using Mladim.Domain.Dtos.Survey.Responses;

namespace Mladim.Client.ViewModels.Survey;

public class ParticipantQuestionResponseVM
{
    public AnonymousParticipantVM AnonymousParticipant { get; } = default!;
    public QuestionResponseVM QuestionResponse { get; } = default!;    

    public ParticipantQuestionResponseVM(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM questionResponse)
    {
        this.AnonymousParticipant = anonymousParticipant;  
        this.QuestionResponse = questionResponse;
    }

    public static ParticipantQuestionResponseVM Create(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM questionResponse) =>
        new ParticipantQuestionResponseVM(anonymousParticipant, questionResponse);


    //public static ParticipantQuestionResponseVM Create(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM questionResponse) => 
    //    questionResponse switch
    //    {
    //        QuestionResponseVM<SurveyRatingResponseType> response => new ParticipantRatingQuestionResponseVM(anonymousParticipant, response),
    //        QuestionResponseVM<SurveyBooleanResponseType> response => new ParticipantQuestionBooleanResponseVM(anonymousParticipant, response),
    //        QuestionResponseVM<SurveryButtonResponseVM> response => new ParticipantQuestionButtonResponseVM(anonymousParticipant, response),
    //        QuestionResponseVM<List<SurveryButtonResponseVM>> response => new ParticipantQuestionMultiButtonResponseVM(anonymousParticipant, response),
    //        QuestionResponseVM<SurveyRepetitiveButtonResponseVM> response => new ParticipantQuestionRepetitiveButtonResponseVM(anonymousParticipant, response),
    //        QuestionResponseVM<List<SurveyRepetitiveButtonResponseVM>> response => new ParticipantQuestionMultiRepetitiveButtonResponseVM(anonymousParticipant, response),            
    //        QuestionResponseVM<string> response => new ParticipantTextQuestionResponseVM(anonymousParticipant, response),
    //        _ => throw new NotImplementedException("Ni implementirana metoda"),
    //    };


    //public static ParticipantQuestionResponseVM Create(AnonymousParticipantVM anonymousParticipant, QuestionResponseVM questionResponse) =>
    //    questionResponse switch
    //    {
    //        QuestionRatingResponseVM rating => new ParticipantRatingQuestionResponseVM(rating, anonymousParticipant),
    //        QuestionBooleanResponseVM boolean => new ParticipantBooleanQuestionResponseVM(boolean, anonymousParticipant),
    //        QuestionMultiRepetitiveButtonResponseVM multiRepetitive => new ParticipantQuestionMultiRepetitiveButtonResponseVM(multiRepetitive.Response, multiRepetitive.UniqueQuestionId, anonymousParticipant),
    //        QuestionMultiButtonResponseVM multiButton => new ParticipantQuestionMultiButtonResponseVM(multiButton.Response, multiButton.UniqueQuestionId, anonymousParticipant),
    //        QuestionTextResponseVM text => new ParticipantTextQuestionResponseVM(text, anonymousParticipant),
    //        _ => throw new NotImplementedException("ParticipantQuestionResponse type does not exist"),           
    //    };

}





//public class ParticipantTextQuestionResponseVM : ParticipantQuestionResponseVM, ITextResponse
//{
//    public ParticipantTextQuestionResponseVM(ITextResponse response, AnonymousParticipantVM anonymousParticipant)
//        : base(response.UniqueQuestionId, anonymousParticipant)
//    {
//        this.Response = response.Response;
//        this.UniqueQuestionId = response.UniqueQuestionId;
//    }

//    public string Response { get; }
   
//}



//public abstract class ParticipantSelectableQuestionResponseVM : ParticipantQuestionResponseVM, ISelectableResponse
//{
//    public Enum ResponseEnum { get; }    

//    public ParticipantSelectableQuestionResponseVM(ISelectableResponse response, AnonymousParticipantVM anonymousParticipant) 
//        : base(response.UniqueQuestionId, anonymousParticipant)
//    {
//        this.ResponseEnum = response.ResponseEnum;       
//    }   
//}

//public class ParticipantRatingQuestionResponseVM : ParticipantSelectableQuestionResponseVM
//{  
//    public ParticipantRatingQuestionResponseVM(ISelectableResponse response, AnonymousParticipantVM anonymousParticipant) 
//        : base(response, anonymousParticipant) {}   
//}

//public class ParticipantBooleanQuestionResponseVM : ParticipantSelectableQuestionResponseVM
//{   
//    public ParticipantBooleanQuestionResponseVM(ISelectableResponse response, AnonymousParticipantVM anonymousParticipant)
//        : base(response, anonymousParticipant) { }
//}


//public class ParticipantQuestionButtonResponseVM : ParticipantSelectableQuestionResponseVM 
//{
//    public ParticipantQuestionButtonResponseVM(ISelectableResponse response, AnonymousParticipantVM anonymousParticipant)
//        : base(response, anonymousParticipant) {}

//    public static ParticipantQuestionButtonResponseVM Create(ISelectableResponse response, AnonymousParticipantVM anonymousParticipant)
//    {
//        return new ParticipantQuestionButtonResponseVM(response, anonymousParticipant);
//    }

//}


//public abstract class ParticipantMultiSelectableQuestionResponseVM : ParticipantQuestionResponseVM, IMultiSelectableResponse
//{     
//    public List<ISelectableResponse> ResponseEnum { get; }
//    public ParticipantMultiSelectableQuestionResponseVM(IEnumerable<ISelectableResponse> responses, int uniqueQuestionId, AnonymousParticipantVM anonymousParticipant)
//        : base(uniqueQuestionId, anonymousParticipant)
//    {
//        this.ResponseEnum = responses.ToList();        
//    }   
//}


//public class ParticipantQuestionMultiButtonResponseVM : ParticipantMultiSelectableQuestionResponseVM
//{     
//    public ParticipantQuestionMultiButtonResponseVM(IEnumerable<ISelectableResponse> responses, int uniqueQuestionId, AnonymousParticipantVM anonymousParticipant)
//        : base(responses, uniqueQuestionId, anonymousParticipant)
//    {
       
//    }    
  
//}

//public class ParticipantQuestionRepetitiveButtonResponseVM : ParticipantSelectableQuestionResponseVM 
//{
//    public ParticipantQuestionRepetitiveButtonResponseVM(ISelectableResponse response, AnonymousParticipantVM anonymousParticipant)
//        : base(response, anonymousParticipant)
//    {
        
//    }

//    public static ParticipantQuestionRepetitiveButtonResponseVM Create(ISelectableResponse selectableResponse, AnonymousParticipantVM anonymousParticipant)
//    {
//        return new ParticipantQuestionRepetitiveButtonResponseVM(selectableResponse, anonymousParticipant);
//    }

//}


//public class ParticipantQuestionMultiRepetitiveButtonResponseVM : ParticipantQuestionMultiButtonResponseVM
//{  
//    public ParticipantQuestionMultiRepetitiveButtonResponseVM(IEnumerable<ISelectableResponse> response, int uniqueQuestionId, AnonymousParticipantVM anonymousParticipant)
//        : base(response, uniqueQuestionId, anonymousParticipant)
//    {      
       
//    }   
//}







