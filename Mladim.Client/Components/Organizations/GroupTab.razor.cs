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
using Mladim.Client.Services.PopupService;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Enums;

namespace Mladim.Client.Components.Organizations;

public partial class GroupTab
{
    private bool IsStaffGroup = false;

    private bool ShowActiveGroups = true;

    [Inject]
    public IGroupService GroupService { get; set; } = default!;

    [Inject]
    public IPopupService PopupService { get; set; } = default!;

    [Parameter]
    public OrganizationVM Organization { get; set; } = default!;

    private List<GroupVM> Groups = new List<GroupVM>();

    private GroupType GroupType => 
        IsStaffGroup ? GroupType.Project : GroupType.Activity;

    //protected async override Task OnParametersSetAsync() =>    
    //    this.Groups = new(await this.GroupService.
    //        GetByOrganizationIdAsync(this.Organization.Id, this.GroupType, ShowActiveGroups));
                

    private void GroupSwitchChangedAsync(bool isStaffGroup)
    {
        this.StateHasChanged();
        this.IsStaffGroup = isStaffGroup;
    }

    private void ShowActiveGroupChangedAsync(bool activeGroups)
    {
        this.StateHasChanged();
        this.ShowActiveGroups = activeGroups;
    }


    public async Task OnCreateGroupAsync()
    {

    }

    public async Task UpdateGroupAsync(GroupVM group)
    {

    }


}