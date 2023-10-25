using AutoMapper;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;

namespace Mladim.Client.Services.SubjectServices.Implementations;

public class SurveyService : ISurveyService
{
    private IMapper Mapper { get; }
    private MladimApiUrls MladimApiUrls { get; }
    private IGenericHttpService HttpClient { get; }
    

    public SurveyService(IGenericHttpService httpClient, IOptions<MladimApiUrls> MladimApiUrls, IMapper mapper)
    {
        this.Mapper = mapper;
        this.HttpClient = httpClient;        
        this.MladimApiUrls = MladimApiUrls.Value;
    }

    public async Task<IEnumerable<SurveyQuestionVM>> GetSurveyQuestionnairyAsync(int activityId, Gender gender)
    {
        string url = string.Format(MladimApiUrls.GetSurveyQuestionnairy, activityId, gender);
        var questionnairy = await HttpClient.GetAsync<IEnumerable<SurveyQuestionQueryDto>>(url);
        return this.Mapper.Map<IEnumerable<SurveyQuestionVM>>(questionnairy);
    }



}
