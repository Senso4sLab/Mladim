using MediatR;
using Mladim.Domain.Dtos.Survey.Statistics;

namespace Mladim.Application.Features.Survey.Queries.GetSurveyResponses;

public class GetSurveyStatisticsQuery : IRequest<IEnumerable<QuestionSurveyStatisticsDto>>
{
    public int? OrganizationId { get; set; }
    public int? ProjectId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }   
}
