using AutoMapper;
using Mladim.Client.ViewModels.Members.StaffMembers;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Enums;

namespace Mladim.Client.MappingProfiles.Profiles.Survey;

public class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<SurveyQuestionQueryDto, SurveyQuestionVM>();
        CreateMap<MaleSurveyQuestionDto, SurveyQuestionVM>();
        CreateMap<FemaleSurveyQuestionDto, SurveyQuestionVM>();
        CreateMap<SurveyQuestionnairyQueryDto, SurveyQuestionResponseVM>();


        CreateMap<AnonymousSurveyResponseVM, AnonymousSurveyResponseDto>().ReverseMap();

        CreateMap<QuestionRatingResponseVM, QuestionRatingResponseDto>().ReverseMap();
        CreateMap<QuestionTextResponseVM, QuestionTextResponseDto>().ReverseMap();
        CreateMap<QuestionBooleanResponseVM, QuestionBooleanResponseDto>().ReverseMap();
        CreateMap<QuestionMultiButtonResponseVM, QuestionMultiButtonResponseDto>()
            .ConvertUsing<SurveryButtonResponseTypeConverter>();

        CreateMap<QuestionMultiButtonResponseDto, QuestionMultiButtonResponseVM>()
           .ConvertUsing<SurveryButtonResponseTypeDtoConverter>();

        


        CreateMap<QuestionResponseVM, QuestionResponseDto>()
            .Include<QuestionRatingResponseVM, QuestionRatingResponseDto>()
            .Include<QuestionTextResponseVM, QuestionTextResponseDto>()
            .Include<QuestionBooleanResponseVM, QuestionBooleanResponseDto>()
            .Include<QuestionMultiButtonResponseVM, QuestionMultiButtonResponseDto>().ReverseMap();

    }

    public class SurveryButtonResponseTypeConverter : ITypeConverter<QuestionMultiButtonResponseVM, QuestionMultiButtonResponseDto>
    {
        public QuestionMultiButtonResponseDto Convert(QuestionMultiButtonResponseVM source, QuestionMultiButtonResponseDto destination, ResolutionContext context)
        {
            return new QuestionMultiButtonResponseDto()
            {
                Response = source.Response.Select(r => r.ButtonType).ToList(),
                UniqueQuestionId = source.UniqueQuestionId,
            };
        }
    }

    public class SurveryButtonResponseTypeDtoConverter : ITypeConverter<QuestionMultiButtonResponseDto, QuestionMultiButtonResponseVM>
    {
        public QuestionMultiButtonResponseVM Convert(QuestionMultiButtonResponseDto source, QuestionMultiButtonResponseVM destination, ResolutionContext context)
        {
            return new QuestionMultiButtonResponseVM(source.UniqueQuestionId)
            {
                Response = source.Response.Select(r =>new SurveryButtonResponseVM() { ButtonType = r }).ToList(),               
            };
        }
    }




}
