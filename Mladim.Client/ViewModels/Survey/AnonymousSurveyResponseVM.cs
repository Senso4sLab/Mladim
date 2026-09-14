using Mladim.Domain.Models;

namespace Mladim.Client.ViewModels.Survey;

public class AnonymousSurveyResponseVM
{
    public SurveyParticipantVM AnonymousParticipant { get; set; } = default!;
    public List<QuestionResponseVM> Responses { get; set; } = new();    

    public static AnonymousSurveyResponseVM Create(SurveyParticipantVM participant, IEnumerable<QuestionResponseVM> responses)
    {
        return new AnonymousSurveyResponseVM() { AnonymousParticipant = participant, Responses = responses.ToList() };
    }
}






