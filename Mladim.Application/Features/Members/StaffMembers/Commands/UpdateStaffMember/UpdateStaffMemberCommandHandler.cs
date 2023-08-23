using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Mladim.Application.Contracts.EmailService;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Models;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Commands.UpdateStaffMember;

public class UpdateStaffMemberCommandHandler : IRequestHandler<UpdateStaffMemberCommand, int>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    public IAuthService AuthService { get; }
    public IEmailService EmailService { get; }
    public PredefinedEmailContent EmailContent { get; }

    public UpdateStaffMemberCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, IEmailService emailService, IOptions<PredefinedEmailContent> emailContent, IMapper mapper)
    {      
        UnitOfWork = unitOfWork;
        AuthService = authService;
        EmailService = emailService;
        EmailContent = emailContent.Value;
        Mapper = mapper;
    } 

    public async Task<int> Handle(UpdateStaffMemberCommand request, CancellationToken cancellationToken)
    {
       var staffMember = await this.UnitOfWork.StaffMemberRepository
            .FirstOrDefaultAsync(sm => sm.Id == request.Id); 

       ArgumentNullException.ThrowIfNull(staffMember);
       
       staffMember = this.Mapper.Map(request, staffMember);
       this.UnitOfWork.StaffMemberRepository.Update(staffMember);

       var claim = new Claim(Enum.GetName(request.Claim)!, staffMember.OrganizationId.ToString());

       // var appUser = this.AuthService.ExistAppUserAsync

       //if (await this.AuthService.UpsertClaimAsync(appUser, claim))
       //{
       //     var emailContent = string.Format(this.EmailContent.ContentUserAddedNewClaim, organization.Attributes.Name, Enum.GetName(request.Claim));
       //     await SendEmailAsync(emailContent, request.Email);
       // }
     
       
       return await this.UnitOfWork.SaveChangesAsync();
    }

    private async Task SendEmailAsync(string content, string receipent)
    {
        var email = new Email(EmailContent.Subject, content, receipent, EmailContent.Sender);

        var response = await this.EmailService.SendEmailAsync(email);

        if (!response)
            throw new Exception("Email was not send");
    }
}
