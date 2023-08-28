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

        if (request.Email != staffMember.Email)
            throw new ArgumentException("Mail uporabnika se ne ujema");

        var appUser = await this.AuthService.ExistAppUserAsync(request.Email);
        
        if(appUser == null)
            throw new ArgumentException("Uporabnik z vnešenim email naslovom ne obstaja");

        if (!await this.UnitOfWork.AppUserRepository.IsUserInOrganizationAsync(appUser.Id,staffMember.OrganizationId))
            throw new ArgumentException("Uporabnik nima dodeljene zahtevane organizacije");


        staffMember = this.Mapper.Map(request, staffMember);
        this.UnitOfWork.StaffMemberRepository.Update(staffMember);

        var claimName = Enum.GetName(request.Claim)!;
        var claim = new Claim(claimName, staffMember.OrganizationId.ToString());

        if (await this.AuthService.UpsertClaimAsync(appUser, claim))
        {
            var organization = await this.UnitOfWork.OrganizationRepository.FirstOrDefaultAsync(o => o.Id == staffMember.OrganizationId, false);
            var emailContent = string.Format(this.EmailContent.ContentUserAddedNewClaim, organization!.Attributes.Name, claimName);
            await SendEmailAsync(emailContent, request.Email);
        }

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
