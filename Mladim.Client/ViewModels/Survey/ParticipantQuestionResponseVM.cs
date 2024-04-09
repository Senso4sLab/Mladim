using Mladim.Client.Validators.SurveyResponseValidators;
using Mladim.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Mladim.Domain.Extensions;
using Mladim.Domain.Models.Survey.Responses;
using Mladim.Domain.Dtos.Survey.Responses;

namespace Mladim.Client.ViewModels.Survey;

public abstract class ParticipantQuestionResponseVM
{
    public AnonymousParticipantVM AnonymousParticipant { get; } = default!;
    public ParticipantQuestionResponseVM(AnonymousParticipantVM anonymousParticipant) => 
        this.AnonymousParticipant = anonymousParticipant;

    public static ParticipantQuestionResponseVM Create(AnonymousParticipantVM participant, QuestionResponseVM response) =>
      response switch
      {
          ISelectableResponse selectedResponse => new ParticipantQuestionResponseVM<ISelectableResponse>(participant, selectedResponse),
          IMultiSelectableResponse multiSelectedResponse => new ParticipantQuestionResponseVM<IMultiSelectableResponse>(participant, multiSelectedResponse),
          ITextResponse textResponse => new ParticipantQuestionResponseVM<ITextResponse>(participant, textResponse),
          _ => throw new NotImplementedException(),
      };
}

public class ParticipantQuestionResponseVM<T> : ParticipantQuestionResponseVM
{  
    public T QuestionResponse { get; } = default!;
    public ParticipantQuestionResponseVM(AnonymousParticipantVM participant, T questionResponse) : base(participant)
    {      
        this.QuestionResponse = questionResponse;
    }
}







