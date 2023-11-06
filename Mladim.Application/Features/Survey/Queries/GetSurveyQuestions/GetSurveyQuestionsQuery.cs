using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Survey.Queries.GetSurvey;

public class GetSurveyQuestionsQuery : IRequest<IEnumerable<SurveyQuestionQueryDto>>
{
    public int ActivityId { get; set; }
    public Gender Gender { get;set; }

}
