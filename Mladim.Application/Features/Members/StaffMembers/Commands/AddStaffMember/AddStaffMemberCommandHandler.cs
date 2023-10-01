using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
using System.Security.Claims;
using Mladim.Domain.Extensions;


namespace Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
public class AddStaffMemberCommandHandler : IRequestHandler<AddStaffMemberCommand, StaffMemberDetailsQueryDto>
{
  IMapper Mapper { get; }
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

        var claim = new Claim(Enum.GetName(request.Claim)!, request.OrganizationId.ToString());

        var user = await this.UnitOfWork.AppUserRepository.FindByEmailAsync(request.Email);

        if (user == null)
        {
            user = await CreateUserAsync(request.Name, request.Surname, request.Email);
            ArgumentNullException.ThrowIfNull(user);
            
            user.Organizations.Add(organization);         
            await this.AuthService.AddClaimAsync(user, claim);
            await this.UnitOfWork.StaffMemberRepository.AddAsync(staffMember);
            await this.UnitOfWork.SaveChangesAsync();


            var emailToken = await this.AuthService.EmailTokenAsync(user);            

            var registrationUrl = $"{HttpContextAccessor?.HttpContext?.AppBaseUrl()}/registration?EmailId={emailToken}";
            var emailContent = string.Format(this.EmailContent.ContentAddedNewUser, organization.Attributes.Name, registrationUrl);
            await SendEmailAsync(emailContent, request.Email);
        }
        else
        {
            if (await this.UnitOfWork.OrganizationRepository.IsUserInOrganizationAsync(user.Id, organization.Id))
                throw new Exception("Uporabnik je že dodan v organizacijo");

            user.Organizations.Add(organization);
            await this.AuthService.AddClaimAsync(user, claim);
            await this.UnitOfWork.StaffMemberRepository.AddAsync(staffMember);
            await this.UnitOfWork.SaveChangesAsync();

            if (Enum.TryParse(claim.Type, out ApplicationClaim appClaim))
            {

                var emailContent = string.Format(this.EmailContent.ContentUserAddedNewOrganization, organization.Attributes.Name, appClaim.GetDisplayAttribute());
                await SendEmailAsync(emailContent, request.Email);
            }
            else
                throw new Exception("Izbrani tip uporabnika ne obstaja");
        }

        return this.Mapper.Map<StaffMemberDetailsQueryDto>(staffMember);
    }



   

    private async Task<bool> SendEmailAsync(string content, string receipent)
    {
        var email = new Email(EmailContent.Subject, content, receipent, EmailContent.Sender);

        return await this.EmailService.SendEmailAsync(email);
    }

    private async Task<AppUser?> CreateUserAsync(string name, string surname, string email)
    {
        var responseUser = await this.AuthService.RegisterAsync(name, surname, name, email);

        if (!responseUser.Succeeded)
            throw new Exception(responseUser.Message);

        var userId = responseUser.Value!.UserId;

        return await this.UnitOfWork.AppUserRepository.FindByIdAsync(userId);
    }

}
