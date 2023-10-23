using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels.Survey;

public class SurveyQuestionVM
{
    public int Id { get; set; }
    public List<string> Texts { get; set; } = new();
    public SurveyQuestionCategory Category { get; set; }
    public SurveyQuestionType Type { get; set; }
}


public class FemaleSurveyQuestionVM : SurveyQuestionVM
{
   
}

public class MaleSurveyQuestionVM : SurveyQuestionVM
{
    
}

