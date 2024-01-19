using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Dtos.Survey.Statistics;
using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.ParticipantResponseTypes;
using Mladim.Domain.Models.Survey.Questions;
using Mladim.Domain.Models.Survey.Responses;
using Mladim.Domain.Models.Survey.Statistics;
using System.Linq;

namespace Mladim.Application.Features.Survey.Queries.GetSurveyResponses;

public class GetSurveyStatisticsQueryHandler : IRequestHandler<GetSurveyStatisticsQuery, IEnumerable<QuestionResponseStatisticsDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public GetSurveyStatisticsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<QuestionResponseStatisticsDto>> Handle(GetSurveyStatisticsQuery request, CancellationToken cancellationToken)
    {

        IEnumerable<int> questionIds = new List<int>() {1,2,3,4,5,11 };

        if (request.ProjectId is null && request.OrganizationId is null)
            return Enumerable.Empty<QuestionResponseStatisticsDto>();


        var surveyResponses = request.ProjectId is int projectId ? 
                await UnitOfWork.SurveyResponseRepository.GetSurveyResponsesByQuestionIdsAndProjectAsync(projectId) : 
                request.OrganizationId is int organizationId ?
                await UnitOfWork.SurveyResponseRepository.GetSurveyResponsesByQuestionIdsAndOrganizationAsync(organizationId, request.Year) :
                Enumerable.Empty<AnonymousSurveyResponse>().ToList();      


        var questionResponseTypes = surveyResponses.SelectMany(sr => sr.Responses, (sr, qr) => (sr.ActivityId, qr))
           .Where(tuple => questionIds.Any(id => id == tuple.qr.UniqueQuestionId))
           .GroupBy(tuple => tuple.qr.UniqueQuestionId, (questionId, tuples) => (questionId, tuples.Select(tuple => new ActivityQuestionResponse(tuple.ActivityId, tuple.qr))))
           .Select(gtuple => new QuestionResponseTypeSelector(gtuple.questionId, gtuple.Item2))
           .Select(qrts => qrts.AverageQuestionResponseTypes())
           .ToList();

        var surveyQuestions = await UnitOfWork.SurveyQuestionRepository
            .GetSurveyQuestionnairy(1, Gender.Female, SurveyQuestionCategory.General | SurveyQuestionCategory.Group | SurveyQuestionCategory.Repetitive);

        var questionsResponseStatistics = questionResponseTypes.Join(surveyQuestions, qrt => qrt.QuestionId, sq => sq.UniqueQuestionId, (qrt, sq) =>new QuestionResponseStatistics(sq, qrt))
            .Where(qrs => qrs.QuestionResponseTypes.SubQuestionResponseTypes.Count() > 0)            
            .ToList();
       
        return this.Mapper.Map<IEnumerable<QuestionResponseStatisticsDto>>(questionsResponseStatistics);
                
    }


    
}
