using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Projects.Queries.GetProjectDetails;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Survey.Queries.GetSurvey;

public class GetSurveyQueryHandler : IRequestHandler<GetSurveyQuery, SurveyQuestionnairyQueryDto>
{

    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public GetSurveyQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }

    public async Task<SurveyQuestionnairyQueryDto> Handle(GetSurveyQuery request, CancellationToken cancellationToken)
    {
        var activity = await this.UnitOfWork.ActivityRepository.FirstOrDefaultAsync(a => a.Id == request.ActivityId && a.SurveyQuestionnairyId != null);

        ArgumentNullException.ThrowIfNull(activity);

        var questionCategory = activity.Attributes.IsGroup ? SurveyQuestionCategory.General | SurveyQuestionCategory.Group : SurveyQuestionCategory.General;

        var surveyQuestions = await this.UnitOfWork.SurveyRepository.GetSurveyQuestions(request.Gender, questionCategory);

        return SurveyQuestionnairyQueryDto.Create(activity.SurveyQuestionnairyId!.Value, this.Mapper.Map<IEnumerable<SurveyQuestionQueryDto>>(surveyQuestions));      

    }

}


