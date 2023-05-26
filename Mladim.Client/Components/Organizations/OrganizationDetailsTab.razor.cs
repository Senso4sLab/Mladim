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

namespace Mladim.Client.Components.Organizations;

public partial class OrganizationDetailsTab
{
    [Parameter]
    public OrganizationVM Organization { get; set; }

    [Parameter]
    public bool ReadOnly { get; set; }

    public TextEditor? textEditor;


    public async Task LoadHtmlFromTextEditor() =>
        await textEditor!.LoadHtmlText();
}