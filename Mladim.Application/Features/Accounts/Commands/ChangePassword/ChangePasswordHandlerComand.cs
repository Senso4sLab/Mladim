using MediatR;
using Microsoft.AspNetCore.Identity;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;

namespace Mladim.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordHandlerComand : IRequestHandler<ChangePasswordCommand, Result>
{  

    private UserManager<AppUser> UserManager { get; } = default!;

    private IAuthService AuthService { get; } = default!;

    public ChangePasswordHandlerComand(IAuthService authService, UserManager<AppUser> userManager)
    {
        this.AuthService = authService; 
        this.UserManager = userManager;       
    }
    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var appUser = await this.UserManager.FindByIdAsync(request.UserId);

        if (appUser != null)
            return await this.AuthService.ChangePasswordrequestAsync(appUser, request.OldPassword, request.Password);
        else
            return Result.Error("Uporabnik ne obstaja!");
    }
}
