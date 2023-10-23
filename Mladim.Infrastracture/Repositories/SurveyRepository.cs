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

public class SurveyRepository : GenericRepository<SurveyQuestionnairy> , ISurveyRepository
{
    public SurveyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<SurveyQuestion>> GetSurveyQuestions(Gender gender, SurveyQuestionCategory category) => gender
        switch
    {
        Gender.Male => await this.DbSet
            .SelectMany(x => x.Questions)
            .OfType<MaleSurveyQuestion>()
            .Where(q => q.Category == category)
            .ToListAsync(),
        Gender.Female or Gender.Undefined or Gender.Other =>await this.DbSet
            .SelectMany(x => x.Questions)
            .OfType<FemaleSurveyQuestion>()
            .Where(q => q.Category == category)
            .ToListAsync(),
        _ => new List<FemaleSurveyQuestion>(),
    };



    

    
}
