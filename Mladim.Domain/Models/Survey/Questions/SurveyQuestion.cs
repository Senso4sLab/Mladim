using Mladim.Domain.Enums;

namespace Mladim.Domain.Models.Survey.Questions;

public class SurveyQuestion
{
    public int Id { get; set; }
    public List<string> Texts { get; set; } = new();    
    public SurveyQuestionCategory Category { get; set; }
    public SurveyQuestionType Type { get; set; }
    public List<SurveyQuestionnairy> SurveyQuestionnairies { get; set; } = new();
    protected SurveyQuestion()
    {

    }

    protected SurveyQuestion(int id, SurveyQuestionType type, SurveyQuestionCategory category)
    {
        this.Id = id;      
        this.Type = type;
        this.Category = category;           
    }

    public SurveyQuestion AddText(string question)
    {
        Texts.Add(question);
        return this;
    }

    public static MaleSurveyQuestion CreateMaleQuestion(int id, SurveyQuestionType type, SurveyQuestionCategory category) =>
        new MaleSurveyQuestion(id, type, category);

    public static FemaleSurveyQuestion CreateFemaleQuestion(int id, SurveyQuestionType type, SurveyQuestionCategory category) =>
        new FemaleSurveyQuestion(id, type, category);   

}


public class FemaleSurveyQuestion : SurveyQuestion
{
    private FemaleSurveyQuestion() 
    { }

    public FemaleSurveyQuestion(int id, SurveyQuestionType type, SurveyQuestionCategory questionType) : base(id, type, questionType)
    {

    }

}


public class MaleSurveyQuestion : SurveyQuestion
{
    private MaleSurveyQuestion() { }

    public MaleSurveyQuestion(int id, SurveyQuestionType type, SurveyQuestionCategory questionType) : base(id, type, questionType)
    {

    }

}

