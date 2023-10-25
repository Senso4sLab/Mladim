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
        CreateMap<SurveyQuestionQueryDto, SurveyQuestionVM>();
        CreateMap<MaleSurveyQuestionDto, SurveyQuestionVM>();
        CreateMap<FemaleSurveyQuestionDto, SurveyQuestionVM>();
        CreateMap<SurveyQuestionnairyQueryDto, SurveyQuestionResponseVM>();
       
    }
}
