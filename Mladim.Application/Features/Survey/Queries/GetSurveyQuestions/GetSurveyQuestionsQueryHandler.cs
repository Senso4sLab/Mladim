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
        var activity = await this.UnitOfWork.ActivityRepository.FirstOrDefaultAsync(a => a.Id == request.ActivityId && a.SurveyQuestionnairyId != null);

        ArgumentNullException.ThrowIfNull(activity);

        var questionCategory = activity.Attributes.IsGroup ? SurveyQuestionCategory.General | SurveyQuestionCategory.Group : SurveyQuestionCategory.General;

        var questionnairy = await this.UnitOfWork.SurveyQuestionRepository.GetSurveyQuestionnairy(activity.SurveyQuestionnairyId!.Value,request.Gender, questionCategory);

        return this.Mapper.Map<IEnumerable<SurveyQuestionQueryDto>>(questionnairy);      

    }

}


