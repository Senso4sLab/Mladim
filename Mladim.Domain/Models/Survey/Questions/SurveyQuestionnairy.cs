using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models.Survey.Questions;

public class SurveyQuestionnairy
{
    public int Id { get; set; }
    public List<SurveyQuestion> Questions { get; set; } = new();

    public static SurveyQuestionnairy Create(int id) => 
        new SurveyQuestionnairy { Id = id };

    public SurveyQuestionnairy AddQuestion(SurveyQuestion question)
    {
        Questions.Add(question);
        return this;
    }

    public List<Activity> Activities { get; set; } = new();

}

