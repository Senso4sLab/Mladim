using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using Mladim.Domain.Models.Survey.ParticipantResponseTypes;
using Mladim.Domain.Models.Survey.Questions;

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


public abstract class SurveyResponsesGroupedByQuestionVM
{
    public SurveyQuestionVM SurveyQuestion { get; } = default!;
    public SurveyResponsesGroupedByQuestionVM(SurveyQuestionVM surveyQuestion) => 
        this.SurveyQuestion = surveyQuestion;
    public abstract IEnumerable<AnonymousParticipantVM> Participants { get; }

    public static SurveyResponsesGroupedByQuestionVM Create(SurveyQuestionVM question, IEnumerable<ParticipantQuestionResponseVM> responses) =>
        responses switch
        {
            IEnumerable<ParticipantQuestionResponseVM<ISelectableResponse>> selectedResponses => new SurveyResponsesGroupedBySelectableQuestionVM(question, selectedResponses),
            IEnumerable<ParticipantQuestionResponseVM<IMultiSelectableResponse>> multiSelectesResponses => new SurveyResponsesGroupedByMultipleSelectableQuestionVM(question, multiSelectesResponses),
            IEnumerable<ParticipantQuestionResponseVM<ITextResponse>> textResponses => new SurveyTextResponsesGroupedByQuestion(question, textResponses),
            _ => throw new NotImplementedException(),
        };
}

public class SurveyResponsesGroupedByQuestionVM<T> : SurveyResponsesGroupedByQuestionVM
{   
    public List<ParticipantQuestionResponseVM<T>> Responses { get; }
    public override IEnumerable<AnonymousParticipantVM> Participants =>
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
    public IEnumerable<SurveyResponsesGroupedBySelectableQuestionVM> SelectableResponseGroups { get; }
    public SurveyResponsesGroupedByMultipleSelectableQuestionVM(SurveyQuestionVM question, IEnumerable<ParticipantQuestionResponseVM<IMultiSelectableResponse>> responses) :
        base(question, responses) 
    {
        SelectableResponseGroups = ConvertToManySelectableResponseGroup(Responses).ToList();
    }   
    public IEnumerable<ParticipantResponseTypeByCriterion> ResponseTypeByCriterion(string criterion, Predicate<AnonymousParticipantVM> predicate) =>
        this.ConvertToManySelectableResponseGroup(this.Responses)       
            .Select(gqr => gqr.ResponseTypeByCriterion(criterion, predicate))
            .ToList();

    private IEnumerable<SurveyResponsesGroupedBySelectableQuestionVM> ConvertToManySelectableResponseGroup(IEnumerable<ParticipantQuestionResponseVM<IMultiSelectableResponse>> responses) =>    
        this.SurveyQuestion.Texts.Select((q, index) => SurveyResponsesGroupedBySelectableQuestionVM.Create(CreateSelectableQuestion(index), ConvertToSelectableResponses(index)));


    private SurveyQuestionVM CreateSelectableQuestion(int index)
    {
        var surveyQuestion = this.SurveyQuestion.Clone();
        surveyQuestion.Texts = SurveyQuestion.Texts.Skip(index).Take(1).ToList();
        return surveyQuestion;
    }

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

    public UnitSelector(string type) =>
        this.Type = type;
   

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








