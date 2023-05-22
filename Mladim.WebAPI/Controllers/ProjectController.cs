using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Organizations.Commands.AddOrganization;
using Mladim.Application.Features.Organizations.Commands.DeleteOrganization;
using Mladim.Application.Features.Organizations.Queries.GetOrganization;
using Mladim.Application.Features.Organizations.Queries.GetOrganizations;
using Mladim.Application.Features.Projects.Commands.AddProject;
using Mladim.Application.Features.Projects.Commands.RemoveProject;
using Mladim.Application.Features.Projects.Commands.UpdateProject;
using Mladim.Application.Features.Projects.Queries.GetProjectDetails;
using Mladim.Application.Features.Projects.Queries.GetProjects;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;

namespace Mladim.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private IMediator Mediator { get; }
    public ProjectController(IMediator mediator)
    {
        this.Mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto?>> AddAsync(AddProjectCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<int>> UpdateAsync(UpdateProjectCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{projectId}")]
    public async Task<ActionResult<bool>> RemoveAsync(int projectId)
    {
        var response = await this.Mediator.Send(new RemoveProjectCommand { ProjectId = projectId });
        return Ok(response);
    }


    [HttpGet("{projectId}")]
    public async Task<ActionResult<ProjectDto?>> GetAsync(int projectId)
    {
        var response = await this.Mediator.Send(new GetProjectQuery { ProjectId = projectId });
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllByOrganizationAsync([FromQuery] int organizationId)
    {
        var response = await this.Mediator.Send(new GetProjectsByOrganizationQuery { OrganizationId = organizationId });
        return Ok(response);
    }
}
