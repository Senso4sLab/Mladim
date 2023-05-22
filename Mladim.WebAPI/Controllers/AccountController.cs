using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Models;

namespace Mladim.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private IAuthService AuthService { get; }

    public AccountController(IAuthService authService)
    {
        AuthService = authService;
    }


    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
    {
        var response = await this.AuthService.RegisterAsync(request);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> LoginAsync(AuthRequest userDto)
    {
        var response = await this.AuthService.LoginAsync(userDto);
        return Ok(response);
    }

}
