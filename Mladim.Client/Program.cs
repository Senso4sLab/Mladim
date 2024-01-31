using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


using Mladim.Client;
using Mladim.Client.Services.Authentication;
using Mladim.Client.Services.HttpService.Generic;
using Mladim.Client.Services.PopupService;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.Services.SubjectServices.Implementations;
using MudBlazor;
using MudBlazor.Services;
using System.Reflection;
using Mladim.Client.Models;
using Mladim.Client.Services.AccountService;
using Mladim.Client.Services.FileService;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Authorization;
using Mladim.Client.Authorization;
using Microsoft.Extensions.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 6000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    config.PopoverOptions.ThrowOnDuplicateProvider = false;

});

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddHttpClient("MladimHttpClient", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).AddHttpMessageHandler<HttpAuthorizationHandler>();

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>()!.CreateClient("MladimHttpClient"));

builder.Services.AddSyncfusionBlazor();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());    
builder.Services.AddTransient<HttpAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, UserHasWorkerClaimHandler>();

builder.Services.AddSingleton<IAuthorizationHandler, UserHasWorkerClaimHandler>();
builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("HasWorkerClaim", policy => policy.Requirements.Add(new UserHasWorkerClaim()));
});

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddScoped<IPopupService, PopupService>(); 
{
    builder.Services.AddTransient<IAuthService, AuthService>();
    builder.Services.AddScoped<IGenericHttpService, GenericHttpService>();
    builder.Services.AddScoped<IOrganizationService, OrganizationService>();
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<IActivityService, ActivityService>();
    builder.Services.AddScoped<IStaffMemberService, StaffMemberService>();
    builder.Services.AddScoped<IParticipantService, ParticipantService>();
    builder.Services.AddScoped<IPartnerService, PartnerService>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IGroupService, GroupService>();
    builder.Services.AddScoped<IFileService, FileService>();
    builder.Services.AddScoped<ISurveyService, SurveyService>();   
}
{
    builder.Services.Configure<MladimApiUrls>(builder.Configuration.GetSection("MladimApiUrls"));
    builder.Services.Configure<StorageKeys>(builder.Configuration.GetSection("StorageKeys"));
}


Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH1dcXVRRGleVUdyX0A=");



await builder.Build().RunAsync();

