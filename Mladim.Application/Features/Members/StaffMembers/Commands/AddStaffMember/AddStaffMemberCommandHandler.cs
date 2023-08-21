using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Mladim.Application.Contracts.EmailService;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Models;
using Mladim.Domain.Dtos;
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
    public EmailContents EmailContents { get; }

    public AddStaffMemberCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, IEmailService emailService, IOptions<EmailContents> emailContents, IMapper mapper)
    {
        UnitOfWork  = unitOfWork;
        AuthService = authService;
        EmailService = emailService;
        EmailContents = emailContents.Value;
        Mapper = mapper;
    }
   
    public async Task<StaffMemberDetailsQueryDto> Handle(AddStaffMemberCommand request, CancellationToken cancellationToken)
    {
        var organization = await this.UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        ArgumentNullException.ThrowIfNull(organization);

        var staffMember = this.Mapper.Map<StaffMember>(request);
        
        staffMember = await this.UnitOfWork.StaffMemberRepository.AddAsync(staffMember);

        await this.UnitOfWork.SaveChangesAsync();

        var appUser = await this.AuthService.ExistAppUserAsync(request.Email);

        string emailContent = string.Empty;
      

        if (appUser == null)
        {
            appUser = await CreateAppUserAsync(request.Name, request.Surname, request.Email);
            
            emailContent = string.Format(this.EmailContents.ContentAddedNewUser, organization.Attributes.Name, "url");

            await EmailService.SendPredefinedEmailAsync(emailContent, request.Email);


        }       
        else
            emailContent = string.Format(this.EmailContents.ContentUserAddedNewOrganization, organization.Attributes.Name, nameof(request.Claim));
        

        var claim = new Claim(nameof(request.Claim), request.OrganizationId.ToString());

        var identityResult = await this.AuthService.UpsertClaimAsync(appUser, claim);

        if (!identityResult.Succeeded)
            throw new Exception("Claim is not added");

        var response =  await SendEmailAsync(emailContent, request.Email);

        if (!response)
            throw new Exception("Email was not send successfuly");

        return this.Mapper.Map<StaffMemberDetailsQueryDto>(staffMember);

    }


    private async Task<bool> SendEmailAsync(string content, string receipent)
    {
        var email = new Email(EmailContents.Subject, content, receipent, EmailContents.Sender);

        return await this.EmailService.SendEmailAsync(email);
    }


    private async Task<AppUser?> CreateAppUserAsync(string name, string surname, string email)
    {
        var registerUser = RegistrationUser.Create(name, surname, name, email, this.AuthService.GenerateAppUserPassword());
        var response = await this.AuthService.RegisterAsync(registerUser);
        return response.Value;
    }


   



}
