using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Models.Survey.Questions;
using Mladim.Domain.Models.Survey.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Survey.Statistics
{
    public class QuestionResponseStatisticsDto
    {
        public SurveyQuestionQueryDto SurveyQuestion { get; set; }
        public QuestionResponseTypesDto QuestionResponseTypes { get; set; }

    } 
}
