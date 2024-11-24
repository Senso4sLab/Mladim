using global::Microsoft.AspNetCore.Components;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels.Activity;
using Syncfusion.Blazor.Grids;
using Mladim.Client.ViewModels.Organization;
using Mladim.Client.ViewModels.Survey;
using Mladim.Client.Models;
using Microsoft.JSInterop;
using System.Timers;
using MudBlazor;
using Syncfusion.Blazor.Charts;
using Mladim.Domain.Models.Survey.Statistics;
using static MudBlazor.CategoryTypes;
using CsvHelper.Configuration;
using CsvHelper;
using Mladim.Domain.Extensions;
using Mladim.Domain.Models.Survey.Questions;
using System.Globalization;
using Mladim.Client.Services.SubjectServices.Implementations;
using Mladim.Domain.Enums;
using Mladim.Client.Services.Csv;
using Mladim.Client.Csv;
using Syncfusion.Blazor.PivotView;


namespace Mladim.Client.Components.Organizations;

public partial class OrganizationStatisticsTab : IExportChart
{

    [Inject]
    public IOrganizationService OrganizationService { get; set; } = default!;

    [Inject]
    public IActivityService ActivityService { get; set; } = default!;

    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;

    [Inject]
    public IJSRuntime JS { get; set; } = default!;

    [Inject]
    public ICsvService CsvService { get; set; }

    bool MoreQuestionStatistics { get; set; } = false;
    IEnumerable<int> defaultQuestionsForStatistics = new List<int>() { 1, 2, 3, 4, 5, 11 };


    bool isActiveExportingImages = false;
    bool isAnyParticipant = false;

    SfAccumulationChart ParticipantsByAgeChart = default!;
    SfAccumulationChart ParticipantsByGenderChart = default!;

    ElementReference Element;

    List<Func<Task>> ExportChartsAsync = new List<Func<Task>>();
    public DefaultOrganization? SelectedOrganization { get; set; }
    private OrganizationStatisticVM organizationStatistics { get; set; } = default!; 

    private DateRange statisticsDateRange = new DateRange();
    
    private List<ActivityForGantt> activities = new List<ActivityForGantt>();

    public string StackedBarWidth { get; set; } =  "100%";
    private List<QuestionSurveyStatisticsVM> ShownQuestionsSurveyStatistics { get; set; } = new List<QuestionSurveyStatisticsVM>();

    private IEnumerable<QuestionSurveyStatisticsVM> QuestionsSurveyStatistics { get; set; } = new List<QuestionSurveyStatisticsVM>();


    int totalParticipants = 0;
    int totalActivities = 0;
    
    protected override async Task OnInitializedAsync()
    {
        SelectedOrganization = await this.OrganizationService.DefaultOrganizationAsync();
        SetDefaultOrgStatisticsDateRange(DateTime.UtcNow);

        ExportChartsAsync.Add(() => ExportAccumulationChartToImage(ParticipantsByAgeChart));
        ExportChartsAsync.Add(() => ExportAccumulationChartToImage(ParticipantsByGenderChart));

        await UpdateOrgStatisticsDataAsync();      
    }

    private async Task OnClickCsvExportFile()
    {
        CsvService.Open();

        CsvService.Write("Začetni datum", "Končni datum", "Št. udeležencev");

        if (organizationStatistics?.ParticipantsByGenders.Count > 0)
            CsvService.Write(organizationStatistics?.ParticipantsByGenders.Select(p => p.Gender.GetDisplayAttribute())!);

        if (organizationStatistics?.ParticipantsByAgeGroups.Count > 0)
            CsvService.Write(organizationStatistics?.ParticipantsByAgeGroups.Select(p => p.AgeGroup.GetDisplayAttribute())!);

        CsvService.Write("Št. aktivnosti", "Št. ur vseh aktivnosti");
        CsvService.NextRow();

        CsvService.Write(statisticsDateRange.Start!.Value.ToString("dd/MM/yyyy"), statisticsDateRange.End!.Value.ToString("dd/MM/yyyy"), totalActivities.ToString());

        if (organizationStatistics?.ParticipantsByGenders.Count > 0)
            CsvService.Write(organizationStatistics?.ParticipantsByGenders.Select(p => p.Number.ToString())!);

        if (organizationStatistics?.ParticipantsByAgeGroups.Count > 0)
            CsvService.Write(organizationStatistics?.ParticipantsByAgeGroups.Select(p => p.Number.ToString())!);

        CsvService.Write(totalActivities.ToString());
        CsvService.Write(organizationStatistics!.TotalActivitiesHours.ToString());

        CsvService.NextRow();
        CsvService.NextRow();

        var activitiesStatistics = await ActivityService.GetStatistics(SelectedOrganization!.Id, statisticsDateRange.Start.Value, statisticsDateRange.End.Value);

        if (activitiesStatistics != null)
        {

            CsvService.Write("Ime projekta", "Ime aktivnosti", "Začetek aktivnosti", "Konec aktivnosti", "Vrsta aktivnosti", "Skupinska aktivnost", "Ponavljajoča aktivnost", "Skupno št. udeležencev");

            CsvService.Write(Enum.GetValues<Gender>().Select(g => g.GetDisplayAttribute()));
            CsvService.Write(Enum.GetValues<AgeGroups>().Select(ag => ag.GetDisplayAttribute()));

            CsvService.NextRow();

            foreach (var statistics in activitiesStatistics)
            {
                CsvService.Write(statistics.ProjectName, statistics.Attributes.Name, statistics.Start.ToString("dd/MM/yyyy"), statistics.End.ToString("dd/MM/yyyy"));
                CsvService.Write(string.Join(',', statistics.Attributes.ActivityTypes.Select(t => t.GetDisplayAttribute())));
                CsvService.Write(statistics.Attributes.IsGroup ? "DA" : "NE", statistics.Attributes.IsRepetitive ? "DA" : "NE", statistics.ParticipantsByAgeGroups.Sum(p => p.Number).ToString());

                var participantGenderSum = Enum.GetValues<Gender>().Select(g => statistics.ParticipantsByGenders.Where(pg => pg.Gender == g).Sum(ap => ap.Number).ToString());
                CsvService.Write(participantGenderSum);

                var participantAgeGroupSum = Enum.GetValues<AgeGroups>().Select(ag => statistics.ParticipantsByAgeGroups.Where(pg => pg.AgeGroup == ag).Sum(ap => ap.Number).ToString());
                CsvService.Write(participantAgeGroupSum);
                CsvService.NextRow();
            }
        }

        CsvService.Close();
       
        using var streamRef = new DotNetStreamReference(CsvService.Stream);
        
        await JS.InvokeVoidAsync("downloadFileFromStream", $"Statistika_{SelectedOrganization.Name}.csv", streamRef);
       
    }




    public void OnMoreQuestionStatisticsChanged(bool toggled)
    {
        MoreQuestionStatistics = !MoreQuestionStatistics;
        ShowingQuestionsForSurveyStatistics();    
    }

    public void AddExportChart(Func<Task> exportChart)
    {
        this.ExportChartsAsync.Add(exportChart);    
    }

    public void RemoveExportChart(Func<Task> exportChart)
    {
        this.ExportChartsAsync.Remove(exportChart);
    }
    private void SetDefaultOrgStatisticsDateRange(DateTime now)
    {
        statisticsDateRange = new DateRange(now.AddYears(-1), now);
    }  
  
    private async Task GeneratePdfAsync()
    {
        StackedBarWidth = "800px";      
        await Task.Delay(100);
        await ParticipantsByAgeChart.PrintAsync(Element);
        StackedBarWidth = "100%";       
    }

 

    private Task ExportAccumulationChartToImage(SfAccumulationChart? chart) =>
        chart?.ExportAsync(Syncfusion.Blazor.Charts.ExportType.PNG, Guid.NewGuid().ToString()) ?? Task.CompletedTask;


    public async Task GenerateImagesAsync()
    {
        isActiveExportingImages = true;

        foreach (var chunk in ExportChartsAsync.Chunk(6))
        {
            await Task.WhenAll(chunk.Select(x => x.Invoke()));
            await Task.Delay(1000);
        }

        isActiveExportingImages = false;       
    }

    public async Task<List<ActivityForGantt>> UpcommingActivitiesAsync(int numOfUpcommingActivities)
    {
        // If upcomming activities is equal ti null all activities will be fetched!
        var upcommingActivities = await this.ActivityService.GetByOrganizationIdAsync(SelectedOrganization.Id, numOfUpcommingActivities);

        return upcommingActivities.Select((a, i) => ActivityForGantt.Create(i + 1, a.Id, a.Attributes.Name, a.Project, a.TimeRange)).ToList();
    }

    

    public void SelectedActivity(RowSelectEventArgs<ActivityForGantt> args) =>
       this.Navigation.NavigateTo($"/activity/{args.Data.ActivityId}");


    private async Task UpdateOrgStatisticsDataAsync()
    {       
       
        this.organizationStatistics = await OrganizationStatisticsAsync(statisticsDateRange);

        this.totalParticipants = organizationStatistics?.IndividualParticipants + organizationStatistics?.AnonymousParticipants ?? 0;

       

        this.totalActivities = organizationStatistics?.ActiveActivities.Count + organizationStatistics?.PastActivities.Count ?? 0;

        this.isAnyParticipant = organizationStatistics?.AgeDoughnut.Count() > 0 && organizationStatistics?.GenderDoughnut.Count() > 0;

        this.activities = await UpcommingActivitiesAsync(5);     
        
        this.QuestionsSurveyStatistics = await this.SurveyService.GetStatisticsByOrganizationIdAsync(SelectedOrganization.Id, statisticsDateRange.Start.Value, statisticsDateRange.End.Value);

        var resulr = QuestionsSurveyStatistics.ToList();



       this.MoreQuestionStatistics = false;

        UpdateShownQuestionsSurveyStatistics();
    }

    private void UpdateShownQuestionsSurveyStatistics()
    {
        ShownQuestionsSurveyStatistics.Clear();
        StateHasChanged();
        ShownQuestionsSurveyStatistics = QuestionsSurveyStatistics.IntersectBy(defaultQuestionsForStatistics, qs => qs.SurveyQuestion.UniqueQuestionId).ToList();
        ShowingQuestionsForSurveyStatistics();
    }


    private async Task OrgStatisticsDateTimePickerClosed()
    {
        await UpdateOrgStatisticsDataAsync();        
    }

    private void ShowingQuestionsForSurveyStatistics()
    {

        var questionSurveyStatistics = QuestionsSurveyStatistics.ExceptBy(defaultQuestionsForStatistics, qs => qs.SurveyQuestion.UniqueQuestionId).ToList();

        if (MoreQuestionStatistics)        
            ShownQuestionsSurveyStatistics.AddRange(questionSurveyStatistics);       
        else
        {
            foreach (var question in questionSurveyStatistics)           
                ShownQuestionsSurveyStatistics.Remove(question);           
        }        
    }   
    
    public async Task<OrganizationStatisticVM?> OrganizationStatisticsAsync(DateRange range)
    {
        return await this.OrganizationService.GetStatisticsByDateRangeAsync(SelectedOrganization!.Id, range.Start!.Value, range.End!.Value);
    }

    public void RowDataBound(RowDataBoundEventArgs<ActivityForGantt> args)
    {
        args.Row.AddClass(new string[] { "custom-row" });
    }
}