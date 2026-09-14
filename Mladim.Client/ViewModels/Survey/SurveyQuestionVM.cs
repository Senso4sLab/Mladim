using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Client.ViewModels.Survey;

public class SurveyQuestionVM
{
    public int Id { get; set; }
    public GenderText? Header { get; set; }
    public List<GenderText> Questions { get; set; } = new();
    public SurveyQuestionCategory Category { get; set; }
    public SurveyQuestionType Type { get; set; }
    public ActivityTargetGroup TargetGroup { get; set; } 

    public SurveyQuestionVM CloneWithQuestions(params GenderText[] questions)
    {        

        var surveyQuestionVM = new SurveyQuestionVM
        {
            Id = this.Id,
            Header = Header is null ? null : new GenderText(this.Header.Female, this.Header.Male),
            Questions = new List<GenderText>(questions),
            Category = this.Category,
            Type = this.Type,
            TargetGroup = this.TargetGroup,
        };

        return surveyQuestionVM;
    }

}







