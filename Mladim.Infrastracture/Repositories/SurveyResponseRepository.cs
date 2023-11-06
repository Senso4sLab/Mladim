using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models.Survey.Responses;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class SurveyResponseRepository : GenericRepository<AnonymousSurveyResponse>, ISurveyResponseRepository
{
    public SurveyResponseRepository(ApplicationDbContext context) : base(context)
    {
    }
}
