using Mladim.Client.Validators.SurveyResponseValidators;
using Mladim.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Mladim.Domain.Extensions;
using Mladim.Domain.Models.Survey.Responses;
using Mladim.Domain.Dtos.Survey.Responses;

namespace Mladim.Client.ViewModels.Survey;



public abstract class ParticipantQuestionResponseVM
{
    public SurveyParticipantVM AnonymousParticipant { get; } = default!;
    public ParticipantQuestionResponseVM(SurveyParticipantVM anonymousParticipant) => 
        this.AnonymousParticipant = anonymousParticipant;

    public static ParticipantQuestionResponseVM Create(SurveyParticipantVM participant, QuestionResponseVM response) =>
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
    public ParticipantQuestionResponseVM(SurveyParticipantVM participant, T questionResponse) : base(participant)
    {      
        this.QuestionResponse = questionResponse; 
    }
}







