using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Organizations.Commands.AddOrganization;
using Mladim.Application.Features.Organizations.Commands.UpdateOrganization;

namespace Mladim.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationController : ControllerBase
{
    private IMediator Mediator { get;}
    public OrganizationController(IMediator mediator)
    {
        this.Mediator = mediator;
    }

    [HttpPost]    
    public async Task<ActionResult<OrganizationDto>> AddOrganizationAsync(AddOrganizationCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<OrganizationDto>> UpdateOrganizationAsync(UpdateOrganizationCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }
}
