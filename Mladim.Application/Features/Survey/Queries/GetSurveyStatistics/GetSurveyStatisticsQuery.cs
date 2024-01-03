using MediatR;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Models.Survey.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Survey.Queries.GetSurveyResponses;

public class GetSurveyStatisticsQuery : IRequest<IEnumerable<SurveyStatisticsQueryDto>>
{
    public int? OrganizationId { get; set; }
    public int? ProjectId { get; set; }
    public IEnumerable<int> QuestionIds { get; set; } = new List<int>();
}
