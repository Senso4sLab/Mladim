using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
using Mladim.Application.Features.Members.StaffMembers.Commands.UpdateStaffMember;
using Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMember;
using Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMembers;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;

namespace Mladim.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StaffMemberController : ControllerBase
{
    private IMediator Mediator { get; }
    public StaffMemberController(IMediator mediator)
    {
        this.Mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<StaffMemberDetailsQueryDto?>> AddAsync(AddStaffMemberCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }


    [HttpPut]
    public async Task<ActionResult<int>> UpdateAsync(UpdateStaffMemberCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberBase>>> GetAsync([FromQuery] GetStaffMembersQuery query)
    {
        var response = await this.Mediator.Send(query);
        return Ok(response);
    }


    [HttpGet("{memId}")]
    public async Task<ActionResult<StaffMemberDetailsQueryDto?>> GetAsync(int memId)
    {
        var response = await this.Mediator.Send(new GetStaffMemberQuery { StaffMemberId = memId });
        return Ok(response);
    }
}
