using AutoMapper;
using Mladim.Application.Features.Members.Partners.Commands.AddPartner;
using Mladim.Application.Features.Members.StaffMembers.Commands.UpdatePartner;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Models;
using Mladim.Domain.Models.Survey.Questions;
using Mladim.Domain.Models.Survey.Responses;
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


        CreateMap<AnonymousSurveyResponseDto, AnonymousSurveyResponse>().ReverseMap();


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





    }
}
