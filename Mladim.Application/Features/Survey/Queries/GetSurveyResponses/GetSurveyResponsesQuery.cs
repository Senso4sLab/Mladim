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

public class GetSurveyResponsesQuery : IRequest<IEnumerable<AnonymousSurveyResponseDto>>
{
    public int ActivityId { get; set; }
}
