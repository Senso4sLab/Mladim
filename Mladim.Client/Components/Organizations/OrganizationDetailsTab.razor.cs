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

using Blazored.TextEditor;
using MudBlazor;
using Mladim.Domain.Dtos;
using Mladim.Client.Services.FileService;
using System.Runtime.CompilerServices;

namespace Mladim.Client.Components.Organizations;

public partial class OrganizationDetailsTab
{
    [Parameter]
    public OrganizationVM Organization { get; set; } = default!;


    [Inject]

    IFileService FileService { get; set; } = default!;

    [Parameter]
    public bool ReadOnly { get; set; }

    //public TextEditor? textEditor;
    //public async Task LoadHtmlFromTextEditor() =>
    //    await textEditor!.LoadHtmlText();


    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }

    private async Task UploadFiles(InputFileChangeEventArgs e)
    {     
        var resizedImage = await e.File.RequestImageFileAsync(e.File.ContentType, 200, 200);
       
        var buffer = new byte[resizedImage.Size];
        await resizedImage.OpenReadStream().ReadAsync(buffer);

        string fileName = Path.GetFileName(resizedImage.Name);

        var profileUrl =  await this.FileService.AddOrganizationProfileImageAsync(this.Organization.Id, buffer.ToList(), fileName);

        this.Organization.Attributes.LogoUrl = profileUrl;

        this.StateHasChanged();
    }

}