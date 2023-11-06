using MediatR;
using Mladim.Domain.Dtos.Survey.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Survey.Commands.AddSurveyResponse;

public class AddSurveyResponseCommand : IRequest<bool>
{
    public int ActivityId { get; set; }
    public AnonymousSurveyResponseDto AnonymousSurveyResponse { get; set; } = default!;
}
