using Mladim.Domain.Enums;

namespace Mladim.Domain.Models.Survey.Responses;






//public abstract class QuestionResponse
//{   
//    public int UniqueQuestionId { get; set; }
//    public QuestionResponse() { }

//    public QuestionResponse(int uniqueQuestionId)
//    {
//        this.UniqueQuestionId = uniqueQuestionId;
//    }

//}

//public interface ISelectableResponse
//{
//    IEnumerable<Enum> ResponseEnum { get; }
//}
//public interface ITextResponse
//{
//    string Response { get; }
//}

//public abstract class SelectableQuestionResponse : QuestionResponse, ISelectableResponse
//{
//    public abstract IEnumerable<Enum> ResponseEnum { get; }
//    public SelectableQuestionResponse() { }
//    public SelectableQuestionResponse(int uniqueQuestionId): base(uniqueQuestionId) { }
//}



//public class QuestionRatingResponse : SelectableQuestionResponse
//{
//    public SurveyRatingResponseType Response { get; set; }
//    public override IEnumerable<Enum> ResponseEnum =>
//        new Enum[] { this.Response };

//    public QuestionRatingResponse(){}
//    public QuestionRatingResponse(int uniqueQuestionId)
//        : base(uniqueQuestionId)
//    {

//    }
//}


//public class QuestionBooleanResponse : SelectableQuestionResponse
//{
//    public SurveyBooleanResponseType Response { get; set; }
//    public override IEnumerable<Enum> ResponseEnum =>
//        new Enum[] { this.Response };
//    public QuestionBooleanResponse(int uniqueQuestionId)
//        : base(uniqueQuestionId)
//    {

//    }
//}

//public class QuestionButtonResponse : SelectableQuestionResponse
//{
//    public SurveyButtonResponseType Response { get; set; }
//    public override IEnumerable<Enum> ResponseEnum =>
//        new Enum[] { this.Response };
//    public QuestionButtonResponse(int uniqueQuestionId)
//        : base(uniqueQuestionId)
//    {
//    }
//}

//public class MultipleQuestionButtonResponse : SelectableQuestionResponse 
//{
//    public List<QuestionButtonResponse> QuestionResponses { get; set; } = new();
//    public override IEnumerable<Enum> ResponseEnum => 
//        QuestionResponses.SelectMany(qr => qr.ResponseEnum).ToList();
//    public MultipleQuestionButtonResponse(int uniqueQuestionId)
//        : base(uniqueQuestionId)
//    {
//    }
//}

//public class QuestionRepetitiveButtonResponse : SelectableQuestionResponse
//{
//    public SurveyRepetitiveButtonResponseType Response { get; set; }
//    public override IEnumerable<Enum> ResponseEnum =>
//        new Enum[] { this.Response };
//    public QuestionRepetitiveButtonResponse(int uniqueQuestionId)
//        : base(uniqueQuestionId)
//    {
//    }
//}


//public class MultipleQuestionRepetitiveButtonResponse : SelectableQuestionResponse
//{
//    public List<QuestionRepetitiveButtonResponse> QuestionResponses { get; set; } = new();
//    public override IEnumerable<Enum> ResponseEnum =>
//        QuestionResponses.SelectMany(qr => qr.ResponseEnum).ToList();
//    public MultipleQuestionRepetitiveButtonResponse(int uniqueQuestionId)
//        : base(uniqueQuestionId)
//    {
//    }
//}

//public abstract class TextQuestionResponse : QuestionResponse, ITextResponse
//{
//    public abstract string Response { get; }
//    public TextQuestionResponse(int uniqueQuestionId) : base(uniqueQuestionId) { }
//}

//public class QuestionTextResponse : TextQuestionResponse
//{
//    public override string Response { get; } = string.Empty;
//    public QuestionTextResponse(int uniqueQuestionId)
//        : base(uniqueQuestionId)
//    {

//    }
//}





