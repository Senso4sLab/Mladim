using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Mladim.Application.Contracts.EmailService;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Extensions;
using Mladim.Application.Models;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;


namespace Mladim.Application.Features.Accounts.Commands.ResetPassword;

public class ResetPasswordHandlerCommand : IRequestHandler<ResetPasswordCommand, Result>
{
    private IAuthService AuthService { get; } = default!;   

    private IEmailService EmailService { get; } = default!; 

    private UserManager<AppUser> UserManager { get; } = default!;

    public PredefinedEmailContent EmailContent { get; }

    public IHttpContextAccessor HttpContextAccessor { get; }

    public ResetPasswordHandlerCommand(IAuthService authService, UserManager<AppUser> userManager, IEmailService emailService, IOptions<PredefinedEmailContent> emailContent, IHttpContextAccessor httpContextAccessor )
    {
        this.AuthService = authService;
        this.UserManager = userManager;
        this.EmailService = emailService;
        this.EmailContent = emailContent.Value;
        this.HttpContextAccessor = httpContextAccessor;
    }
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await this.UserManager.FindByEmailAsync(request.Email);      

        if (user == null)
            return Result.Error("Vnešeni podatki so napačni");

        if (user.ResetPasswordUtc != null && ((DateTime.UtcNow - new DateTime(user.ResetPasswordUtc.Value)).TotalSeconds  <= 600))
            return Result.Error("Zahtevek za določitev novega gesla je časovno omejen. Poskusite kasneje.");

        user.ResetPasswordUtc = DateTime.UtcNow.Ticks;

        var identityResult = await this.UserManager.UpdateAsync(user);

        if (!identityResult.Succeeded)
            return Result.Error("Prišlo je do napake!");

        var token = await this.AuthService.GeneratePasswordResetTokenAsync(user);        
     
        var passwordUrl = $"{HttpContextAccessor?.HttpContext?.AppBaseUrl()}/newpassword?PasswordId={token}";        
      
        var emailContent = string.Format(this.EmailContent.ContentResetPassword, passwordUrl);    

        if (await SendEmailAsync(emailContent, user.Email))
            return Result.Success();
        else
            return Result.Error("Emaila ni bilo mogoče poslati!");        
    }

    private async Task<bool> SendEmailAsync(string content, string receipent)
    {
        var email = new Email(EmailContent.Subject, content, receipent, EmailContent.Sender);

        return await this.EmailService.SendEmailAsync(email);
    }
}
