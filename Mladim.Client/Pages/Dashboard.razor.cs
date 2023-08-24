using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Mladim.Client;
using Mladim.Client.Shared;
using Mladim.Client.Services.Authentication;
using Mladim.Client.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Layouts;
using Mladim.Client.Extensions;
using Mladim.Client.Components.Organizations;
using Blazored.TextEditor;
using MudBlazor;
using Syncfusion.Blazor;
using Syncfusion.Blazor.RichTextEditor;
using Syncfusion.Blazor.Gantt;
using Mladim.Client.Models;
using Mladim.Client.Services.SubjectServices.Contracts;
using Syncfusion.Blazor.Schedule.Internal;

namespace Mladim.Client.Pages;

public partial class Dashboard
{   

    [Inject]
    public IOrganizationService OrganizationService { get; set; } = default!;

    [CascadingParameter]
    public OrganizationVM? SelectedOrganization { get; set; }

    private int selectedYear = DateTime.UtcNow.Year;
  
    private IEnumerable<int> availableYears = new List<int>();

   

    protected override void OnParametersSet()
    {
       if(SelectedOrganization != null && !availableYears.Any())
            availableYears = this.AvailableYearsForSelectedOrganization();
    }




    

    



    private IEnumerable<int> AvailableYearsForSelectedOrganization()
    {
        int createdYear = SelectedOrganization!.Attributes.CreatedStamp.Year;
        return Enumerable.Range(createdYear, DateTime.UtcNow.Year - createdYear + 1).ToList();
    }


    private Task OnYearChangedAsync(int selectedYear)
    {
        this.selectedYear = selectedYear;
        return Task.CompletedTask;
    }
}