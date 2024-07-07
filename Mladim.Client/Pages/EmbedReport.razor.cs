using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Mladim.Client.ViewModels.PowerBI;
using System.Net.Http.Json;

namespace Mladim.Client.Pages;

public partial class EmbedReport
{
    private IJSObjectReference embedModule;
    private EmbeddedReportVM report;

    [Inject]
    public IJSRuntime JS { get; set; }

    [Inject]
    public HttpClient HttpClient { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
       try
       {
            if(firstRender)
            {
                var embedModuleTask = JS.InvokeAsync<IJSObjectReference>("import", "./embed.js");
                var reportVMTask = HttpClient.GetFromJsonAsync<EmbeddedReportVM>("api/PowerBI");
                embedModule = await embedModuleTask;
                report = await reportVMTask;
                StateHasChanged();
            }
            else if(embedModule is not null && report is not null)
            {
                await embedModule.InvokeVoidAsync("embedReport", "embed-container", report.Id, report.EmbedUrl, report.Token);
            }
            else
            {

            }
       }
       catch(Exception ex)
       { 
       
       }
    }

}