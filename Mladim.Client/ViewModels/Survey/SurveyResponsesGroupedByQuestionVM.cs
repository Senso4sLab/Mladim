using Mladim.Domain.Enums;
using Mladim.Client.Extensions;
using Mladim.Domain.Extensions;
using CsvHelper;
using System.Linq;

namespace Mladim.Client.ViewModels.Survey;


public abstract class SurveyResponsesGroupedByQuestionVM
{
    public string Question { get; set; }

    public SurveyResponsesGroupedByQuestionVM(string question)
    {
        this.Question = question;
    }   
}


public abstract class SurveyResponsesGroupedByQuestionVM<T> : SurveyResponsesGroupedByQuestionVM
{ 
    
    public List<ParticipantQuestionResponseVM<T>> ParticipantQuestionResponses = new();

    public SurveyResponsesGroupedByQuestionVM(string question, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) : base(question)
    {
        ParticipantQuestionResponses = participantQuestionResponses.OfType<ParticipantQuestionResponseVM<T>>().ToList();
    }

    protected virtual IEnumerable<string> Types() => new List<string>();

    //public virtual SurveyParticipantRow NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name) =>
    //     new SurveyParticipantRow(name, this.ParticipantQuestionResponses
    //        .Where(pqr => predicate(pqr.AnonymousParticipant))
    //        .Select(r => r.QuestionResponse)
    //        .GroupBy(g => g.Response)
    //        .Select(g => new ParticipantsPerType<T>(g.Key, g.Count()))
    //        .UnionBy(Types(), ppt => ppt.Type)
    //        .OrderBy(g => g.Type)
    //        .ToList());
    public static SurveyResponsesGroupedByQuestionVM Create(SurveyQuestionVM? surveyQuestion, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses)
    {
        return surveyQuestion?.Type switch
        {
            SurveyQuestionType.Text => new SurveyTextResponsesGroupedByQuestion(surveyQuestion.Texts.FirstOrDefault(), participantQuestionResponses),
            SurveyQuestionType.Rating => new SurveyRatingResponsesGroupedByQuestion(surveyQuestion.Texts.FirstOrDefault(), participantQuestionResponses),
            SurveyQuestionType.Boolean => new SurveyBoleanResponsesGroupedByQuestion(surveyQuestion.Texts.FirstOrDefault(), participantQuestionResponses),
            SurveyQuestionType.Multiple => new SurveyButtonGroupResponsesGroupedByQuestion(surveyQuestion.Texts, participantQuestionResponses),
            _ => throw new NotImplementedException()
        };
    }
}

public record AnonymousCommand(AnonymousParticipantVM Participant, string Comment);

public class SurveyTextResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<string>
{
    public SurveyTextResponsesGroupedByQuestion(string question,
            IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) : base(question, participantQuestionResponses)
    {

    }

    public IEnumerable<AnonymousCommand> GetAnonymousComments()
    {
        return this.ParticipantQuestionResponses.Select(pqr => new AnonymousCommand(pqr.AnonymousParticipant, (pqr.QuestionResponse as QuestionTextResponseVM)!.Response))
            .ToList();
    }


    

    //public override SurveyParticipantRow<string> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name) =>
    //    new SurveyParticipantRow<string>(string.Empty, new List<ParticipantsPerType<string>>());
    
   
}


public class SurveyRatingResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<SurveyRatingResponseType>
{
    public SurveyRatingResponsesGroupedByQuestion(string question,
            IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) : base(question, participantQuestionResponses)
    {

    }


    protected override IEnumerable<string> Types() => 
        Enum.GetNames<SurveyRatingResponseType>()
        .ToList();
    


}



public record ParticipantsPerType(string Type, int NumOfParticipants);

public record SurveyParticipantRow(string Criterion, List<ParticipantsPerType> ParticipantsPerType);


public class SurveyBoleanResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<SurveyBooleanResponseType>
{
    public SurveyBoleanResponsesGroupedByQuestion(string question,
             IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) : base(question, participantQuestionResponses)
    {

    }

    protected override IEnumerable<string> Types() =>
        Enum.GetNames<SurveyBooleanResponseType>()
        .ToList();


}


public class SurveyButtonResponseGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<SurveryButtonResponseVM>
{   
    public SurveyButtonResponseGroupedByQuestion(string question, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
        : base(question, participantQuestionResponses)
    {
       
    }
    protected override IEnumerable<string> Types() =>
         Enum.GetNames<SurveyButtonResponseType>()
         .ToList();

}



public class SurveyButtonGroupResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<List<SurveyButtonResponseGroupedByQuestion>>
{
    public List<SurveyButtonResponseGroupedByQuestion> ButtonGroupResponses { get; set; } = new();
    public SurveyButtonGroupResponsesGroupedByQuestion(IEnumerable<string> questions, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) :
        base(questions.First(), participantQuestionResponses)
    {

        var surveyButtonResponses = ParticipantQuestionResponses.Select((ro, outer) => ro.QuestionResponse.Response.Select((y, inner) =>) )
            
            
            SelectMany((pqr, index) => pqr.QuestionResponse.Response, (pqr, response) => new {Ap = pqr.AnonymousParticipant, Response = response});
        



        var surveyButtonResponses = ParticipantQuestionResponses.Select((ro, index) => ParticipantQuestionResponses.Select(ri =>
            new ParticipantQuestionResponseVM<SurveyButtonResponseGroupedByQuestion>(ro.AnonymousParticipant,
                new QuestionResponseVM<SurveyButtonResponseGroupedByQuestion>(ri.QuestionResponse.UniqueQuestionId, ri.QuestionResponse.Response[index]))))
            .Merge(questions.Skip(1))
            .Select(tuple => new SurveyButtonResponseGroupedByQuestion(tuple.element2, tuple.element1));

        ButtonGroupResponses.AddRange(surveyButtonResponses);
    }

    //public override SurveyParticipantRow<List<SurveyButtonResponseGroupedByQuestion>> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name) 
    //{

    //    var ss = ButtonGroupResponses.Select(x => x.NumberOfParticipantsByCriterion(predicate, name))            
    //        .SelectMany(s => s.ParticipantsPerType)
    //        .GroupBy(s => s.Type)
    //        .Select(g => new ParticipantsPerType<>(g.Key, g.Sum(s => s.NumOfParticipants)))
    //        .ToList());



    //    return new SurveyParticipantRow<List<SurveyButtonResponseGroupedByQuestion>>(name, )
    //}
        //new SurveyParticipantRow(name, ParticipantQuestionResponses.Select(x => x.Number   NumberOfParticipantsByCriterion(predicate, name))
        //    .SelectMany(s => s.ParticipantsPerType)
        //    .GroupBy(s => s.Type)
        //    .Select(g => new ParticipantsPerType(g.Key, g.Sum(s => s.NumOfParticipants)))
        //    .ToList());
}






