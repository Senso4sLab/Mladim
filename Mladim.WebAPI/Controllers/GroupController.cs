using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Activities.Commands.AddActivity;
using Mladim.Application.Features.Activities.Commands.RemoveActivity;
using Mladim.Application.Features.Activities.Commands.UpdateActivity;
using Mladim.Application.Features.Activities.Queries.GetActivities;
using Mladim.Application.Features.Activities.Queries.GetActivity;
using Mladim.Application.Features.Groups.Commands.AddGroup;
using Mladim.Application.Features.Groups.Commands.UpdateGroup;
using Mladim.Application.Features.Groups.Queries.GetGroup;
using Mladim.Application.Features.Groups.Queries.GetGroups;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Group;

namespace Mladim.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GroupController : ControllerBase
{
    private IMediator Mediator { get; }
    public GroupController(IMediator mediator)
    {
        this.Mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<GroupQueryDto?>> AddAsync(AddGroupCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<int>> UpdateAsync(UpdateGroupCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("{groupId}")]
    public async Task<ActionResult<GroupDetailsQueryDto?>> GetAsync(int groupId)
    {
        var response = await this.Mediator.Send(new GetGroupQuery { GroupId = groupId });
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroupQueryDto>>> GetAllByQueryAsync([FromQuery] GetGroupsQuery query)
    {
        var response = await this.Mediator.Send(query);
        return Ok(response);
    }

}
