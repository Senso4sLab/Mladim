using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;
using Mladim.Infrastracture.Persistance;

namespace Mladim.Infrastracture.Repositories;

public class SurveyQuestionRepository : GenericRepository<SurveyQuestion> , ISurveyQuestionRepository
{
    public SurveyQuestionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<SurveyQuestion>> GetQuestionnaire(ActivityTargetGroup targetGroup, SurveyQuestionCategory categories) =>
        await DbSet
            .Where(question => question.TargetGroup == targetGroup)
            .Where(question => (question.Category & categories) > 0)
            .ToListAsync();  
    
}
