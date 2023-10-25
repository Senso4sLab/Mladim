using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts.Persistence;

public interface ISurveyRepository : IGenericRepository<SurveyQuestion>
{
    //   Task<IEnumerable<SurveyQuestion>> GetSurveyQuestions(Gender gender, SurveyQuestionCategory category);

    //    Task<SurveyQuestionnairy?> GetSurveyQuestionnairy(Gender gender, SurveyQuestionCategory category);
    Task<SurveyQuestionnairy> GetSurveyQuestionnairy(int questionnairyId, Gender gender, SurveyQuestionCategory category);
}
