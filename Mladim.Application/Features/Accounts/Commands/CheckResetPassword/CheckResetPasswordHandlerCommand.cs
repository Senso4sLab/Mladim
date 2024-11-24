using MediatR;
using Microsoft.AspNetCore.Identity;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Features.Accounts.Commands.ResetPassword;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Accounts.Commands.CheckResetPassword;

public class CheckResetPasswordHandlerCommand : IRequestHandler<CheckResetPasswordCommand, Result>
{
    private UserManager<AppUser> UserManager { get; } = default!;

    private IAuthService AuthService { get; } = default!;

    public CheckResetPasswordHandlerCommand(UserManager<AppUser> userManager, IAuthService authService)
    {
        this.UserManager = userManager;
        this.AuthService = authService;
    }

    public async Task<Result> Handle(CheckResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var appUser = await this.UserManager.FindByEmailAsync(request.Email);       

        if (appUser == null)
            return Result.Error("Uporabnik ne obstaja.");

        return await AuthService.ResetPasswordRequestAsync(appUser, request.Token, request.Password);
    }
}
