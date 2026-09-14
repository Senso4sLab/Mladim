using AutoMapper;
using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Dtos.Survey.Statistics;
using System.Data;

namespace Mladim.Client.MappingProfiles.Profiles.Survey;

public class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<SurveyQuestionQueryDto, SurveyQuestionVM>();
            
   
        
        CreateMap<SurveyQuestionnairyQueryDto, SurveyQuestionResponseVM>();


       

        CreateMap<AnonymousSurveyResponseVM, AnonymousSurveyResponseDto>().ReverseMap();

        CreateMap<QuestionRatingResponseVM, QuestionRatingResponseDto>().ReverseMap();
        CreateMap<QuestionTextResponseVM, QuestionTextResponseDto>().ReverseMap();
        CreateMap<QuestionBooleanResponseVM, QuestionBooleanResponseDto>().ReverseMap();
        CreateMap<QuestionMultiButtonResponseVM, QuestionMultiButtonResponseDto>()
            .ConvertUsing<SurveryButtonResponseTypeConverter>();

        CreateMap<QuestionMultiButtonResponseDto, QuestionMultiButtonResponseVM>()
           .ConvertUsing<SurveryButtonResponseTypeDtoConverter>();

        CreateMap<QuestionMultiRepetitiveButtonResponseVM, QuestionMultiRepetitiveButtonResponseDto>()
           .ConvertUsing<SurveryRepetitiveButtonResponseTypeConverter>();

        CreateMap<QuestionMultiRepetitiveButtonResponseDto, QuestionMultiRepetitiveButtonResponseVM>()
           .ConvertUsing<SurveryRepetitiveButtonResponseTypeDtoConverter>();


        CreateMap<QuestionResponseVM, QuestionResponseDto>()
            .Include<QuestionRatingResponseVM, QuestionRatingResponseDto>()
            .Include<QuestionTextResponseVM, QuestionTextResponseDto>()
            .Include<QuestionBooleanResponseVM, QuestionBooleanResponseDto>()
            .Include<QuestionMultiButtonResponseVM, QuestionMultiButtonResponseDto>()
            .Include<QuestionMultiRepetitiveButtonResponseVM, QuestionMultiRepetitiveButtonResponseDto>()
            .ReverseMap();



        CreateMap<QuestionSurveyStatisticsVM, QuestionSurveyStatisticsDto>().ReverseMap();
        CreateMap<SurveyStatisticsVM, SurveyStatisticsDto>().ReverseMap();
        CreateMap<QuestionResponseStatisticsVM, QuestionResponseStatisticsDto>().ReverseMap();
        CreateMap<ParticipantResponseTypeVM, ParticipantResponseTypeDto>().ReverseMap();
    }



    




    public class SurveryButtonResponseTypeConverter : ITypeConverter<QuestionMultiButtonResponseVM, QuestionMultiButtonResponseDto>
    {
        public QuestionMultiButtonResponseDto Convert(QuestionMultiButtonResponseVM source, QuestionMultiButtonResponseDto destination, ResolutionContext context)
        {
            return new QuestionMultiButtonResponseDto()
            {
                Response = source.Response.Select(r => r.Response).ToList(),
                UniqueQuestionId = source.UniqueQuestionId,
            };
        }
    }

    public class SurveryButtonResponseTypeDtoConverter : ITypeConverter<QuestionMultiButtonResponseDto, QuestionMultiButtonResponseVM>
    {
        public QuestionMultiButtonResponseVM Convert(QuestionMultiButtonResponseDto source, QuestionMultiButtonResponseVM destination, ResolutionContext context)
        {

            var questionMultiButtonResponse = new QuestionMultiButtonResponseVM(source.UniqueQuestionId);

            foreach(var reponseType in source.Response)
                questionMultiButtonResponse.AddResponse(new QuestionButtonResponseVM(source.UniqueQuestionId) { Response = reponseType });

            return questionMultiButtonResponse;
        }
    }


    public class SurveryRepetitiveButtonResponseTypeConverter : ITypeConverter<QuestionMultiRepetitiveButtonResponseVM, QuestionMultiRepetitiveButtonResponseDto>
    {
        public QuestionMultiRepetitiveButtonResponseDto Convert(QuestionMultiRepetitiveButtonResponseVM source, QuestionMultiRepetitiveButtonResponseDto destination, ResolutionContext context)
        {
            return new QuestionMultiRepetitiveButtonResponseDto()
            {
                Response = source.Response.Select(r => r.Response).ToList(),
                UniqueQuestionId = source.UniqueQuestionId,
            };
        }
    }

    public class SurveryRepetitiveButtonResponseTypeDtoConverter : ITypeConverter<QuestionMultiRepetitiveButtonResponseDto, QuestionMultiRepetitiveButtonResponseVM>
    {
        public QuestionMultiRepetitiveButtonResponseVM Convert(QuestionMultiRepetitiveButtonResponseDto source, QuestionMultiRepetitiveButtonResponseVM destination, ResolutionContext context)
        {

            var questionMultiRepetitiveButtonResponse = new QuestionMultiRepetitiveButtonResponseVM(source.UniqueQuestionId);

            foreach (var reponseType in source.Response)
                questionMultiRepetitiveButtonResponse.AddResponse(new QuestionRepetitiveButtonResponseVM(source.UniqueQuestionId) { Response = reponseType });

            return questionMultiRepetitiveButtonResponse;           
        }
    }




}
