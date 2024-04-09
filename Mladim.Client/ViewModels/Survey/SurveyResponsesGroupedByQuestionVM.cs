using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using Mladim.Domain.Models.Survey.ParticipantResponseTypes;

namespace Mladim.Client.ViewModels.Survey;




public record ParticipantTextResponse(AnonymousParticipantVM Participant, string Content);

public interface ITextableReponseType
{
    IEnumerable<ParticipantTextResponse> GetTextResponse();
}

public interface ISelectableQuestionReponseCalculator
{
    ParticipantResponseTypeByCriterion ResponseTypeByCriterion(string criterion, Predicate<AnonymousParticipantVM> predicate);
}

public interface IMultiSelectableQuestionResponseCalculator
{
    IEnumerable<ParticipantResponseTypeByCriterion> ResponseTypeByCriterion(string criterion, Predicate<AnonymousParticipantVM> predicate);  
}


public class SurveyResponsesGroupedByQuestionVM
{
    public SurveyQuestionVM SurveyQuestionVM { get; } = default!;
    public SurveyResponsesGroupedByQuestionVM(SurveyQuestionVM surveyQuestion) => 
        this.SurveyQuestionVM = surveyQuestion;

    public static SurveyResponsesGroupedByQuestionVM Create(SurveyQuestionVM question, IEnumerable<ParticipantQuestionResponseVM> responses) =>
        responses switch
        {
            IEnumerable<ParticipantQuestionResponseVM<ISelectableResponse>> selectedResponses => new SurveyResponsesGroupedByQuestionVM<ISelectableResponse>(question, selectedResponses),
            IEnumerable<ParticipantQuestionResponseVM<IMultiSelectableResponse>> multiSelectesResponses => new SurveyResponsesGroupedByQuestionVM<IMultiSelectableResponse>(question, multiSelectesResponses),
            IEnumerable<ParticipantQuestionResponseVM<ITextResponse>> textResponses => new SurveyResponsesGroupedByQuestionVM<ITextResponse>(question, textResponses),
            _ => throw new NotImplementedException(),
        };
}

public class SurveyResponsesGroupedByQuestionVM<T> : SurveyResponsesGroupedByQuestionVM
{   
    public List<ParticipantQuestionResponseVM<T>> Responses { get; }
    public IEnumerable<AnonymousParticipantVM> Participants =>
        this.Responses.Select(pqr => pqr.AnonymousParticipant)
       .ToList();
    public SurveyResponsesGroupedByQuestionVM(SurveyQuestionVM surveyQuestion, IEnumerable<ParticipantQuestionResponseVM<T>> responses) : base(surveyQuestion) =>
        this.Responses = responses.ToList();   
    
}

public class SurveyResponsesGroupedBySelectableQuestionVM : SurveyResponsesGroupedByQuestionVM<ISelectableResponse>, ISelectableQuestionReponseCalculator
{ 
    public SurveyResponsesGroupedBySelectableQuestionVM(SurveyQuestionVM question, IEnumerable<ParticipantQuestionResponseVM<ISelectableResponse>> responses) : 
        base(question, responses) { }   

    public ParticipantResponseTypeByCriterion ResponseTypeByCriterion(string criterion, Predicate<AnonymousParticipantVM> predicate) =>
       new ParticipantResponseTypeByCriterion(criterion, ResponseTypesForCriterion(predicate));

    private IEnumerable<ParticipantResponseType> ResponseTypesForCriterion(Predicate<AnonymousParticipantVM> predicate) =>
       this.Responses
           .Where(pqr => predicate(pqr.AnonymousParticipant))
           .GroupBy(pgr => pgr.QuestionResponse.ResponseEnum, (key, sequence) => new ParticipantResponseType(key, sequence.Count()))         
           .ToList();
    public static SurveyResponsesGroupedBySelectableQuestionVM Create(SurveyQuestionVM question, IEnumerable<ParticipantQuestionResponseVM<ISelectableResponse>> responses) => new SurveyResponsesGroupedBySelectableQuestionVM(question, responses);
}

public class SurveyResponsesGroupedByMultipleSelectableQuestionVM : SurveyResponsesGroupedByQuestionVM<IMultiSelectableResponse>, IMultiSelectableQuestionResponseCalculator
{
    public SurveyResponsesGroupedByMultipleSelectableQuestionVM(SurveyQuestionVM question, IEnumerable<ParticipantQuestionResponseVM<IMultiSelectableResponse>> responses) :
        base(question, responses) {}   
    public IEnumerable<ParticipantResponseTypeByCriterion> ResponseTypeByCriterion(string criterion, Predicate<AnonymousParticipantVM> predicate) =>
        this.ConvertToSelectableResponseGroup(this.Responses)       
            .Select(gqr => gqr.ResponseTypeByCriterion(criterion, predicate))
            .ToList();

    private IEnumerable<SurveyResponsesGroupedBySelectableQuestionVM> ConvertToSelectableResponseGroup(IEnumerable<ParticipantQuestionResponseVM<IMultiSelectableResponse>> responses) =>    
        this.SurveyQuestionVM.Texts.Select((q, index) => SurveyResponsesGroupedBySelectableQuestionVM.Create(this.SurveyQuestionVM, ConvertToSelectableResponses(index)));

    private IEnumerable<ParticipantQuestionResponseVM<ISelectableResponse>> ConvertToSelectableResponses(int index) =>    
        Responses.Select(pqr => new ParticipantQuestionResponseVM<ISelectableResponse>(pqr.AnonymousParticipant, pqr.QuestionResponse.ResponseEnum[index]));
    
}


public class SurveyTextResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<ITextResponse>, ITextableReponseType
{    
    public SurveyTextResponsesGroupedByQuestion(SurveyQuestionVM question, IEnumerable<ParticipantQuestionResponseVM<ITextResponse>> responses)
        : base(question, responses) { }     

    public IEnumerable<ParticipantTextResponse> GetTextResponse() =>
        this.Responses
            .Select(pqr => new ParticipantTextResponse(pqr.AnonymousParticipant, pqr.QuestionResponse.Response))
            .ToList();    
}




//public abstract class SurveyResponsesGroupedBySelectableQuestionVM : SurveyResponsesGroupedByQuestionVM, ISelectableQuestionReponseCalculator
//{

//    public List<ParticipantSelectableQuestionResponseVM> ParticipantQuestionResponses = new();

//    public override IEnumerable<AnonymousParticipantVM> AnonymousParticipants =>
//        ParticipantQuestionResponses.Select(pqr => pqr.AnonymousParticipant)
//        .ToList();

//    public SurveyResponsesGroupedBySelectableQuestionVM(IEnumerable<ParticipantSelectableQuestionResponseVM> participantQuestionResponses, int questionId) : base(questionId)
//    {
//        this.ParticipantQuestionResponses = participantQuestionResponses.ToList();        
//    }

//    public abstract IEnumerable<Enum> AvailableResponseTypes { get; }

//    public ParticipantResponseTypeByCriterion ParticipantsResponseTypeForCriterion(string criterionName, Predicate<AnonymousParticipantVM> predicate) =>
//       new ParticipantResponseTypeByCriterion(criterionName, ResponseTypesForCriterion(predicate));


//    private IEnumerable<ParticipantResponseType> ResponseTypesForCriterion(Predicate<AnonymousParticipantVM> predicate) =>
//       this.ParticipantQuestionResponses
//          .Where(pqr => predicate(pqr.AnonymousParticipant))
//          .GroupBy(pgr => pgr.ResponseEnum, (key, sequence) => new ParticipantResponseType(key, sequence.Count()))
//          .UnionBy(AvailableResponseTypes.Select(ParticipantResponseType.Zero), prt => prt.ResponseType)
//          .ToList();
//}



//public abstract class SurveyResponsesGroupedByMultipleSelectableQuestionVM : SurveyResponsesGroupedByQuestionVM, IMultiSelectableQuestionResponseCalculator
//{
//    public abstract List<SurveyResponsesGroupedBySelectableQuestionVM> GroupedByQuestionResponses { get; }

//    public override IEnumerable<AnonymousParticipantVM> AnonymousParticipants =>
//        this.GroupedByQuestionResponses
//            .SelectMany(pqr => pqr.AnonymousParticipants)
//            .ToList();

//    public SurveyResponsesGroupedByMultipleSelectableQuestionVM(int questionId) : base(questionId)
//    {        
//    }
//    public abstract IEnumerable<Enum> AvailableResponseTypes { get; }
//    public IEnumerable<ParticipantResponseTypeByCriterion> ParticipantsByQuestion(string name, Predicate<AnonymousParticipantVM> predicate) =>
//        this.GroupedByQuestionResponses
//            .Select(gqr => gqr.ParticipantsResponseTypeForCriterion(name, predicate))
//            .ToList();   

//}









//public class SurveyBoleanResponsesGroupedByQuestion : SurveyResponsesGroupedBySelectableQuestionVM
//{
//    public SurveyBoleanResponsesGroupedByQuestion(SurveyQuestionVM question, IEnumerable<ParticipantBooleanQuestionResponseVM> participantQuestionResponses)
//        : base(participantQuestionResponses, question.UniqueQuestionId)
//    {
//        this.Content = null;
//        this.SubContent = question.Texts.FirstOrDefault();
//    }

//    public override IEnumerable<Enum> AvailableResponseTypes => 
//        Enum.GetValues<SurveyBooleanResponseType>().OfType<Enum>().ToList(); 
//}

//public class SurveyRatingResponsesGroupedByQuestion : SurveyResponsesGroupedBySelectableQuestionVM
//{
//    public SurveyRatingResponsesGroupedByQuestion(SurveyQuestionVM question, IEnumerable<ParticipantRatingQuestionResponseVM> participantQuestionResponses)
//        : base(participantQuestionResponses, question.UniqueQuestionId)
//    {
//        this.Content = null;
//        this.SubContent = question.Texts.FirstOrDefault();
//    }

//    public override IEnumerable<Enum> AvailableResponseTypes =>
//        Enum.GetValues<SurveyRatingResponseType>().OfType<Enum>().ToList();
//}

//public class SurveyButtonResponseGroupedByQuestion : SurveyResponsesGroupedBySelectableQuestionVM
//{
//    public SurveyButtonResponseGroupedByQuestion(string content, int questionId, IEnumerable<ParticipantQuestionButtonResponseVM> participantQuestionResponses)
//        : base(participantQuestionResponses, questionId)
//    {
//        this.Content = null;
//        this.SubContent = content;
//    }
//    public override IEnumerable<Enum> AvailableResponseTypes => Enum.GetValues<SurveyButtonResponseType>().OfType<Enum>().ToList();
//}
//public class SurveyButtonResponsesGroupedByQuestion : SurveyResponsesGroupedByMultipleSelectableQuestionVM
//{
//    public override List<SurveyResponsesGroupedBySelectableQuestionVM> GroupedByQuestionResponses { get; } = new List<SurveyResponsesGroupedBySelectableQuestionVM>();

//    public SurveyButtonResponsesGroupedByQuestion(SurveyQuestionVM question, IEnumerable<ParticipantQuestionMultiButtonResponseVM> participantQuestionResponses)
//        : base(question.UniqueQuestionId)
//    {
//        this.Content = question.Texts.FirstOrDefault();
//        this.SubContent = null;

//        var response = question.Texts.Skip(1)
//            .Select((q, index) => new SurveyButtonResponseGroupedByQuestion(q, question.UniqueQuestionId,
//                participantQuestionResponses.Select(pqr => ParticipantQuestionButtonResponseVM.Create(pqr.ResponseEnum[index], pqr.AnonymousParticipant))));              

//        this.GroupedByQuestionResponses.AddRange(response);
//    }
//    public override IEnumerable<Enum> AvailableResponseTypes =>
//        Enum.GetValues<SurveyButtonResponseType>().OfType<Enum>().ToList();
//}

//public class SurveyRepetitiveButtonResponseGroupedByQuestion : SurveyResponsesGroupedBySelectableQuestionVM
//{
//    public SurveyRepetitiveButtonResponseGroupedByQuestion(string question, int questionId, IEnumerable<ParticipantQuestionRepetitiveButtonResponseVM> participantQuestionResponses)
//        : base(participantQuestionResponses, questionId)
//    {
//        this.Content = null;
//        this.SubContent = question;
//    }   

//    public override IEnumerable<Enum> AvailableResponseTypes => 
//        Enum.GetValues<SurveyRepetitiveButtonResponseType>().OfType<Enum>(); 
//}


//public class SurveyRepetitiveButtonGroupResponsesGroupedByQuestion : SurveyResponsesGroupedByMultipleSelectableQuestionVM
//{
//    public override List<SurveyResponsesGroupedBySelectableQuestionVM> GroupedByQuestionResponses { get; } = new List<SurveyResponsesGroupedBySelectableQuestionVM>();

//    public SurveyRepetitiveButtonGroupResponsesGroupedByQuestion(SurveyQuestionVM question, IEnumerable<ParticipantQuestionMultiRepetitiveButtonResponseVM> participantQuestionResponses)
//        : base(question.UniqueQuestionId)
//    {
//        this.Content = question.Texts.FirstOrDefault();
//        this.SubContent = null;

//        var response = question.Texts.Skip(1)
//            .Select((q, index) => new SurveyRepetitiveButtonResponseGroupedByQuestion(q, question.UniqueQuestionId,
//                participantQuestionResponses.Select(pqr => ParticipantQuestionRepetitiveButtonResponseVM.Create(pqr.ResponseEnum[index], pqr.AnonymousParticipant))));

//        this.GroupedByQuestionResponses.AddRange(response);
//    }
//    public override IEnumerable<Enum> AvailableResponseTypes =>
//        Enum.GetValues<SurveyRepetitiveButtonResponseType>().OfType<Enum>().ToList();
//}







public class CogntigencyTableContext
{
    public UnitSelector UnitSelector { get; set; } = UnitSelector.PercentagesAsUnit();
    public SurveyCriterionSelector CriterionSelector { get; set; } = SurveyCriterionSelector.GenderSelector();

    public ContingencyTable GetContingencyTableFor(ISelectableQuestionReponseCalculator responses) =>    
        new ContingencyTable(CriterionSelector.Name, GetParticipantsByResponseTypes(responses, CriterionSelector.ParticipantPredicatesByType));
    
    private IEnumerable<ParticipantResponseTypeByCriterion> GetParticipantsByResponseTypes(ISelectableQuestionReponseCalculator responses, IEnumerable<ParticipantPredicate> predicates) =>
       UnitSelector.ResponseTypeByCriteria(responses, predicates); 

}



public abstract class UnitSelector
{
    public string Type { get; }
    
    public UnitSelector(string type)
    {        
       this.Type = type;      
    }

    public static UnitSelector ParticipantsAsUnit() =>
        new NumOfParticipantsUnit();

    public static UnitSelector PercentagesAsUnit() =>
        new PercantagesUnit();

    public IEnumerable<ParticipantResponseTypeByCriterion> ResponseTypeByCriteria(ISelectableQuestionReponseCalculator calculator, IEnumerable<ParticipantPredicate> predicates) =>
        ResponseTypeInSelectedUnit(predicates.Select(pp => calculator.ResponseTypeByCriterion(pp.Name, pp.Predicate)));                

    protected abstract IEnumerable<ParticipantResponseTypeByCriterion> ResponseTypeInSelectedUnit(IEnumerable<ParticipantResponseTypeByCriterion> responseTypeByCriteria);


}

public class NumOfParticipantsUnit : UnitSelector
{   
    public NumOfParticipantsUnit() : base("Frekvenca/število") {}
    protected override IEnumerable<ParticipantResponseTypeByCriterion> ResponseTypeInSelectedUnit(IEnumerable<ParticipantResponseTypeByCriterion> responseTypeByCriteria) =>
        responseTypeByCriteria.ToList();
  
}

public class PercantagesUnit : UnitSelector
{
    public PercantagesUnit()
        :base("Odstotki")
    {

    }
    private float NumberOfParticipants(IEnumerable<ParticipantResponseTypeByCriterion> responseTypeByCriteria) =>
      responseTypeByCriteria.SelectMany(prc => prc.ReponseTypesPerCriterion).Sum(pr => pr.Value);

  
    private ParticipantResponseType ToPercent(ParticipantResponseType reponseType, float numParticipants) =>
        new ParticipantResponseType(reponseType.ResponseType, (float)Math.Round((reponseType.Value * 100 / numParticipants), 1));
    

    protected override IEnumerable<ParticipantResponseTypeByCriterion> ResponseTypeInSelectedUnit(IEnumerable<ParticipantResponseTypeByCriterion> responseTypeByCriteria)
    {
        var numOfParticipants = NumberOfParticipants(responseTypeByCriteria);
        return responseTypeByCriteria.Select(prt => new ParticipantResponseTypeByCriterion(prt.Criterion, ResponseTypeInSelectedUnit(prt, numOfParticipants))).ToList();
    }
    
    private IEnumerable<ParticipantResponseType> ResponseTypeInSelectedUnit(ParticipantResponseTypeByCriterion responseType, float numOfParticipants)
    {
        return responseType.ReponseTypesPerCriterion.Select(prt => ToPercent(prt, numOfParticipants));
    }



}








