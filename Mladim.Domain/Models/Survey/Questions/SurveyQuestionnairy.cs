namespace Mladim.Domain.Models.Survey.Questions;

public class SurveyQuestionnairy
{
    public int Id { get; set; }
    public List<SurveyQuestion> Questions { get; set; } = new();    
    public static SurveyQuestionnairy Create(int id, IEnumerable<SurveyQuestion> questions = null) => 
        new SurveyQuestionnairy { Id = id, Questions = questions?.ToList() ?? new List<SurveyQuestion>() };  

    public List<Activity> Activities { get; set; } = new();

}


