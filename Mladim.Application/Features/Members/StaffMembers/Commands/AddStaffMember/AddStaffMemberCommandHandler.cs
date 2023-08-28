using AutoMapper;
using AutoMapper.Execution;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Mladim.Application.Contracts.EmailService;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Extensions;
using Mladim.Application.Models;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
public class AddStaffMemberCommandHandler : IRequestHandler<AddStaffMemberCommand, StaffMemberDetailsQueryDto>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    public IAuthService AuthService { get; }
    public IEmailService EmailService { get; } 
    public PredefinedEmailContent EmailContent { get; }
    public IHttpContextAccessor HttpContextAccessor { get; }  


    public AddStaffMemberCommandHandler(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IAuthService authService, IEmailService emailService, IOptions<PredefinedEmailContent> emailContent, IMapper mapper)
    {       
            HttpContextAccessor = httpContextAccessor;
            UnitOfWork = unitOfWork;
            AuthService = authService;
            EmailService = emailService;
            EmailContent = emailContent.Value;
            Mapper = mapper;       
    }
   
    public async Task<StaffMemberDetailsQueryDto> Handle(AddStaffMemberCommand request, CancellationToken cancellationToken)
    {
        var organization = await this.UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        ArgumentNullException.ThrowIfNull(organization);

        var staffMember = this.Mapper.Map<StaffMember>(request);

        var claimName = Enum.GetName(request.Claim)!;

        var claim = new Claim(claimName, request.OrganizationId.ToString());

        var appUser = await this.AuthService.ExistAppUserAsync(request.Email);

        if (appUser == null)
        {
            // nov app user
            appUser = await this.AuthService.CreateUserWithClaimAsync(request.Name, request.Surname, request.Email, claim);
            appUser.Organizations.Add(organization);

            await this.UnitOfWork.AppUserRepository.AddAsync(appUser);
            await this.UnitOfWork.StaffMemberRepository.AddAsync(staffMember);
            await this.UnitOfWork.SaveChangesAsync();

            var registrationUrl = $"{HttpContextAccessor?.HttpContext?.AppBaseUrl()}/registration/{appUser.Id}";

            var emailContent = string.Format(this.EmailContent.ContentAddedNewUser, organization.Attributes.Name, registrationUrl); 

            await SendEmailAsync(emailContent, request.Email);
        }
        else
        {
           if (await this.UnitOfWork.AppUserRepository.IsUserInOrganizationAsync(appUser.Id, organization.Id)) 
                throw new Exception("Uporabnik je že dodan v organizaciji");

            appUser.Organizations.Add(organization);
            await this.UnitOfWork.SaveChangesAsync();

            await this.AuthService.UpsertClaimAsync(appUser, claim);                      
            var emailContent = string.Format(this.EmailContent.ContentUserAddedNewOrganization, organization.Attributes.Name, claimName);
            await SendEmailAsync(emailContent, request.Email);                       
        }   

        return this.Mapper.Map<StaffMemberDetailsQueryDto>(staffMember);

    }


    private async Task SendEmailAsync(string content, string receipent)
    {
        var email = new Email(EmailContent.Subject, content, receipent, EmailContent.Sender);

        var response = await this.EmailService.SendEmailAsync(email);

        if (!response)
            throw new Exception("Email was not send");
    }

}
