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
    private StorageKeys StorageKeys { get; }

    public SurveyService(IGenericHttpService httpClient, IOptions<MladimApiUrls> MladimApiUrls,
      IOptions<StorageKeys> storageKeys, IMapper mapper)
    {
        this.Mapper = mapper;
        this.HttpClient = httpClient;
        this.StorageKeys = storageKeys.Value;
        this.MladimApiUrls = MladimApiUrls.Value;
    }

    public async Task<SurveyQuestionnairyVM> GetSurveyQuestionnairyAsync(int activityId, Gender gender)
    {
        string url = string.Format(MladimApiUrls.GetSurveyQuestionnairy, activityId, gender);
        var questionnairy = await HttpClient.GetAsync<SurveyQuestionnairyQueryDto>(url);
        return this.Mapper.Map<SurveyQuestionnairyVM>(questionnairy);
    }



}
