using Mladim.Domain.Models;

namespace Mladim.Application.Contracts.PowerBIService;

public interface IPowerBIService
{
    Task<string> GetAccessToken();
    Task<EmbeddedReport> GetReport(Guid WorkspaceId, Guid ReportId);
}