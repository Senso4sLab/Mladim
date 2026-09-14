using AutoMapper;
using Mladim.Domain.Dtos.Members.AnonymousParticipants;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Dtos.Survey.Statistics;
using Mladim.Domain.Models;
using Mladim.Domain.Models.Survey.ParticipantResponseTypes;
using Mladim.Domain.Models.Survey.Questions;
using Mladim.Domain.Models.Survey.Responses;
using Mladim.Domain.Models.Survey.Statistics;

namespace Mladim.Application.MappingProfiles.Profiles.Survey;

public class SurveyProfile : Profile
{
    public SurveyProfile()
    {

        CreateMap<SurveyQuestion, SurveyQuestionQueryDto>();

        CreateMap<AnonymousParticipantCommandDto, AnonymousParticipant>().ReverseMap();

        CreateMap<AnonymousYouthWorkerCommandDto, AnonymousYouthWorker>().ReverseMap();

        CreateMap<AnonymousSurveyResponseDto, AnonymousSurveyResponse>()
                   .ForMember(dest => dest.Id, opt => opt.Ignore())
                   .ForMember(dest => dest.Activity, opt => opt.Ignore())
                   .ForMember(dest => dest.ActivityId, opt => opt.Ignore())
                   // ignore polymorphic properties here and set them in AfterMap
                   .ForMember(dest => dest.AnonymousParticipant, opt => opt.Ignore())
                   .ForMember(dest => dest.AnonymousYouthWorker, opt => opt.Ignore())
                   .AfterMap((src, dest, ctx) =>
                   {
                       dest.AnonymousParticipant = null;
                       dest.AnonymousYouthWorker = null;

                       if (src.AnonymousParticipant is AnonymousParticipantCommandDto participantDto)
                       {
                           dest.AnonymousParticipant = ctx.Mapper.Map<AnonymousParticipant>(participantDto);
                       }
                       else if (src.AnonymousParticipant is AnonymousYouthWorkerCommandDto youthWorkerDto)
                       {
                           dest.AnonymousYouthWorker = ctx.Mapper.Map<AnonymousYouthWorker>(youthWorkerDto);
                       }
                   });


        CreateMap<AnonymousSurveyResponse, AnonymousSurveyResponseDto>()
            .ForMember(dest => dest.AnonymousParticipant, opt => opt.Ignore())
            .AfterMap((src, dest, ctx) =>
            {
                if (src.AnonymousYouthWorker is not null)
                {
                    dest.AnonymousParticipant = ctx.Mapper.Map<AnonymousYouthWorkerCommandDto>(src.AnonymousYouthWorker);
                }
                else if (src.AnonymousParticipant is not null)
                {
                    dest.AnonymousParticipant = ctx.Mapper.Map<AnonymousParticipantCommandDto>(src.AnonymousParticipant);
                }
                else
                {
                    dest.AnonymousParticipant = null!;
                }
            });


        CreateMap<QuestionRatingResponseDto, QuestionRatingResponse>().ReverseMap();
        CreateMap<QuestionTextResponseDto, QuestionTextResponse>().ReverseMap();
        CreateMap<QuestionBooleanResponseDto, QuestionBooleanResponse>().ReverseMap();
        CreateMap<QuestionMultiButtonResponseDto, QuestionMultiButtonResponse>().ReverseMap();
        CreateMap<QuestionMultiRepetitiveButtonResponseDto, QuestionMultiRepetitiveButtonResponse>().ReverseMap();


        CreateMap<QuestionResponseDto, QuestionResponse>()
            .Include<QuestionRatingResponseDto, QuestionRatingResponse>()
            .Include<QuestionTextResponseDto, QuestionTextResponse>()
            .Include<QuestionBooleanResponseDto, QuestionBooleanResponse>()
            .Include<QuestionMultiButtonResponseDto, QuestionMultiButtonResponse>()
            .Include<QuestionMultiRepetitiveButtonResponseDto, QuestionMultiRepetitiveButtonResponse>()
            .ReverseMap();

       

        CreateMap<QuestionSurveyStatistics, QuestionSurveyStatisticsDto>();
        CreateMap<SurveyStatistics, SurveyStatisticsDto>();
        CreateMap<QuestionResponseStatistics, QuestionResponseStatisticsDto>();
        CreateMap<ParticipantResponseType, ParticipantResponseTypeDto>()
             .ForMember(dest => dest.ResponseType, m => m.MapFrom(src => src.ResponseType.ToString()));
       


    }
}
