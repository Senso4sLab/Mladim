using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts.Persistence;

public interface ISurveyRepository : IGenericRepository<SurveyQuestionnairy>
{
   Task<IEnumerable<SurveyQuestion>> GetSurveyQuestions(Gender gender, SurveyQuestionCategory category);
}
