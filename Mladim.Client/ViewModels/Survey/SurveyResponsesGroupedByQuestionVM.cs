using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.ParticipantResponseTypes;

namespace Mladim.Client.ViewModels.Survey;


public interface ITextableReponseType
{
    IEnumerable<ParticipantTextResponse> GetParticipantTextResponse();
}

public interface ISelectableQuestionReponseCalculator
{
    ParticipantResponseTypeByCriterion ParticipantsResponseTypeForCriterion(string criterionName, Predicate<AnonymousParticipantVM> predicate);
    IEnumerable<Enum> AvailableResponseTypes { get; }
   
}

public interface IMultiSelectableQuestionResponseCalculator
{
    IEnumerable<ParticipantResponseTypeByCriterion> ParticipantsByQuestion(string criterionName, Predicate<AnonymousParticipantVM> predicate);

    IEnumerable<Enum> AvailableResponseTypes { get; }
}




public abstract class SurveyResponsesGroupedByQuestionVM
{
    public string? Content { get; set; } = null;
    public string? SubContent { get; set; } = null;
    public int UniqueQuestionId { get; set; }
    public abstract IEnumerable<AnonymousParticipantVM> AnonymousParticipants { get; }

    public SurveyResponsesGroupedByQuestionVM(int questionId) =>
        this.UniqueQuestionId = questionId;   

    public static SurveyResponsesGroupedByQuestionVM Create(SurveyQuestionVM? surveyQuestion, IEnumerable<ParticipantQuestionResponseVM> pResponse)
    {
        return surveyQuestion?.Type switch
        {
            SurveyQuestionType.Text => new SurveyTextResponsesGroupedByQuestion(surveyQuestion, pResponse.OfType<ParticipantTextQuestionResponseVM>()),
            SurveyQuestionType.Rating => new SurveyRatingResponsesGroupedByQuestion(surveyQuestion, pResponse.OfType<ParticipantRatingQuestionResponseVM>()),
            SurveyQuestionType.Boolean  => new SurveyBoleanResponsesGroupedByQuestion(surveyQuestion, pResponse.OfType<ParticipantBooleanQuestionResponseVM>()),
            SurveyQuestionType.Multiple  => new SurveyButtonResponsesGroupedByQuestion(surveyQuestion, pResponse.OfType<ParticipantQuestionMultiButtonResponseVM>()),
            SurveyQuestionType.MultipleRepetitive => new SurveyRepetitiveButtonGroupResponsesGroupedByQuestion(surveyQuestion, pResponse.OfType<ParticipantQuestionMultiRepetitiveButtonResponseVM>()),         
            _ => throw new NotImplementedException()
        };
    }   
}

public abstract class SurveyResponsesGroupedBySelectableQuestionVM : SurveyResponsesGroupedByQuestionVM, ISelectableQuestionReponseCalculator
{

    public List<ParticipantSelectableQuestionResponseVM> ParticipantQuestionResponses = new();

    public override IEnumerable<AnonymousParticipantVM> AnonymousParticipants =>
        ParticipantQuestionResponses.Select(pqr => pqr.AnonymousParticipant)
        .ToList();
    
    public SurveyResponsesGroupedBySelectableQuestionVM(IEnumerable<ParticipantSelectableQuestionResponseVM> participantQuestionResponses, int questionId) : base(questionId)
    {
        this.ParticipantQuestionResponses = participantQuestionResponses.ToList();        
    }

    public abstract IEnumerable<Enum> AvailableResponseTypes { get; }

    public ParticipantResponseTypeByCriterion ParticipantsResponseTypeForCriterion(string criterionName, Predicate<AnonymousParticipantVM> predicate) =>
       new ParticipantResponseTypeByCriterion(criterionName, ResponseTypesForCriterion(predicate));


    private IEnumerable<ParticipantResponseType> ResponseTypesForCriterion(Predicate<AnonymousParticipantVM> predicate) =>
       this.ParticipantQuestionResponses
          .Where(pqr => predicate(pqr.AnonymousParticipant))
          .GroupBy(pgr => pgr.ResponseEnum, (key, sequence) => new ParticipantResponseType(key, sequence.Count()))
          .UnionBy(AvailableResponseTypes.Select(ParticipantResponseType.Zero), prt => prt.ResponseType)
          .ToList();
}



public abstract class SurveyResponsesGroupedByMultipleSelectableQuestionVM : SurveyResponsesGroupedByQuestionVM, IMultiSelectableQuestionResponseCalculator
{
    public abstract List<SurveyResponsesGroupedBySelectableQuestionVM> GroupedByQuestionResponses { get; }

    public override IEnumerable<AnonymousParticipantVM> AnonymousParticipants =>
        this.GroupedByQuestionResponses
            .SelectMany(pqr => pqr.AnonymousParticipants)
            .ToList();

    public SurveyResponsesGroupedByMultipleSelectableQuestionVM(int questionId) : base(questionId)
    {        
    }
    public abstract IEnumerable<Enum> AvailableResponseTypes { get; }
    public IEnumerable<ParticipantResponseTypeByCriterion> ParticipantsByQuestion(string name, Predicate<AnonymousParticipantVM> predicate) =>
        this.GroupedByQuestionResponses
            .Select(gqr => gqr.ParticipantsResponseTypeForCriterion(name, predicate))
            .ToList();   

}




public record ParticipantTextResponse(AnonymousParticipantVM Participant, string Content);

public class SurveyTextResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM, ITextableReponseType
{
    private List<ParticipantTextQuestionResponseVM> ParticipantQuestionResponses = new();
    public SurveyTextResponsesGroupedByQuestion(SurveyQuestionVM surveyQuestion, IEnumerable<ParticipantTextQuestionResponseVM> participantResponses) 
        : base(surveyQuestion.UniqueQuestionId)
    {
        this.Content = null;
        this.SubContent = surveyQuestion.Texts.FirstOrDefault();
        this.ParticipantQuestionResponses = participantResponses.ToList();
    }

    public override IEnumerable<AnonymousParticipantVM> AnonymousParticipants => 
        this.ParticipantQuestionResponses.Select(pqr => pqr.AnonymousParticipant).ToList();

    public IEnumerable<ParticipantTextResponse> GetParticipantTextResponse() =>    
        this.ParticipantQuestionResponses.Select(pqr => new ParticipantTextResponse(pqr.AnonymousParticipant, pqr.Response)).ToList();  
}


public class SurveyBoleanResponsesGroupedByQuestion : SurveyResponsesGroupedBySelectableQuestionVM
{
    public SurveyBoleanResponsesGroupedByQuestion(SurveyQuestionVM question, IEnumerable<ParticipantBooleanQuestionResponseVM> participantQuestionResponses)
        : base(participantQuestionResponses, question.UniqueQuestionId)
    {
        this.Content = null;
        this.SubContent = question.Texts.FirstOrDefault();
    }

    public override IEnumerable<Enum> AvailableResponseTypes => 
        Enum.GetValues<SurveyBooleanResponseType>().OfType<Enum>().ToList(); 
}

public class SurveyRatingResponsesGroupedByQuestion : SurveyResponsesGroupedBySelectableQuestionVM
{
    public SurveyRatingResponsesGroupedByQuestion(SurveyQuestionVM question, IEnumerable<ParticipantRatingQuestionResponseVM> participantQuestionResponses)
        : base(participantQuestionResponses, question.UniqueQuestionId)
    {
        this.Content = null;
        this.SubContent = question.Texts.FirstOrDefault();
    }

    public override IEnumerable<Enum> AvailableResponseTypes =>
        Enum.GetValues<SurveyRatingResponseType>().OfType<Enum>().ToList();
}

public class SurveyButtonResponseGroupedByQuestion : SurveyResponsesGroupedBySelectableQuestionVM
{
    public SurveyButtonResponseGroupedByQuestion(string content, int questionId, IEnumerable<ParticipantQuestionButtonResponseVM> participantQuestionResponses)
        : base(participantQuestionResponses, questionId)
    {
        this.Content = null;
        this.SubContent = content;
    }
    public override IEnumerable<Enum> AvailableResponseTypes => Enum.GetValues<SurveyButtonResponseType>().OfType<Enum>().ToList();
}
public class SurveyButtonResponsesGroupedByQuestion : SurveyResponsesGroupedByMultipleSelectableQuestionVM
{
    public override List<SurveyResponsesGroupedBySelectableQuestionVM> GroupedByQuestionResponses { get; } = new List<SurveyResponsesGroupedBySelectableQuestionVM>();

    public SurveyButtonResponsesGroupedByQuestion(SurveyQuestionVM question, IEnumerable<ParticipantQuestionMultiButtonResponseVM> participantQuestionResponses)
        : base(question.UniqueQuestionId)
    {
        this.Content = question.Texts.FirstOrDefault();
        this.SubContent = null;

        var response = question.Texts.Skip(1)
            .Select((q, index) => new SurveyButtonResponseGroupedByQuestion(q, question.UniqueQuestionId,
                participantQuestionResponses.Select(pqr => ParticipantQuestionButtonResponseVM.Create(pqr.ResponseEnum[index], pqr.AnonymousParticipant))));              

        this.GroupedByQuestionResponses.AddRange(response);
    }
    public override IEnumerable<Enum> AvailableResponseTypes =>
        Enum.GetValues<SurveyButtonResponseType>().OfType<Enum>().ToList();
}

public class SurveyRepetitiveButtonResponseGroupedByQuestion : SurveyResponsesGroupedBySelectableQuestionVM
{
    public SurveyRepetitiveButtonResponseGroupedByQuestion(string question, int questionId, IEnumerable<ParticipantQuestionRepetitiveButtonResponseVM> participantQuestionResponses)
        : base(participantQuestionResponses, questionId)
    {
        this.Content = null;
        this.SubContent = question;
    }   

    public override IEnumerable<Enum> AvailableResponseTypes => 
        Enum.GetValues<SurveyRepetitiveButtonResponseType>().OfType<Enum>(); 
}


public class SurveyRepetitiveButtonGroupResponsesGroupedByQuestion : SurveyResponsesGroupedByMultipleSelectableQuestionVM
{
    public override List<SurveyResponsesGroupedBySelectableQuestionVM> GroupedByQuestionResponses { get; } = new List<SurveyResponsesGroupedBySelectableQuestionVM>();

    public SurveyRepetitiveButtonGroupResponsesGroupedByQuestion(SurveyQuestionVM question, IEnumerable<ParticipantQuestionMultiRepetitiveButtonResponseVM> participantQuestionResponses)
        : base(question.UniqueQuestionId)
    {
        this.Content = question.Texts.FirstOrDefault();
        this.SubContent = null;

        var response = question.Texts.Skip(1)
            .Select((q, index) => new SurveyRepetitiveButtonResponseGroupedByQuestion(q, question.UniqueQuestionId,
                participantQuestionResponses.Select(pqr => ParticipantQuestionRepetitiveButtonResponseVM.Create(pqr.ResponseEnum[index], pqr.AnonymousParticipant))));

        this.GroupedByQuestionResponses.AddRange(response);
    }
    public override IEnumerable<Enum> AvailableResponseTypes =>
        Enum.GetValues<SurveyRepetitiveButtonResponseType>().OfType<Enum>().ToList();
}


//public record ParticipantResponseTypesPerQuestion
//{
//    public IEnumerable<ParticipantResponseType> ParticipantResponseTypes { get; }
//    public int UniqueQuestionId { get; set; }
//    public ParticipantResponseTypesPerQuestion(int uniqueQuestionId, IEnumerable<ParticipantResponseType> participantResonseTypes)
//    {
//        this.UniqueQuestionId = uniqueQuestionId;
//        this.ParticipantResponseTypes = participantResonseTypes.ToList();
//    }

//    public static ParticipantResponseTypesPerQuestion Create(int uniqueQuestionId, IEnumerable<ParticipantResponseType> participantResonseTypes)
//    {
//        return new ParticipantResponseTypesPerQuestion(uniqueQuestionId, participantResonseTypes);
//    }

//    public ParticipantResponseTypesPerQuestion ToPercent(float numOfParticipants) =>
//       new ParticipantResponseTypesPerQuestion(this.UniqueQuestionId, ParticipantResponseTypes.Select(prt => prt.ToPercent(numOfParticipants)));
//}







public class CogntigencyTableContext
{
    public UnitSelector UnitSelector { get; set; } = UnitSelector.PercentagesAsUnit();
    public SurveyCriterionSelector CriterionSelector { get; set; } = SurveyCriterionSelector.GenderSelector();  

    public ContingencyTable GetContingencyTableFor(ISelectableQuestionReponseCalculator responses)
    {
        return new ContingencyTable(CriterionSelector.Name, GetParticipantsByResponseTypes(responses, CriterionSelector.ParticipantPredicatesByType));
    }
    private IEnumerable<ParticipantResponseTypeByCriterion> GetParticipantsByResponseTypes(ISelectableQuestionReponseCalculator responses, IEnumerable<ParticipantPredicate> participantPredicates) =>
      participantPredicates.Select(pp => GetParticipantsByResponseTypes(responses, pp))
          .ToList();
    
    public ParticipantResponseTypeByCriterion GetParticipantsByResponseTypes(ISelectableQuestionReponseCalculator responses, ParticipantPredicate participantPredicate) =>
         UnitSelector.ParticipantCalculationForCriterion(responses, participantPredicate);

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

    public abstract ParticipantResponseTypeByCriterion ParticipantCalculationForCriterion(ISelectableQuestionReponseCalculator responses, ParticipantPredicate participantPredicate);


}

public class NumOfParticipantsUnit : UnitSelector
{   
    public NumOfParticipantsUnit() 
        : base("Frekvenca/število")
    {
       
    }

    public override ParticipantResponseTypeByCriterion ParticipantCalculationForCriterion(ISelectableQuestionReponseCalculator responses, ParticipantPredicate pPredicate)
    {
        return responses.ParticipantsResponseTypeForCriterion(pPredicate.Name, pPredicate.Predicate);  
    }
}

public class PercantagesUnit : UnitSelector
{
    public PercantagesUnit()
        :base("Odstotki")
    {
        
    }
    private float NumOfParticipants(ISelectableQuestionReponseCalculator responses) =>
        responses.ParticipantsResponseTypeForCriterion(ParticipantPredicate.None.Name, ParticipantPredicate.None.Predicate)
        .ReponseTypesPerCriterion.Sum(rt => rt.Value);

    public override ParticipantResponseTypeByCriterion ParticipantCalculationForCriterion(ISelectableQuestionReponseCalculator responses, ParticipantPredicate pPredicate)
    {
        var numOfParticipants = NumOfParticipants(responses);

        return responses.ParticipantsResponseTypeForCriterion(pPredicate.Name, pPredicate.Predicate)
            .ToPercent(numOfParticipants);       
    }
}








