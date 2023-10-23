
using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Survey.Questions;

public class SurveyQuestionQueryDto
{
    public int Id { get; set; }
    public List<string> Texts { get; set; } = new();
    public SurveyQuestionCategory Category { get; set; }
    public SurveyQuestionType Type { get; set; }

    protected SurveyQuestionQueryDto() { }

    protected SurveyQuestionQueryDto(int id, SurveyQuestionType type, SurveyQuestionCategory category)
    {
        this.Id = id;
        this.Type = type;
        this.Category = category;
    }

    public static MaleSurveyQuestionDto CreateMaleQuestion(int id, SurveyQuestionType type, SurveyQuestionCategory category) =>
       new MaleSurveyQuestionDto(id, type, category);

    public static FemaleSurveyQuestionDto CreateFemaleQuestion(int id, SurveyQuestionType type, SurveyQuestionCategory category) =>
        new FemaleSurveyQuestionDto(id, type, category);
}


public class FemaleSurveyQuestionDto : SurveyQuestionQueryDto
{
    private FemaleSurveyQuestionDto(): base()
    { }

    public FemaleSurveyQuestionDto(int id, SurveyQuestionType type, SurveyQuestionCategory questionType) : base(id, type, questionType)
    {

    }
}

public class MaleSurveyQuestionDto : SurveyQuestionQueryDto
{
    private MaleSurveyQuestionDto() :base()
    { 
    }

    public MaleSurveyQuestionDto(int id, SurveyQuestionType type, SurveyQuestionCategory questionType) : base(id, type, questionType)
    {

    }
}

