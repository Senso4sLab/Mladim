using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mladim.Application.Contracts.EmailService;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
using Mladim.Application.Models;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Commands.ResetStaffMemberPassword;

public class ResetStaffMemberPasswordCommandHandler : IRequestHandler<ResetStaffMemberPasswordCommand, bool>
{
    IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    public IAuthService AuthService { get; }
    public IEmailService EmailService { get; }
    public PredefinedEmailContent EmailContent { get; }
    public IHttpContextAccessor HttpContextAccessor { get; }  


    public ResetStaffMemberPasswordCommandHandler(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IAuthService authService, IEmailService emailService, IOptions<PredefinedEmailContent> emailContent, IMapper mapper)
    {
        HttpContextAccessor = httpContextAccessor;
        UnitOfWork = unitOfWork;
        AuthService = authService;
        EmailService = emailService;
        EmailContent = emailContent.Value;
        Mapper = mapper;
    }


    public async Task<bool> Handle(ResetStaffMemberPasswordCommand request, CancellationToken cancellationToken)
    {
        var staffMember = await UnitOfWork.AppUserRepository.FindByEmailAsync(request.Email);

        if (staffMember == null)
            return false;

        var passwordToken = await AuthService.GeneratePasswordResetTokenAsync(staffMember);

        // TODO 
        //var emailContent = string.Format(this.EmailContent.ContentUserAddedNewClaim, passwordToken);
        //await SendEmailAsync(emailContent, request.Email);

        return true;
    }

    private async Task<bool> SendEmailAsync(string content, string receipent)
    {
        var email = new Email(EmailContent.Subject, content, receipent, EmailContent.Sender);

        return await this.EmailService.SendEmailAsync(email);
    }
}
