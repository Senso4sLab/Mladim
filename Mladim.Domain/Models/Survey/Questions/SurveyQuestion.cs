using Mladim.Domain.Enums;

namespace Mladim.Domain.Models.Survey.Questions;

public class SurveyQuestion
{
    public int Id { get; set; }    
    public GenderText? Header { get; set; } 
    public List<GenderText> Questions { get; set; } = new();
    public SurveyQuestionCategory Category { get; set; }
    public SurveyQuestionType Type { get; set; }
    public ActivityTargetGroup TargetGroup { get; set; }  
    protected SurveyQuestion()
    {
    }
    protected SurveyQuestion(SurveyQuestionType type, SurveyQuestionCategory category, ActivityTargetGroup targetGroup, GenderText? header)
    {       
        Type = type;
        Category = category;  
        TargetGroup = targetGroup;
        Header = header;
    }
    public SurveyQuestion AddQuestion(GenderText subQuestion)
    {    
        Questions.Add(subQuestion);
        return this;
    }
    public SurveyQuestion AddQuestions(IEnumerable<GenderText> subQuestions)
    {
        Questions.AddRange(subQuestions);
        return this;
    }
    public static SurveyQuestion ForParticipant(SurveyQuestionType type, SurveyQuestionCategory category, GenderText? header = null) =>
        new SurveyQuestion(type, category, ActivityTargetGroup.Participants, header);

    public static SurveyQuestion ForYouthWorker(SurveyQuestionType type, SurveyQuestionCategory category, GenderText? header = null) =>
        new SurveyQuestion(type, category, ActivityTargetGroup.YouthWorkers, header);
}


