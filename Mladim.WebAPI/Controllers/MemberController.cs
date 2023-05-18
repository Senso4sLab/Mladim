using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mladim.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemberController : ControllerBase
{
    private IMediator Mediator { get; }
    public MemberController(IMediator mediator)
    {
        this.Mediator = mediator;
    }
}
