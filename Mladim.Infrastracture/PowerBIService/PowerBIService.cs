namespace Mladim.Infrastracture.PowerBI;

using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using Microsoft.Rest;
using Mladim.Application.Contracts.PowerBIService;
using Mladim.Domain.Models;

public class PowerBIService : IPowerBIService
{  
    private IConfiguration _configuration;


    public PowerBIService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static readonly string[] RequiredScopes = new string[] {
         "https://analysis.windows.net/powerbi/api/Report.Read.All"
     };

    public async Task<string> GetAccessToken()
    {
        var tenantId = _configuration["AzureAd:TenantId"];
        var clientId = _configuration["AzureAd:ClientId"];
        var clinetSecret = _configuration["AzureAd:ClientSecret"];
        var authorityUri = new Uri($"https://login.microsoftonline.com/{tenantId}");

        var app = ConfidentialClientApplicationBuilder
            .Create(clientId)
            .WithClientSecret(clinetSecret)
            .WithAuthority(authorityUri)
            .Build();

        var powerApiDefaultScope = "https://analysis.windows.net/powerbi/api/.default";
        var scopes = new string[] { powerApiDefaultScope };

        try
        {
            var authenticationResult = await app.AcquireTokenForClient(scopes).ExecuteAsync();
            return authenticationResult.AccessToken;
        }
        catch (Exception ex) 
        {
            return string.Empty;
        }
    }

    private PowerBIClient GetPowerBiClient(string accessToken)
    {
        var tokenCredentials = new TokenCredentials(accessToken, "Bearer");
        return new PowerBIClient(new Uri("https://api.powerbi.com"), tokenCredentials);
    }

    public async Task<EmbeddedReport> GetReport(Guid WorkspaceId, Guid ReportId)
    {
        try
        {
            var access_token = await GetAccessToken();
            PowerBIClient pbiClient = GetPowerBiClient(access_token);
            // Call the Power BI Service API to get embedding data
            var report = await pbiClient.Reports.GetReportInGroupAsync(WorkspaceId, ReportId);


            var tokenRequest = new GenerateTokenRequest(TokenAccessLevel.View, report.DatasetId);
            var embedTokenResponse = await pbiClient.Reports.GenerateTokenAsync(WorkspaceId, report.Id, tokenRequest);

            // Return report embedding data to caller
            return new EmbeddedReport(report.Id.ToString(), report.Name, report.EmbedUrl, embedTokenResponse.Token);
        }
        catch (Exception ex) 
        {
            return null;
        }

    }
}
