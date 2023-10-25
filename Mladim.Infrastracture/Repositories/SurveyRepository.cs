using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class SurveyRepository : GenericRepository<SurveyQuestion> , ISurveyRepository
{
    public SurveyRepository(ApplicationDbContext context) : base(context)
    {
    }



    public async Task<SurveyQuestionnairy> GetSurveyQuestionnairy(int questionnairyId, Gender gender, SurveyQuestionCategory category)
    {
        var sequence = GetByQuestionnairtyId(this.DbSet, questionnairyId);
        sequence = GetByGender(sequence, gender);
        sequence = GetByCategory(sequence, category);
        return SurveyQuestionnairy.Create(questionnairyId, await sequence.ToListAsync());
    }

    private IQueryable<SurveyQuestion> GetByQuestionnairtyId(IQueryable<SurveyQuestion> surveyQuestions, int questionnairyId) =>
        surveyQuestions.Where(sq => sq.SurveyQuestionnairies.Any(q => q.Id == questionnairyId));

    private IQueryable<SurveyQuestion> GetByGender(IQueryable<SurveyQuestion> sequence, Gender gender) => gender
            switch
    {
        Gender.Male => sequence.OfType<MaleSurveyQuestion>(),
        Gender.Female or Gender.Undefined or Gender.Other => sequence.OfType<FemaleSurveyQuestion>(),
        _ => throw new NotImplementedException(),
    };

    private IQueryable<SurveyQuestion> GetByCategory(IQueryable<SurveyQuestion> surveyQuestions, SurveyQuestionCategory category) =>
        surveyQuestions.Where(sq => (sq.Category & category) > 0);   


}
