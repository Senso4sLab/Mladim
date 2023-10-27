using AutoMapper;
using Mladim.Application.Features.Members.Partners.Commands.AddPartner;
using Mladim.Application.Features.Members.StaffMembers.Commands.UpdatePartner;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Models;
using Mladim.Domain.Models.Survey.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.Survey;

public class SurveyProfile : Profile
{
    public SurveyProfile()
    {

        CreateMap<MaleSurveyQuestion, MaleSurveyQuestionDto>();
        CreateMap<FemaleSurveyQuestion, FemaleSurveyQuestionDto>();


        CreateMap<SurveyQuestion, SurveyQuestionQueryDto>()
            .Include<MaleSurveyQuestion, MaleSurveyQuestionDto>()
            .Include<FemaleSurveyQuestion, FemaleSurveyQuestionDto>();

        CreateMap<SurveyQuestionnairy, SurveyQuestionnairyQueryDto>();



    }
}
