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
using Mladim.Client.MappingProfiles.Profiles.Participants;

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

    protected async override Task OnParametersSetAsync() =>
        this.Groups = new(await this.GroupService.
            GetByOrganizationIdAsync(this.Organization.Id, this.GroupType, ShowActiveGroups));


    private async Task GroupSwitchChangedAsync(bool isStaffGroup)
    {       
        this.IsStaffGroup = isStaffGroup;
        await OnParametersSetAsync();
    }

    private async Task ShowActiveGroupChangedAsync(bool activeGroups)
    {      
        this.ShowActiveGroups = activeGroups;
        await OnParametersSetAsync();
    }


    public async Task OnCreateGroupAsync()
    {
        var group = new GroupVM();

        var groupResponse = await this.PopupService.ShowGroupDialog("Nova skupina", group, GroupType, Organization.Id);

        if (!groupResponse)
            return;

        group = await this.GroupService.AddAsync(this.Organization.Id, group, GroupType);

        if (group != null)
        {
            Groups.Add(group);
            this.PopupService.ShowSnackbarSuccess("Skupina uspešno dodana");
        }
        else
            this.PopupService.ShowSnackbarError();
    }

    public async Task UpdateGroupAsync(GroupVM group)
    {      
        var detailsGroup = await this.GroupService.GetByGroupIdAsync(group.Id);

        ArgumentNullException.ThrowIfNull(detailsGroup);       

        var groupResponse = await this.PopupService.ShowGroupDialog("Uredi skupino", detailsGroup, GroupType, Organization.Id);

        if (!groupResponse)
            return;

        var response = await this.GroupService.UpdateAsync(detailsGroup);

        if (response)
        {
            this.PopupService.ShowSnackbarSuccess("Podatki uspešno posodobljeni");
            await OnParametersSetAsync();
        }
        else
            this.PopupService.ShowSnackbarError();
    }

    private string RowStyleFunc(GroupVM participant, int index)
    {
        string rowCss = "font-size: 0.8rem; font-family:poppins; font-weight:400; line-height:1.0; letter-spacing:-0.024rem; color:#6e7191;";

        return index % 2 == 0 ? rowCss + "background-color:white;" : rowCss + "background-color:#EFEFEF;";
    }


}