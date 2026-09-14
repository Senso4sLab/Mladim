
using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Domain.Dtos.Survey.Questions;

public class SurveyQuestionQueryDto
{
    public int Id { get; set; }
    public GenderText? Header { get;  set; }
    public List<GenderText> Questions { get; set; } = new();
    public SurveyQuestionCategory Category { get; set; }
    public SurveyQuestionType Type { get; set; }
    public ActivityTargetGroup TargetGroup { get; set; }     
}


