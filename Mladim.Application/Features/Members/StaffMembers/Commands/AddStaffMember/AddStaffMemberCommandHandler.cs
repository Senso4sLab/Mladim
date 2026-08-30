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
using System.Text.Json;
using System.Xml.Linq;


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
   

    // 1) narediš app userja dodaš mu organizacijo
    // 2) ali pa dodaš organizacijo

    public async Task<StaffMemberDetailsQueryDto> Handle(AddStaffMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await this.UnitOfWork.StaffMemberRepository.FirstOrDefaultAsync(sm => sm.Email == request.Email && sm.OrganizationId == request.OrganizationId);

        if (member is not null)        
            throw new Exception("Uporabnik že obstaja v organizaciji!");

        var organization = await this.UnitOfWork.OrganizationRepository.FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        ArgumentNullException.ThrowIfNull(organization);        

        var user = await this.UnitOfWork.AppUserRepository.FindByEmailAsync(request.Email);
        var claim = new Claim(Enum.GetName(request.Claim)!, request.OrganizationId.ToString());

        string emailContent = string.Empty;

        if (user is null)
        {
            user = await CreateUserAsync(request.Name, request.Surname, request.Email);

            var emailToken = await this.AuthService.GenerateEmailTokenAsync(user);
            var registrationUrl = $"{HttpContextAccessor?.HttpContext?.AppBaseUrl()}/registration?EmailId={emailToken}";
            emailContent = string.Format(this.EmailContent.ContentAddedNewUser, registrationUrl);
        }
        else
        {
            if (!Enum.TryParse(claim.Type, out ApplicationClaim appClaim))
                throw new Exception("Izbrani tip uporabnika ne obstaja");

            emailContent = string.Format(this.EmailContent.ContentUserAddedNewOrganization, appClaim.GetDisplayAttribute());
        }

        user.Organizations.Add(organization);
        await this.AuthService.AddClaimAsync(user, claim);
        
        member = this.Mapper.Map<StaffMember>(request);
        await this.UnitOfWork.StaffMemberRepository.AddAsync(member);       

        bool isEmailSend = await SendEmailAsync(emailContent, request.Email);

        if (isEmailSend)        
            member.EmailSent = DateTime.UtcNow;
        

        await this.UnitOfWork.SaveChangesAsync();
        
        var staffMemberDto = this.Mapper.Map<StaffMemberDetailsQueryDto>(member);

        if (isEmailSend)
            staffMemberDto.IEmailSent = true;

        return staffMemberDto;
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
