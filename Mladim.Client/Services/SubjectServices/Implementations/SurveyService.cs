using AutoMapper;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Members.AnonymousParticipants;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Dtos.Survey.Statistics;
using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;
using Syncfusion.Blazor.Schedule.Internal;

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
        string url = string.Format(MladimApiUrls.GetSurveyQuestionnaire, activityId, gender);
        var questionnairy = await HttpClient.GetAsync<IEnumerable<SurveyQuestionQueryDto>>(url);
        return this.Mapper.Map<IEnumerable<SurveyQuestionVM>>(questionnairy);
    }

    public async Task<bool> PostAnonymousSurveyResponseAsync(int activityId, AnonymousSurveyResponseVM anonymousSurveyResponse)
    {
        string url = string.Format(MladimApiUrls.AnonymousSurveyCommand, activityId);
        //var questionResponseDto = this.Mapper.Map<IEnumerable<QuestionResponseDto>>(anonymousSurveyResponse.Responses);
        //var anonymousParticipant = this.Mapper.Map<AnonymousParticipantCommandDto>(anonymousSurveyResponse.AnonymousParticipant);        
        
        return await HttpClient.PostAsync<AnonymousSurveyResponseDto, bool>(url, this.Mapper.Map<AnonymousSurveyResponseDto>(anonymousSurveyResponse));      
    }

    public async Task<IEnumerable<AnonymousSurveyResponseVM>> GetAnonymousSurveyResponsesAsync(int activityId)
    {
        string url = string.Format(MladimApiUrls.GetSurveyResponses, activityId);
        var responses = await HttpClient.GetAsync<IEnumerable<AnonymousSurveyResponseDto>>(url);
        return this.Mapper.Map<IEnumerable<AnonymousSurveyResponseVM>>(responses);       
    }

    public async Task<IEnumerable<QuestionSurveyStatisticsVM>> GetStatisticsByOrganizationIdAsync(int organizationId, DateTime start, DateTime end)
    {
        string url = string.Format(MladimApiUrls.GetSurveyStatisticsByOrganization, organizationId, start.ToLongDateString(), end.ToLongDateString());
        var responses = await HttpClient.GetAsync<IEnumerable<QuestionSurveyStatisticsDto>>(url);            
        return this.Mapper.Map<IEnumerable<QuestionSurveyStatisticsVM>>(responses);       
    }

    public async Task<IEnumerable<QuestionSurveyStatisticsVM>> GetStatisticsByProjectIdIdAsync(int projectId)
    {
        string url = string.Format(MladimApiUrls.GetSurveyStatisticsByProject, projectId);
        var responses = await HttpClient.GetAsync<IEnumerable<QuestionSurveyStatisticsDto>>(url);
        return this.Mapper.Map<IEnumerable<QuestionSurveyStatisticsVM>>(responses);
    }
}
