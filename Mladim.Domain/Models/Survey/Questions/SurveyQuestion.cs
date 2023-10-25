using Mladim.Domain.Enums;

namespace Mladim.Domain.Models.Survey.Questions;

public class SurveyQuestion
{
    public int Id { get; set; }
    public List<string> Texts { get; set; } = new();    
    public SurveyQuestionCategory Category { get; set; }
    public SurveyQuestionType Type { get; set; }
    public int UniqueQuestionId { get; set; }
    public List<SurveyQuestionnairy> SurveyQuestionnairies { get; set; } = new();
    protected SurveyQuestion()
    {

    }

    protected SurveyQuestion(int id, int uniqueQuestionId, SurveyQuestionType type, SurveyQuestionCategory category)
    {
        this.Id = id;       
        this.Type = type;
        this.Category = category;
        this.UniqueQuestionId = uniqueQuestionId;
    }

    public SurveyQuestion AddText(string question)
    {
        Texts.Add(question);
        return this;
    }

    public static MaleSurveyQuestion CreateMaleQuestion(int id, int uniqueQuestionId, SurveyQuestionType type, SurveyQuestionCategory category) =>
        new MaleSurveyQuestion(id, uniqueQuestionId, type, category);

    public static FemaleSurveyQuestion CreateFemaleQuestion(int id, int uniqueQuestionId, SurveyQuestionType type, SurveyQuestionCategory category) =>
        new FemaleSurveyQuestion(id, uniqueQuestionId, type, category);

}


public class FemaleSurveyQuestion : SurveyQuestion
{
    private FemaleSurveyQuestion()
    { }

    public FemaleSurveyQuestion(int id, int uniqueQuestionId, SurveyQuestionType type, SurveyQuestionCategory questionType) : base(id, uniqueQuestionId, type, questionType)
    {

    }

}


public class MaleSurveyQuestion : SurveyQuestion
{
    private MaleSurveyQuestion() { }

    public MaleSurveyQuestion(int id, int uniqueQuestionId, SurveyQuestionType type, SurveyQuestionCategory questionType) : base(id, uniqueQuestionId, type, questionType)
    {

    }

}

