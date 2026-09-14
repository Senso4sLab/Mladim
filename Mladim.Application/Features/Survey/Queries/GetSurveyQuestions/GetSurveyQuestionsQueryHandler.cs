using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;

using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Enums;

namespace Mladim.Application.Features.Survey.Queries.GetSurvey;

public class GetSurveyQuestionsQueryHandler : IRequestHandler<GetSurveyQuestionsQuery, IEnumerable<SurveyQuestionQueryDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public GetSurveyQuestionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<SurveyQuestionQueryDto>> Handle(GetSurveyQuestionsQuery request, CancellationToken cancellationToken)
    {
        var activity = await UnitOfWork.ActivityRepository.FirstOrDefaultAsync(a => a.Id == request.ActivityId);

        ArgumentNullException.ThrowIfNull(activity);        

        var questionnaire = await UnitOfWork.SurveyQuestionRepository.GetQuestionnaire(activity.Attributes.ActivityTargetGroup, activity.Attributes.GetSurveyQuestionCategory());

        return this.Mapper.Map<IEnumerable<SurveyQuestionQueryDto>>(questionnaire);      

    }

}


