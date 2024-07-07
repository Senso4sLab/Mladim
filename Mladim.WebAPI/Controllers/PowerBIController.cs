using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.PowerBI.Queries.GetADToken;
using Mladim.Domain.Dtos.PowerBI;

namespace Mladim.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PowerBIController : ControllerBase
{
    private IMediator Mediator { get; }
    public PowerBIController(IMediator mediator)
    {
        this.Mediator = mediator;
    }


    [HttpGet]
    public async Task<ActionResult<EmbeddedReportDto>> GetEmbeddedReport()
    {
        var response = await this.Mediator.Send(new GetEmbeddedReportQuery());
        return Ok(response);
    }

}
