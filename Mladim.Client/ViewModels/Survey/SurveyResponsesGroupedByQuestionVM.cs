using Mladim.Domain.Enums;
using Mladim.Client.Extensions;
using Mladim.Domain.Extensions;
using CsvHelper;
using System.Linq;
using System.Collections;

namespace Mladim.Client.ViewModels.Survey;


public abstract class SurveyResponsesGroupedByQuestionVM
{
    public string? QuestionDescription { get; set; } = null;
    public string? Question { get; set; } = null;

    public SurveyResponsesGroupedByQuestionVM()
    {
           
    }

    public abstract IEnumerable<string> ResponseTypes();

    public abstract IEnumerable<SurveyParticipantRow> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name);

    public static SurveyResponsesGroupedByQuestionVM Create(SurveyQuestionVM? surveyQuestion, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses)
    {
        return surveyQuestion?.Type switch
        {
            SurveyQuestionType.Text => new SurveyTextResponsesGroupedByQuestion(surveyQuestion.Texts, participantQuestionResponses),
            SurveyQuestionType.Rating => new SurveyRatingResponsesGroupedByQuestion(surveyQuestion.Texts, participantQuestionResponses),
            SurveyQuestionType.Boolean => new SurveyBoleanResponsesGroupedByQuestion(surveyQuestion.Texts, participantQuestionResponses),
            SurveyQuestionType.Multiple => new SurveyButtonGroupResponsesGroupedByQuestion(surveyQuestion.Texts, participantQuestionResponses),
            _ => throw new NotImplementedException()
        };
    }   
}


public abstract class SurveyResponsesGroupedByQuestionVM<T> : SurveyResponsesGroupedByQuestionVM
{

    public List<ParticipantQuestionResponseVM<T>> ParticipantQuestionResponses = new();

    public SurveyResponsesGroupedByQuestionVM(IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
    {
        ParticipantQuestionResponses = participantQuestionResponses.OfType<ParticipantQuestionResponseVM<T>>().ToList();
    }
    public override IEnumerable<SurveyParticipantRow> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name) =>
        new List<SurveyParticipantRow>()
        {
             new SurveyParticipantRow(name,this.Question!, this.ParticipantQuestionResponses
            .Where(pqr => predicate(pqr.AnonymousParticipant))
            .Select(r => r.QuestionResponse.ToString())
            .GroupBy(g => g)
            .Select(g => new ParticipantsPerType(g.Key, g.Count()))
            .UnionBy(this.ResponseTypes().Select(type => new ParticipantsPerType(type, 0)), ppt => ppt.Type)
            .OrderBy(g => g.Type)
            .ToList()),
        };
        
       

}

public record AnonymousCommand(AnonymousParticipantVM Participant, string Comment);

public class SurveyTextResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<string>
{
    public SurveyTextResponsesGroupedByQuestion(IEnumerable<string> questions,
            IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = questions.FirstOrDefault();
    }

    public IEnumerable<AnonymousCommand> GetAnonymousComments()
    {
        return this.ParticipantQuestionResponses.Select(pqr => new AnonymousCommand(pqr.AnonymousParticipant, (pqr.QuestionResponse as QuestionTextResponseVM)!.Response))
            .ToList();
    }

    public override IEnumerable<string> ResponseTypes() =>
        new List<string>();
   
}


public class SurveyRatingResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<SurveyRatingResponseType>
{    
    public SurveyRatingResponsesGroupedByQuestion(IEnumerable<string> questions,
            IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = questions.FirstOrDefault();

    }
    public override IEnumerable<string> ResponseTypes() =>
        Enum.GetValues<SurveyRatingResponseType>()
         .Select(type => type.GetDisplayAttribute())
         .ToList();
}



public record ParticipantsPerType(string Type, int NumOfParticipants);

public record SurveyParticipantRow(string Criterion, string Question, List<ParticipantsPerType> ParticipantsPerType);


public class SurveyBoleanResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<SurveyBooleanResponseType>
{
    public SurveyBoleanResponsesGroupedByQuestion(IEnumerable<string> questions,
             IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = questions.FirstOrDefault();
    }

    public override IEnumerable<string> ResponseTypes() =>
         Enum.GetValues<SurveyBooleanResponseType>()
         .Select(type => type.GetDisplayAttribute())
         .ToList();


}


public class SurveyButtonResponseGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<SurveryButtonResponseVM>
{   
    public SurveyButtonResponseGroupedByQuestion(string question, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
        : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = question; 
    }
    public override IEnumerable<string> ResponseTypes() =>
         Enum.GetValues<SurveyButtonResponseType>()
         .Select(type => type.GetDisplayAttribute()) 
         .ToList();

}



public class SurveyButtonGroupResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<List<SurveryButtonResponseVM>>
{
    public List<SurveyButtonResponseGroupedByQuestion> ButtonGroupResponses { get; set; } = new();
    public SurveyButtonGroupResponsesGroupedByQuestion(IEnumerable<string> questions, 
        IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) :base(participantQuestionResponses)
    {
        this.QuestionDescription = questions.FirstOrDefault();
        this.Question = null;

        var response = questions.Skip(1)
            .Select((q, index) => new SurveyButtonResponseGroupedByQuestion(q,ParticipantQuestionResponses
                .Select(pqr => new ParticipantQuestionResponseVM<SurveryButtonResponseVM>(pqr.AnonymousParticipant,
                    new QuestionResponseVM<SurveryButtonResponseVM>(pqr.QuestionResponse.UniqueQuestionId, pqr.QuestionResponse.Response[index])))));

         ButtonGroupResponses.AddRange(response);
    }

    public override IEnumerable<SurveyParticipantRow> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name) =>
        ButtonGroupResponses.SelectMany(bgr => bgr.NumberOfParticipantsByCriterion(predicate, name)).ToList();
          
    

    public override IEnumerable<string> ResponseTypes() =>
         Enum.GetValues<SurveyButtonResponseType>()
         .Select(type => type.GetDisplayAttribute())
         .ToList();
}






