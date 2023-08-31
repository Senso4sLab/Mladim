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
            throw new ArgumentException("Vnešeni mail se ne ujema");

        var appUser = await this.UnitOfWork.AppUserRepository.FindByEmailAsync(request.Email);
        
        if(appUser == null)
            throw new ArgumentException("Uporabnik z vnešenim email naslovom ne obstaja");

        if (!await this.UnitOfWork.OrganizationRepository.IsUserInOrganizationAsync(appUser.Id,staffMember.OrganizationId))
            throw new ArgumentException("Uporabnik nima dodeljene zahtevane organizacije");
        

        staffMember = this.Mapper.Map(request, staffMember);
        this.UnitOfWork.StaffMemberRepository.Update(staffMember);

        var dbResponse = await this.UnitOfWork.SaveChangesAsync();

        var claim = new Claim(Enum.GetName(request.Claim)!, staffMember.OrganizationId.ToString());

        if (await this.AuthService.ReplaceClaimAsync(appUser, claim))
        {
            var organization = await this.UnitOfWork.OrganizationRepository.FirstOrDefaultAsync(o => o.Id == staffMember.OrganizationId, false);
            var emailContent = string.Format(this.EmailContent.ContentUserAddedNewClaim, organization!.Attributes.Name, claim.Type);
            await SendEmailAsync(emailContent, request.Email);
        }

        return dbResponse;
    }

    private async Task<bool> SendEmailAsync(string content, string receipent)
    {
        var email = new Email(EmailContent.Subject, content, receipent, EmailContent.Sender);

        return await this.EmailService.SendEmailAsync(email);      
    }
}
