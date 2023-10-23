using AutoMapper;
using Mladim.Client.ViewModels.Members.StaffMembers;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Client.ViewModels.Survey;

namespace Mladim.Client.MappingProfiles.Profiles.Survey;

public class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<SurveyQuestionQueryDto, SurveyQuestionVM>()
            .Include<MaleSurveyQuestionDto, MaleSurveyQuestionVM>()
            .Include<FemaleSurveyQuestionDto, FemaleSurveyQuestionVM>();

        CreateMap<SurveyQuestionnairyQueryDto, SurveyQuestionnairyVM>();
       
    }
}
