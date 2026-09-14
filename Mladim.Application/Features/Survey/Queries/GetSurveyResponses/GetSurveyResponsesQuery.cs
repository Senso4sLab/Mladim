using MediatR;
using Mladim.Domain.Dtos.Survey.Responses;

namespace Mladim.Application.Features.Survey.Queries.GetSurveyResponses;

public class GetSurveyResponseQuery : IRequest<IEnumerable<AnonymousSurveyResponseDto>>
{
    public int ActivityId { get; set; }
}
