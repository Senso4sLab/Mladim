using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Organizations;
using Mladim.Application.Features.Organizations.Commands.AddOrganization;
using Mladim.Application.Features.Organizations.Commands.DeleteOrganization;
using Mladim.Application.Features.Organizations.Commands.UpdateOrganization;
using Mladim.Application.Features.Organizations.Commands.UserGetOrganization;
using Mladim.Application.Features.Organizations.Queries.GetOrganization;
using Mladim.Application.Features.Organizations.Queries.GetOrganizations;
using Mladim.Domain.Dtos;

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
    public async Task<ActionResult<OrganizationQueryDto?>> AddAsync(AddOrganizationCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<int>> UpdateAsync(UpdateOrganizationCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{orgId}")]
    public async Task<ActionResult<bool>> RemoveAsync(int orgId)
    {
        var response = await this.Mediator.Send(new RemoveOrganizationCommand { OrganizationId = orgId });
        return Ok(response);
    }

    [HttpGet("{orgId}")]
    public async Task<ActionResult<OrganizationQueryDto?>> GetAsync(int orgId)
    {
        var response = await this.Mediator.Send(new GetOrganizationQuery { OrganizationId = orgId });
        return Ok(response);
    }

    [HttpGet("{orgId}/assignedTo/{userId}")]
    public async Task<ActionResult<bool>> AssignOrganizationToUser(int orgId, string userId)
    {
        var response = await this.Mediator.Send(new AssignOrganizationCommand { AppUserId = userId, OrganizationId = orgId });
        return Ok(response);
    }



    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrganizationQueryDto>>> GetAllAsync([FromQuery] string userId)
    {
        var response = await this.Mediator.Send(new GetOrganizationsQuery { AppUserId = userId });
        return Ok(response);
    }
}
