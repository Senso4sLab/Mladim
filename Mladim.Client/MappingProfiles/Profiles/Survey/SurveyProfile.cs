using AutoMapper;
using Mladim.Client.ViewModels.Members.StaffMembers;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Enums;
using Mladim.Domain.Dtos.Survey.Statistics;
using Mladim.Domain.Models.Survey.Statistics;
using CsvHelper.TypeConversion;

namespace Mladim.Client.MappingProfiles.Profiles.Survey;

public class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<SurveyQuestionQueryDto, SurveyQuestionVM>()
             .ConvertUsing<SurveryQuestionConverter>(); 
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


        CreateMap<QuestionResponseStatisticsDto, QuestionResponseStatisticsVM>();       
        CreateMap<QuestionResponseTypesDto, QuestionResponseTypesVM>();
        CreateMap<SubQuestionResponseTypesDto, SubQuestionResponseTypesVM>();
        CreateMap<ParticipantResponseTypeDto, ParticipantResponseTypeVM>();
    }



    public class SurveryQuestionConverter : ITypeConverter<SurveyQuestionQueryDto, SurveyQuestionVM>
    {
        public SurveyQuestionVM Convert(SurveyQuestionQueryDto source, SurveyQuestionVM destination, ResolutionContext context)
        {
            return new SurveyQuestionVM()
            {
                Type = source.Type,
                UniqueQuestionId = source.UniqueQuestionId,
                Header = IsMultipleQuestion(source.Type) ? source.Texts.FirstOrDefault() : null,
                Texts = IsMultipleQuestion(source.Type) ? source.Texts.Skip(1).ToList() : source.Texts.ToList(),
            };
           
        }
        private bool IsMultipleQuestion(SurveyQuestionType type) => 
            type is SurveyQuestionType.Multiple or SurveyQuestionType.MultipleRepetitive;
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
