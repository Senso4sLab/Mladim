using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Mladim.Application.Contracts.EmailService;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Extensions;
using Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
using Mladim.Application.Models;
using Mladim.Domain.Dtos;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;

namespace Mladim.Application.Features.Members.StaffMembers.Commands.ResetStaffMemberPassword;

public class ResendConfiramtionEmailCommandHandler : IRequestHandler<ResendConfiramtionEmailCommand, Result>
{
    IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    public IAuthService AuthService { get; }
    public IEmailService EmailService { get; }
    public PredefinedEmailContent EmailContent { get; }
    public IHttpContextAccessor HttpContextAccessor { get; }  

    public UserManager<AppUser> UserManager { get; }

    public ResendConfiramtionEmailCommandHandler(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IAuthService authService, IEmailService emailService, IOptions<PredefinedEmailContent> emailContent, IMapper mapper, UserManager<AppUser> userManager)
    {
        HttpContextAccessor = httpContextAccessor;
        UnitOfWork = unitOfWork;
        AuthService = authService;
        EmailService = emailService;
        EmailContent = emailContent.Value;
        Mapper = mapper;
        UserManager = userManager;
    }


    public async Task<Result> Handle(ResendConfiramtionEmailCommand request, CancellationToken cancellationToken)
    {
        StaffMember? member = await this.UnitOfWork.StaffMemberRepository.FirstOrDefaultAsync(sm => sm.Email == request.Email && sm.OrganizationId == request.OrganizationId);

        var user = await this.UnitOfWork.AppUserRepository.FindByEmailAsync(request.Email);

        if (member is null)
            return Result.Error("Uporabnik je potrjen."); 

        var emailToken = await this.AuthService.GenerateEmailTokenAsync(user!);
        var registrationUrl = $"{HttpContextAccessor?.HttpContext?.AppBaseUrl()}/registration?EmailId={emailToken}";
        string content = string.Format(this.EmailContent.ContentAddedNewUser, registrationUrl);

        if (!await SendEmailAsync(content, request.Email))
            return Result.Error("Prišlo je to napake pri pošiljanju emaila.");       
        
        member!.EmailSent = DateTime.UtcNow;
        await this.UnitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    private async Task<bool> SendEmailAsync(string content, string receipent)
    {
        var email = new Email(EmailContent.Subject, content, receipent, EmailContent.Sender);

        return await this.EmailService.SendEmailAsync(email);
    }
}
