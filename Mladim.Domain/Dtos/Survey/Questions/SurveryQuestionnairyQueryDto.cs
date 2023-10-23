using Mladim.Domain.Models.Survey.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Survey.Questions;

public class SurveyQuestionnairyQueryDto
{
    public int Id { get; set; }
    public List<SurveyQuestionQueryDto> Questions { get; set; } = new();

    public static SurveyQuestionnairyQueryDto Create(int id, IEnumerable<SurveyQuestionQueryDto> surveyQuestions) =>
        new SurveyQuestionnairyQueryDto { Id = id, Questions = surveyQuestions.ToList() };
    
}
