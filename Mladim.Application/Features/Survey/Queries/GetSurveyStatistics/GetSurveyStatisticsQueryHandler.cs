using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Models.Survey.Responses;

namespace Mladim.Application.Features.Survey.Queries.GetSurveyResponses;

public class GetSurveyStatisticsQueryHandler : IRequestHandler<GetSurveyStatisticsQuery, IEnumerable<SurveyStatisticsQueryDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public GetSurveyStatisticsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<SurveyStatisticsQueryDto>> Handle(GetSurveyStatisticsQuery request, CancellationToken cancellationToken)
    {
        
        
       
        var surveyStatisticsQuery = new List<SurveyStatisticsQueryDto>();
        
        if(request.OrganizationId is int organizationId)
        {           
            var surveyresponses = await UnitOfWork.SurveyResponseRepository.GetSurveyResponseByOrganizationIdAsync(organizationId);
        }
        else if(request.ProjectId is int projectId)
        {           
            var surveyResponses = await UnitOfWork.SurveyResponseRepository.GetSurveyResponseByProjectIdAsync(projectId);

            var groupedResponses = surveyResponses.SelectMany(sr => sr.Responses)
                .Where(r => request.QuestionIds.Any(qId => qId == r.UniqueQuestionId))
                .GroupBy(r => r.UniqueQuestionId)                
                .ToList();
         
           
                
        }

        return surveyStatisticsQuery;
        
        
        //var responses = await this.UnitOfWork.SurveyResponseRepository.GetAllAsync(sr => sr.ActivityId == request.ActivityId);

        //return this.Mapper.Map<IEnumerable<AnonymousSurveyResponseDto>>(responses);
    }


    private void CalculateAverage(IEnumerable<QuestionResponse> responses)
    {

    }



}
