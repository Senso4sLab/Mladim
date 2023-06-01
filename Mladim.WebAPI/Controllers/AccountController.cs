using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Features.Accounts.Commands.UpdateAppUser;
using Mladim.Application.Features.Accounts.Queries.GetAppUser;
using Mladim.Application.Models;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;

namespace Mladim.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private IAuthService AuthService { get; }
    private IMediator Mediater { get; }

    public AccountController(IMediator mediator, IAuthService authService)
    {
        Mediater = mediator;
        AuthService = authService;
    }


    [HttpPost("register")]
    public async Task<ActionResult<Result<RegistrationResponse>>> RegisterAsync(RegistrationUser request)
    {
        var response = await this.AuthService.RegisterAsync(request);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<Result<AuthResponse>>> LoginAsync(LoginUser userDto)
    {
        var response = await this.AuthService.LoginAsync(userDto);
        return Ok(response);
    }

    [Authorize]
    [HttpPut("profile")]
    public async Task<ActionResult<int>> UserProfileAsync(UpdateAppUserCommand appUserCommand)
    {
        var response = await this.Mediater.Send(appUserCommand);
        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<AppUserQueryDto>> GetUserAccount(string userId)
    {
        var response = await this.Mediater.Send(new GetAppUserQuery { UserId = userId} );
        return Ok(response);
    }

}
