using Mladim.Domain.Enums;

namespace Mladim.Domain.Models;

public class ActivityAttributes : BaseAttibutes
{
    public ActivityTypes ActivityTypes { get; protected set; }
    public bool IsGroup { get; set; }
    public bool IsRepetitive { get; set; }
    public ActivityRepetitiveInterval RepetitiveInterval { get; set; }
    public int NumOfRepetitions { get; set; }

    public void ChangeName(string name) => Name = name;


    public SurveyQuestionCategory GetSurveyQuestionCategory()
    {
        var surveyCategory = SurveyQuestionCategory.General;

        if (IsGroup)
            surveyCategory = surveyCategory | SurveyQuestionCategory.Group;

        if (IsRepetitive)
            surveyCategory = surveyCategory | SurveyQuestionCategory.Repetitive;

        return surveyCategory;
    }
   





    public ActivityAttributes Clone()
    {
        return new ActivityAttributes()
        {
            ActivityTypes = ActivityTypes,
            IsGroup = IsGroup,
            IsRepetitive = IsRepetitive,
            RepetitiveInterval = RepetitiveInterval,
            Description = Description,
            Name = Name,
            NumOfRepetitions = NumOfRepetitions,
        };
    }


}


