using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts.Persistence;

public interface ISurveyQuestionRepository : IGenericRepository<SurveyQuestion>
{    
    Task<IEnumerable<SurveyQuestion>> GetSurveyQuestionnairy(int questionnairyId, Gender gender, SurveyQuestionCategory category);
}
